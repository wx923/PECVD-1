using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NPOI.SS.Formula.Functions;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.Services.WpfApp4.Services;

namespace WpfApp4.ViewModel
{

    public partial class AlermVm :ObservableObject
    {
        private readonly int _tubeNumber;

        // 当前炉管的报警状态
        [ObservableProperty]
        private AlarmInfo _currentAlarm;

        // 当前炉管的报警日志
        [ObservableProperty]
        private ObservableCollection<AlarmLog> _alarmLogs;

        //当前操作日志
        [ObservableProperty]
        private ObservableCollection<OperationLog> _operationLogs;

        [ObservableProperty]
        private DateTime? _startDate;

        [ObservableProperty]
        private DateTime? _endDate;

        [ObservableProperty]
        private ObservableCollection<Alarmr> _alarmrLogs;

        [ObservableProperty]
        private ObservableCollection<OperationRecord> _operationRecords;

        [ObservableProperty]
        private ObservableCollection<RunningRecord> _runningRecords;
        public AlermVm(int tubeNumber)
        {
           _tubeNumber = tubeNumber;
            // 从 AlarmService 单例中获取数据
            CurrentAlarm = AlarmService.Instance._alarmStates[tubeNumber] ?? new AlarmInfo();
            AlarmLogs = AlarmService.Instance.AlarmLogs[tubeNumber];
            OperationLogs = AlarmService.Instance.OperationLogs[tubeNumber];

            // 加载当天数据
            LoadTodayDataAsync();
        }

        private async void LoadTodayDataAsync()
        {
            try
            {
                // 获取当天时间范围（本地时间）
                var todayStart = DateTime.Today; // 00:00:00 本地时间
                var todayEnd = todayStart.AddDays(1).AddTicks(-1); // 23:59:59.9999999 本地时间

                // 转换为 UTC 时间用于查询（假设数据库存储的是 UTC 时间）
                var todayStartUtc = todayStart.ToUniversalTime();
                var todayEndUtc = todayEnd.ToUniversalTime();

                // 并行查询
                var alarmTask = MongoDbService.Instance.GetAlarmLogsByDateRangeAsync(todayStartUtc, todayEndUtc);
                var operationTask = MongoDbService.Instance.GetOperationRecordsByDateRangeAsync(todayStartUtc, todayEndUtc);
                var runningTask = MongoDbService.Instance.GetRunningRecordsByDateRangeAsync(todayStartUtc, todayEndUtc);

                await Task.WhenAll(alarmTask, operationTask, runningTask);

                var alarmLogs = alarmTask.Result ?? new List<Alarmr>();
                var operationRecords = operationTask.Result ?? new List<OperationRecord>();
                var runningRecords = runningTask.Result ?? new List<RunningRecord>();

                // 确保集合已初始化
                AlarmrLogs ??= new ObservableCollection<Alarmr>();
                OperationRecords ??= new ObservableCollection<OperationRecord>();
                RunningRecords ??= new ObservableCollection<RunningRecord>();

                // 在UI线程中更新集合
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    AlarmrLogs.Clear();
                    foreach (var log in alarmLogs)
                    {
                        // 将 UTC 时间转换为本地时间（如果数据库返回的是 UTC 时间）
                        log.Timestamp = log.Timestamp.ToLocalTime();
                        AlarmrLogs.Add(log);
                    }

                    OperationRecords.Clear();
                    foreach (var record in operationRecords)
                    {
                        record.Timestamp = record.Timestamp.ToLocalTime();
                        OperationRecords.Add(record);
                    }

                    RunningRecords.Clear();
                    foreach (var record in runningRecords)
                    {
                        record.OperationTime = record.OperationTime.ToLocalTime();
                        RunningRecords.Add(record);
                    }

                    // 设置默认日期为今天
                    StartDate = todayStart;
                    EndDate = todayEnd;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载当天数据失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private async Task GenerateTestRecords()
        {
            try
            {
                // 生成测试记录
                var now = DateTime.Now;

                var testAlarm = new Alarmr
                {
                    Timestamp = now,
                    User = "TestUser",
                    Level = "高",
                    RecoveryTime = null,
                    Details = "测试报警：温度超限",
                    Description = "模拟高温报警"
                };

                var testOperation = new OperationRecord
                {
                    Timestamp = now,
                    User = "TestUser",
                    RecoveryTime = null,
                    Details = "测试操作：启动设备",
                    Description = "模拟手动启动"
                };

                var testRunning = new RunningRecord
                {
                    OperationTime = now,
                    User = "TestUser",
                    OperationMode = "手动",
                    DeviceName = "TestDevice",
                    BoatNumber = "BN001",
                    BoatStatus = "运行",
                    Description = "模拟设备运行记录"
                };

                // 插入数据库
                await MongoDbService.Instance.AddAlarmLogAsync(testAlarm);
                await MongoDbService.Instance.AddOperationRecordAsync(testOperation);
                await MongoDbService.Instance.AddRunningRecordAsync(testRunning);

                // 刷新当天数据
                await Task.Run(() => LoadTodayDataAsync());

                MessageBox.Show("测试记录生成成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"生成测试记录失败: {ex.Message}");
            }
        }
        [RelayCommand]
        private void ClearOperationLogs()
        {
            OperationLogs.Clear();
            // 可选：记录清除操作
            OperationLogs.Add(new OperationLog
            {
                Timestamp = DateTime.Now,
                UserName = "User",
                Details = $"炉管 {_tubeNumber + 1} 操作日志已清除"
            });
        }

        [RelayCommand]
        private void ClearAlarmLogs()
        {
            AlarmLogs.Clear();
            // 可选：记录清除操作到操作日志
            OperationLogs.Add(new OperationLog
            {
                Timestamp = DateTime.Now,
                UserName = "User",
                Details = $"炉管 {_tubeNumber + 1} 报警日志已清除"
            });
        }
        [RelayCommand]
        private async Task QueryLogsByDateRange()
        {
            try
            {
                // 验证日期
                if (!StartDate.HasValue || !EndDate.HasValue)
                {
                    MessageBox.Show("请选择起始和结束日期");
                    return;
                }

                if (EndDate < StartDate)
                {
                    MessageBox.Show("结束日期不能早于起始日期");
                    return;
                }

                // 查询数据库
                var alarmrLogs = await MongoDbService.Instance.GetAlarmLogsByDateRangeAsync(StartDate.Value, EndDate.Value);
                var operationRecords = await MongoDbService.Instance.GetOperationRecordsByDateRangeAsync(StartDate.Value, EndDate.Value);
                var runningRecords = await MongoDbService.Instance.GetRunningRecordsByDateRangeAsync(StartDate.Value, EndDate.Value);

                // 更新集合
                AlarmrLogs.Clear();
                foreach (var log in alarmrLogs)
                {
                    AlarmrLogs.Add(log);
                }

                OperationRecords.Clear();
                foreach (var record in operationRecords)
                {
                    OperationRecords.Add(record);
                }

                RunningRecords.Clear();
                foreach (var record in runningRecords)
                {
                    RunningRecords.Add(record);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败: {ex.Message}");
            }
        }
    }
}
