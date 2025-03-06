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
    partial class GlobalMonitoringDataModel : ObservableObject
    {
        [ObservableProperty]
        public bool valveV1;

        [ObservableProperty]
        public bool valveV2;

        [ObservableProperty]
        public bool valveV3;

        [ObservableProperty]
        public bool valveV4;

        [ObservableProperty]
        public bool valveV5;

        [ObservableProperty]
        public bool valveV6;

        [ObservableProperty]
        public bool valveV7;

        [ObservableProperty]
        public bool valveV8;

        [ObservableProperty]
        public bool valveV9;

        [ObservableProperty]
        public bool valveV10;

        [ObservableProperty]
        public bool valveV11;

        [ObservableProperty]
        public bool valveV12;

        [ObservableProperty]
        public bool valveV13;

        [ObservableProperty]
        public bool valveV20;

        [ObservableProperty]
        public float mfc1Setpoint;

        [ObservableProperty]
        public float mfc1ActualValue;

        [ObservableProperty]
        public float mfc2Setpoint;

        [ObservableProperty]
        public float mfc2ActualValue;

        [ObservableProperty]
        public float mfc3Setpoint;

        [ObservableProperty]
        public float mfc3ActualValue;

        [ObservableProperty]
        public float mfc4Setpoint;

        [ObservableProperty]
        public float mfc4ActualValue;

        [ObservableProperty]
        public double vacuumGaugeActualPressure;

        [ObservableProperty]
        public double vacuumGaugeSetPressure;

        [ObservableProperty]
        public int butterflyValveOpening;

        [ObservableProperty]
        public float setpointTemperatureT1;

        [ObservableProperty]
        public float setpointTemperatureT2;

        [ObservableProperty]
        public float setpointTemperatureT3;

        [ObservableProperty]
        public float setpointTemperatureT4;

        [ObservableProperty]
        public float setpointTemperatureT5;

        [ObservableProperty]
        public float setpointTemperatureT6;

        [ObservableProperty]
        public float thermocoupleInternalT1;

        [ObservableProperty]
        public float thermocoupleInternalT2;

        [ObservableProperty]
        public float thermocoupleInternalT3;

        [ObservableProperty]
        public float thermocoupleInternalT4;

        [ObservableProperty]
        public float thermocoupleInternalT5;

        [ObservableProperty]
        public float thermocoupleInternalT6;

        [ObservableProperty]
        public float thermocoupleExternalT1;

        [ObservableProperty]
        public float thermocoupleExternalT2;

        [ObservableProperty]
        public float thermocoupleExternalT3;

        [ObservableProperty]
        public float thermocoupleExternalT4;

        [ObservableProperty]
        public float thermocoupleExternalT5;

        [ObservableProperty]
        public float thermocoupleExternalT6;

        [ObservableProperty]
        public float auxiliaryHeatingACurrent;

        [ObservableProperty]
        public float auxiliaryHeatingAVoltage;

        [ObservableProperty]
        public float auxiliaryHeatingBCurrent;

        [ObservableProperty]
        public float auxiliaryHeatingBVoltage;

        [ObservableProperty]
        public float auxiliaryHeatingCCurrent;

        [ObservableProperty]
        public float auxiliaryHeatingCVoltage;

        [ObservableProperty]
        public float rfVoltage;

        [ObservableProperty]
        public float rfCurrent;

        [ObservableProperty]
        public float rfPower;

        [ObservableProperty]
        public int auxiliaryHeatingValveOpening;


        // 辅热实际温度
        [ObservableProperty]
        private double auxiliaryHeatingActualTemperature;

        // 辅热设置温度
        [ObservableProperty]
        private double auxiliaryHeatingSetTemperature;

        // 辅热丝电流
        [ObservableProperty]
        private double auxiliaryHeatingWireCurrent;
    }

    public partial class GlobalMonitoringStatusModel : ObservableObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        // 工艺文件名
        [ObservableProperty]
        public string processFileName;

        // 工艺当前步
        [ObservableProperty]
        public int processCurrentStep;

        // 工艺类型
        [ObservableProperty]
        public string processType;

        // 工艺步时间
        [ObservableProperty]
        public string processStepTime;

        // 舟状态
        [ObservableProperty]
        public string boatStatus;

        // 炉门状态
        [ObservableProperty]
        public string furnaceDoorStatus;

        //用于判断当前炉管是否在工作
        [ObservableProperty]
        public bool isWork;

        //设置炉管编号
        [ObservableProperty]
        public int fnum;
    }
}
