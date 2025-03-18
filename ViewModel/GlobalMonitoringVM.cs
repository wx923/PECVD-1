using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Driver;
using WpfApp4.Models;
using WpfApp4.Services;

namespace WpfApp4.ViewModel
{
    partial class GlobalMonitoringVM :ObservableObject
    {
        [ObservableProperty]
        public GlobalMonitoringDataModel _dataCollection;

        //步骤监控对象
        [ObservableProperty]
        public GlobalMonitoringStatusModel _progressMonitor;

        //配方文件结构
        public Dictionary<int, ProcessExcelModel> _processExcels;

        //用于倒计时的秒数
        [ObservableProperty]
        public int _countdownValue;

        //设置定时器用于显示页面数据
        private DispatcherTimer _timer;
        [ObservableProperty]
        //开始工艺运行标志
        public bool _isProcessRunning;
        //是否暂停
        [ObservableProperty]
        public bool _isPaused;

        public CancellationTokenSource _countdownCancellation;    


        int _turbNum = 0;
        public GlobalMonitoringVM(int turbNum) {
            _progressMonitor = MongoDbService.Instance.GlobalProcessFlowSteps.FirstOrDefault(x => x.Fnum == (turbNum - 1));
            _dataCollection = GlobalMonitoringService.Instance.GlobalMonitoringAllData[turbNum - 1];
            _processExcels = new Dictionary<int, ProcessExcelModel>();
            _turbNum=turbNum-1;
            //设置定时器触发事件为1s
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _isProcessRunning = false;
            _isPaused=false;
            _countdownCancellation=new CancellationTokenSource();
        }

        #region 辅助函数

        private Task WaitForCountdown()
        {
            if (CountdownValue <= 0) return Task.CompletedTask;

            var tcs = new TaskCompletionSource<bool>();
            EventHandler handler = null;
            var token=_countdownCancellation.Token;
            handler = (s, e) =>
            {
                if (token.IsCancellationRequested)
                {
                    _timer.Stop();
                    _timer.Tick -= handler;
                    tcs.TrySetCanceled();
                    return;
                }
                if (_isPaused)
                {
                    // 暂停时不减少 CountdownValue，保持等待
                    //GlobalMonitoringService.Instance.PausePlcDataCollection(_turbNum);
                    return;
                }

                if (CountdownValue > 0)
                {
                    CountdownValue--; // 未暂停时减少倒计时
                }

                if (CountdownValue <= 0)
                {
                    _timer.Stop();
                    _timer.Tick -= handler;
                    tcs.SetResult(true); // 倒计时完成
                }
            };
            _timer.Tick -=handler;
            _timer.Tick += handler;
            if (!_timer.IsEnabled) _timer.Start(); // 确保定时器启动

            // 清理 handler
            tcs.Task.ContinueWith(_ =>
            {
                if (_timer.IsEnabled)
                {
                    _timer.Tick -= handler;
                }
            },TaskContinuationOptions.OnlyOnRanToCompletion);

            return tcs.Task;
        }


        #endregion


