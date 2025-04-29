using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WpfApp4.Models
{
    // 用于DataShowPage数据展示的模型
    public partial class ProcessDataDisplayModel : ObservableObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [ObservableProperty]
        private string _id;

        [ObservableProperty]
        private string _t1;
        [ObservableProperty]
        private string _t2;
        [ObservableProperty]
        private string _t3;
        [ObservableProperty]
        private string _t4;
        [ObservableProperty]
        private string _t5;
        [ObservableProperty]
        private string _t6;
        [ObservableProperty]
        private string _t7;
        [ObservableProperty]
        private string _t8;
        [ObservableProperty]
        private string _t9;
        [ObservableProperty]
        private string _n2;
        [ObservableProperty]
        private string _siH4;
        [ObservableProperty]
        private string _n2O;
        [ObservableProperty]
        private string _h2;
        [ObservableProperty]
        private string _pH3;
        [ObservableProperty]
        private string _pressure;
        [ObservableProperty]
        private string _power1;
        [ObservableProperty]
        private string _power2;
        [ObservableProperty]
        private string _inOut;
        [ObservableProperty]
        private string _moveSpeed;
        [ObservableProperty]
        private string _upDownSpeed;
        [ObservableProperty]
        private string _typicalHeatTime;
        [ObservableProperty]
        private string _assistTemp;
        [ObservableProperty]
        private string _pulseOn1;
        [ObservableProperty]
        private string _pulseOff1;
        [ObservableProperty]
        private string _pulseOn2;
        [ObservableProperty]
        private string _pulseOff2;
        [ObservableProperty]
        private string _rFCurrent;
        [ObservableProperty]
        private string _currentRef;
        [ObservableProperty]
        private string _currentLimit;
        [ObservableProperty]
        private string _rFVoltage;
        [ObservableProperty]
        private string _voltageRef;
        [ObservableProperty]
        private string _voltageLimit;

        public ProcessDataDisplayModel()
        {
        }
    }
} 