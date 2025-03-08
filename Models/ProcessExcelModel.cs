using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WpfApp4.Models
{
    public partial class ProcessExcelModel : ObservableObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [ObservableProperty]
        private string fileId = string.Empty;

        [ObservableProperty]
        private int step; // 步数

        [ObservableProperty]
        private string name; // 名称

        [ObservableProperty]
        private int time; // 时间（s）

        [ObservableProperty]
        private int t1; // T1

        [ObservableProperty]
        private int t2; // T2

        [ObservableProperty]
        private int t3; // T3

        [ObservableProperty]
        private int t4; // T4

        [ObservableProperty]
        private int t5; // T5

        [ObservableProperty]
        private int t6; // T6

        [ObservableProperty]
        private int n2; // N2

        [ObservableProperty]
        private int sih4; // SiH4

        [ObservableProperty]
        private int n2o; // N2O

        [ObservableProperty]
        private int nh3; // NH3

        [ObservableProperty]
        private int pressureValue; // 压力值

        [ObservableProperty]
        private int power; // 电源功率

        [ObservableProperty]
        private int pulseOn; // 脉冲开

        [ObservableProperty]
        private int pulseOff; // 脉冲关

        [ObservableProperty]
        private int moveSpeed; // 进舟速度

        [ObservableProperty]
        private int retreatSpeed; // 退舟速度

        [ObservableProperty]
        private int verticalSpeed; // 垂直速度

        [ObservableProperty]
        private int auxiliaryHeatTemperature; // 辅热温度
    }

    public partial class ProcessFileInfo : ObservableObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [ObservableProperty]
        public string fileName = string.Empty;

        [ObservableProperty]
        public DateTime createTime = DateTime.Now;

        [ObservableProperty]
        public string description = string.Empty;

        [ObservableProperty]
        public string collectionName = string.Empty;

        [ObservableProperty]
        public bool isSelected;
    }
} 