using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp4.Models;
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
        public AlermVm(int tubeNumber)
        {
           _tubeNumber = tubeNumber;
            // 从 AlarmService 单例中获取数据
            CurrentAlarm = AlarmService.Instance._alarmStates[tubeNumber] ?? new AlarmInfo();
            AlarmLogs = AlarmService.Instance.AlarmLogs[tubeNumber];
            OperationLogs = AlarmService.Instance.OperationLogs[tubeNumber];
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

    }
}
