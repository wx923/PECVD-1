using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WpfApp4.Models
{
    public partial class Alarmr : ObservableObject
    {
        [BsonId] // 标记这是 _id 字段
        [BsonRepresentation(BsonType.ObjectId)] // 让 MongoDB 自动转换 string <-> ObjectId
        public string Id { get; set; }

        /// <summary>
        /// 报警触发时间
        /// </summary>
        [ObservableProperty]
        private DateTime _timestamp;

        /// <summary>
        /// 触发报警的用户
        /// </summary>
        [ObservableProperty]
        private string _user;

        /// <summary>
        /// 报警等级（如：低、中、高）
        /// </summary>
        [ObservableProperty]
        private string _level;

        /// <summary>
        /// 报警恢复时间（可为空）
        /// </summary>
        [ObservableProperty]
        private DateTime? _recoveryTime;

        /// <summary>
        /// 报警详情
        /// </summary>
        [ObservableProperty]
        private string _details;

        /// <summary>
        /// 报警的描述信息
        /// </summary>
        [ObservableProperty]
        private string _description;
    }

    /// <summary>
    /// 操作记录类，记录用户操作的相关信息
    /// </summary>
    public partial class OperationRecord : ObservableObject
    {
        [BsonId] // 标记这是 _id 字段
        [BsonRepresentation(BsonType.ObjectId)] // 让 MongoDB 自动转换 string <-> ObjectId
        public string Id { get; set; }

        /// <summary>
        /// 操作触发时间
        /// </summary>
        [ObservableProperty]
        private DateTime _timestamp;

        /// <summary>
        /// 执行操作的用户
        /// </summary>
        [ObservableProperty]
        private string _user;

        /// <summary>
        /// 操作恢复时间（可为空）
        /// </summary>
        [ObservableProperty]
        private DateTime? _recoveryTime;

        /// <summary>
        /// 操作详情
        /// </summary>
        [ObservableProperty]
        private string _details;

        /// <summary>
        /// 操作的描述信息
        /// </summary>
        [ObservableProperty]
        private string _description;
    }

    /// <summary>
    /// 运行记录类，记录设备运行状态的相关信息
    /// </summary>
    public partial class RunningRecord : ObservableObject
    {
        [BsonId] // 标记这是 _id 字段
        [BsonRepresentation(BsonType.ObjectId)] // 让 MongoDB 自动转换 string <-> ObjectId
        public string Id { get; set; }

        /// <summary>
        /// 操作发生的时间
        /// </summary>
        [ObservableProperty]
        private DateTime _operationTime;

        /// <summary>
        /// 执行操作的用户
        /// </summary>
        [ObservableProperty]
        private string _user;

        /// <summary>
        /// 操作模式（如：手动、自动）
        /// </summary>
        [ObservableProperty]
        private string _operationMode;

        /// <summary>
        /// 设备名称
        /// </summary>
        [ObservableProperty]
        private string _deviceName;

        /// <summary>
        /// 舟号
        /// </summary>
        [ObservableProperty]
        private string _boatNumber;

        /// <summary>
        /// 舟状态（如：运行、停止、故障）
        /// </summary>
        [ObservableProperty]
        private string _boatStatus;

        /// <summary>
        /// 运行记录的描述信息
        /// </summary>
        [ObservableProperty]
        private string _description;
    }
}
