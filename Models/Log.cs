using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp4.Models
{
    public partial class OperationLog:ObservableObject
    {
        [ObservableProperty]
        private DateTime _timestamp;

        // 执行操作的用户
        [ObservableProperty]
        private string _userName;

        // 操作的具体描述信息
        [ObservableProperty]
        private string _details;
    }

    public partial class AlarmLog:ObservableObject
    {
        // 报警的级别，例如 Info、Warning、Critical
        [ObservableProperty]
        private AlarmLevel _level;

        // 报警的具体消息内容
        [ObservableProperty]
        private string _message;

        // 报警发生的时间
        [ObservableProperty]
        private DateTime _timestamp;

        // 报警的来源，例如 "Equipment" 或具体系统名称
        [ObservableProperty]
        private string _source;
    }

    // 报警级别枚举
    public enum AlarmLevel
    {
        Info,      // 信息级别
        Warning,   // 警告级别
        Critical,  // 严重级别
        Emergency  // 紧急级别
    }
}