        #region 开始工艺
        [RelayCommand(CanExecute =nameof(CanStartProcess))]
        public async Task StartProcess() {
            try
            {
                //开始运行条件判断
                if (_isProcessRunning) return; // 如果已经在运行，直接返回

                _isProcessRunning = true; // 设置为运行中

                //从工艺配方表中读取对应的结构
                // 使用await等待异步操作完成
                var list = await MongoDbService.Instance._database.GetCollection<ProcessExcelModel>(_progressMonitor.ProcessFileName)
                    .Find(ProcessExcelModel => true)
                    .ToListAsync();
                if (!list.Any())
                {
                    _isProcessRunning = false;
                    return;
                }

                // 将列表转换为字典（假设ProcessExcelModel有一个Id属性）
                int index = 0; // 初始化索引变量
                foreach (var item in list)
                {
                    _processExcels[index] = item; // 使用索引作为字典的键
                    index++; // 索引递增
                }

                //判断是否能够开始工艺


                //开始工艺数据采集

                // 启动当前炉管的 PLC 数据采集
                GlobalMonitoringService.Instance.StartPlcDataCollection(_turbNum, true);

                //开始工艺循环
                for (var i = 0; i < _processExcels.Count; i++)
                {
                    //修改工艺步对象信息
                    ProgressMonitor.ProcessCurrentStep = _processExcels[i].Step;
                    ProgressMonitor.ProcessType = _processExcels[i].Name;
                    CountdownValue = _processExcels[i].Time;
                    switch (_processExcels[i].Name)
                    {
                        case "装片":
                            ProgressMonitor.BoatStatus = "进舟运动中";
                            await LoadSampleAsync();
                            break;
                        case "升温":
                            ProgressMonitor.BoatStatus = "无运动";
                            await HeatUpAsync();
                            break;
                        case "慢抽真空":
                            await SlowPumpDownAsync();
                            break;
                        case "抽真空":
                            await PumpDownAsync();
                            break;
                        case "检漏":
                            await DetectAsync();
                            break;
                        case "调压":
                            await AdjustPressureAsync();
                            break;
                        case "淀积":
                            await DepositAsync();
                            break;
                        case "清洗":
                            await CleanAsync();
                            break;
                        case "充氮":
                            await FillWithNitrogenAsync();
                            break;
                        case "卸片":
                            ProgressMonitor.BoatStatus = "进舟运动中";
                            await UnloadSampleAsync();
                            break;
                        default:
                            Console.WriteLine($"未知步骤: {_processExcels[i].Name}");
                            break;
                    }
                    // 等待倒计时，支持中途暂停
                    await WaitForCountdown();

                }
                //停止当前炉管的 PLC 数据采集并导出
                GlobalMonitoringService.Instance.StopPlcDataCollection(_turbNum);
                await GlobalMonitoringService.Instance.ExportPlcDataToExcelAsync(_turbNum);
            }
            catch (TaskCanceledException)
            {

            }
            finally
            {
                _isProcessRunning = false;
                _isPaused = false;
                if (_timer.IsEnabled) _timer.Stop(); // 确保结束时停止定时器
            }

        }
        //判断能够开始工艺的条件
        private bool CanStartProcess()
        {
            return !_isProcessRunning; // 只有未运行时才允许执行
        }


        private async Task LoadSampleAsync()
        {
            // 实现装片逻辑
        }

        private async Task HeatUpAsync()
        {
            // 实现升温逻辑
        }

        private async Task SlowPumpDownAsync()
        {
            // 实现慢抽真空逻辑
        }

        private async Task PumpDownAsync()
        {
            // 实现抽真空逻辑
        }

        private async Task DetectAsync()
        {
            // 实现检测逻辑
        }
        private async Task AdjustPressureAsync()
        {
            //实现调压
        }
        private async Task FillWithNitrogenAsync()
        {
            //实现充氮
        }

        private async Task DepositAsync()
        {
            // 实现沉积逻辑
        }

        private async Task CleanAsync()
        {
            // 实现清洗逻辑
        }

        private async Task UnloadSampleAsync()
        {
            // 实现卸片逻辑
        }
        #endregion

        #region 暂停工艺
        [RelayCommand]
        public void PauseProcess() {
            if (_isProcessRunning && !_isPaused)
            {
                _isPaused = true;
                _isProcessRunning = false;
                //GlobalMonitoringService.Instance.PausePlcDataCollection(_turbNum);
                // 不停止 _timer，因为 WaitForCountdown 会在暂停时停止倒计时
            }
        }

        #endregion

        #region 恢复工艺
        [RelayCommand]
        public void ResumeProcess()
        {
            if (!_isProcessRunning && _isPaused)
            {
                _isPaused = false;
                _isProcessRunning=true;
                //GlobalMonitoringService.Instance.ResumePlcDataCollection(_turbNum);
                // 不需要手动启动 _timer，因为 WaitForCountdown 已在运行中
            }
        }

        #endregion


        #region 跳步工艺

