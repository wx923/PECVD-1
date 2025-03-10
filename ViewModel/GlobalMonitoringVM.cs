using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        int _turbNum = 0;
        public GlobalMonitoringVM(int turbNum) {
            _progressMonitor = MongoDbService.Instance.GlobalProcessFlowSteps.FirstOrDefault(x => x.Fnum == (turbNum - 1));
            _dataCollection = GlobalMonitoringService.Instance.GlobalMonitoringAllData[turbNum - 1];
            _processExcels = new Dictionary<int, ProcessExcelModel>();
            _turbNum=turbNum;
            //设置定时器触发事件为1s
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
        }

        #region 辅助函数

        private Task WaitForCountdown()
        {
            if (CountdownValue <= 0) return Task.CompletedTask; // 如果倒计时已为 0，直接完成

            var tcs = new TaskCompletionSource<bool>();
            EventHandler handler = null;
            handler = (s, e) =>
            {
                if (CountdownValue <= 0)
                {
                    _timer.Stop();
                    _timer.Tick -= handler;
                    tcs.SetResult(true);
                }
            };
            _timer.Tick += handler;
            if (!_timer.IsEnabled) _timer.Start(); // 确保定时器启动
            return tcs.Task;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CountdownValue > 0)
            {
                CountdownValue--;
            }
            else
            {
                _timer.Stop(); // 每步结束后停止定时器
            }
        }
        #endregion


        #region 开始工艺
        [RelayCommand]
        public async Task StartProcess() {
            //从工艺配方表中读取对应的结构
            // 使用await等待异步操作完成
            var list = await MongoDbService.Instance._database.GetCollection<ProcessExcelModel>(_progressMonitor.ProcessFileName)
                .Find(ProcessExcelModel => true)
                .ToListAsync();


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
            GlobalMonitoringService.Instance.StartPlcDataCollection(_turbNum);

            //开始工艺循环
            for (var i= 0; i < _processExcels.Count; i++)
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
                // 等待倒计时结束
                await WaitForCountdown();
            }
            //停止当前炉管的 PLC 数据采集并导出
            GlobalMonitoringService.Instance.StopPlcDataCollection(_turbNum);
            await GlobalMonitoringService.Instance.ExportPlcDataToExcelAsync(_turbNum);


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
    }
}