        [RelayCommand]
        public async void SkipStep()
        {

            try
            { //什么条件下才允许跳步


                //完成暂停工艺以及相关数据恢复
                PauseProcess();
                _countdownCancellation.Cancel();
                _countdownCancellation.Dispose();
                _countdownCancellation = new CancellationTokenSource();

                //提示输入框

                string input = Microsoft.VisualBasic.Interaction.InputBox("请输入要跳转的步骤编号 (1-" + (_processExcels.Count ) + "):",
                    "跳步操作",
                    ProgressMonitor.ProcessCurrentStep.ToString());
                if (string.IsNullOrEmpty(input)) return;

                if (int.TryParse(input, out int target) && target > 0 && target <= _processExcels.Count)
                {
                    int targetStep = target - 1;
                    ProgressMonitor.ProcessCurrentStep = targetStep;
                    ProgressMonitor.ProcessType = _processExcels[targetStep].Name;
                    CountdownValue = _processExcels[targetStep].Time;

                    _isPaused = false;
                    _isProcessRunning = true;

                    //开始工作循环
                    for (var i = targetStep; i < _processExcels.Count; i++)
                    {
                        ProgressMonitor.ProcessCurrentStep = _processExcels[i].Step;
                        ProgressMonitor.ProcessType = _processExcels[i].Name;
                        CountdownValue = _processExcels[i].Time;

                        switch (_processExcels[i].Name)
                        {
                            case "装片":
                                ProgressMonitor.BoatStatus = "进舟运动中";
                                await LoadSampleAsync();
                                break;
                            case "升温":
                                ProgressMonitor.BoatStatus = "无运动";
                                await HeatUpAsync();
                                break;
                            case "慢抽真空":
                                await SlowPumpDownAsync();
                                break;
                            case "抽真空":
                                await PumpDownAsync();
                                break;
                            case "检漏":
                                await DetectAsync();
                                break;
                            case "调压":
                                await AdjustPressureAsync();
                                break;
                            case "淀积":
                                await DepositAsync();
                                break;
                            case "清洗":
                                await CleanAsync();
                                break;
                            case "充氮":
                                await FillWithNitrogenAsync();
                                break;
                            case "卸片":
                                ProgressMonitor.BoatStatus = "进舟运动中";
                                await UnloadSampleAsync();
                                break;
                            default:
                                Console.WriteLine($"未知步骤: {_processExcels[i].Name}");
                                break;
                        }
                        await WaitForCountdown();

                    }
                    //生成工艺文件
                    GlobalMonitoringService.Instance.StopPlcDataCollection(_turbNum);
                    await GlobalMonitoringService.Instance.ExportPlcDataToExcelAsync(_turbNum);
                }
                else
                {
                    MessageBox.Show("输入的步骤编号无效，请输入 1 到 " + (_processExcels.Count ) + " 之间的数字",
                    "错误",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    ResumeProcess();
                }
            }
            catch (TaskCanceledException)
            {

            }
            finally
            {
                _isPaused = false;
                _isProcessRunning = false;
                if(_timer.IsEnabled) _timer.Stop();
                _countdownCancellation.Cancel();
                _countdownCancellation.Dispose();
                _countdownCancellation = new CancellationTokenSource();
            }
        }
        #endregion

        #region 终止工艺
        [RelayCommand]
        public async void TerminateProcess()
        {
            //终止工艺执行条件
            if (!_isProcessRunning) return;
            PauseProcess();
            //暂停工艺信息的采集
            GlobalMonitoringService.Instance.StopPlcDataCollection(_turbNum);

            //执行相关信息的下发

            //状态改变
            _isProcessRunning= false;
            _isPaused = false;
            _countdownCancellation.Cancel();
            _countdownCancellation.Dispose();
            _countdownCancellation = new CancellationTokenSource();
            if(_timer.IsEnabled)_timer.Stop();
            await GlobalMonitoringService.Instance.ExportPlcDataToExcelAsync(_turbNum);
        }
        #endregion
    }
}
