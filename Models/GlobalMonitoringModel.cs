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
   public partial class GlobalMonitoringDataModel : ObservableObject
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

    [BsonIgnoreExtraElements]
    public partial class GlobalMonitoringStatusModel : ObservableObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        // 工艺文件名
        [ObservableProperty]
        public string _processFileName;

        // 工艺当前步
        [ObservableProperty]
        public int _processCurrentStep;

        // 工艺类型
        [ObservableProperty]
        public string _processType;

        // 工艺步时间
        [ObservableProperty]
        public int _processStepTime;

        // 舟状态
        [ObservableProperty]
        public string _boatStatus;

        // 炉门状态
        [ObservableProperty]
        public string _furnaceDoorStatus;

        //用于判断当前炉管是否在工作
        [ObservableProperty]
        public bool _isWork;

        //设置炉管编号
        [ObservableProperty]
        public int _fnum;

        public GlobalMonitoringStatusModel()
        {
            _id = ObjectId.GenerateNewId().ToString();
            _processFileName = "无工艺文件";
            _processStepTime = 0;
            _processType = "无";
            _processCurrentStep = 0;
            _isWork = false;
        }
    }


    //工艺定时采集数据数据结构
        public partial class RegularCollectDataModel : ObservableObject
        {
            [ObservableProperty]
            private DateTime _timestamp; // 数据采集时间戳

            [ObservableProperty]
            private TimeSpan _processTime; // 工艺时间

            [ObservableProperty]
            private TimeSpan _remainingTime; // 剩余时间

            [ObservableProperty]
            private int _stepNumber; // 步号

            [ObservableProperty]
            private string _processType; // 工艺类型

            [ObservableProperty]
            private string _boatNumber; // 舟号

            [ObservableProperty]
            private string _stopReason; // 停止原因

            // 设定温度区 1-6
            [ObservableProperty]
            private double _setTempZone1;

            [ObservableProperty]
            private double _setTempZone2;

            [ObservableProperty]
            private double _setTempZone3;

            [ObservableProperty]
            private double _setTempZone4;

            [ObservableProperty]
            private double _setTempZone5;

            [ObservableProperty]
            private double _setTempZone6;

            // 实际温度区 1-6
            [ObservableProperty]
            private double _realTempZone1;

            [ObservableProperty]
            private double _realTempZone2;

            [ObservableProperty]
            private double _realTempZone3;

            [ObservableProperty]
            private double _realTempZone4;

            [ObservableProperty]
            private double _realTempZone5;

            [ObservableProperty]
            private double _realTempZone6;

            // MFC 设定值和实际值
            [ObservableProperty]
            private double _setMFC1;

            [ObservableProperty]
            private double _setMFC2;

            [ObservableProperty]
            private double _setMFC3;

            [ObservableProperty]
            private double _setMFC4;

            [ObservableProperty]
            private double _realMFC1;

            [ObservableProperty]
            private double _realMFC2;

            [ObservableProperty]
            private double _realMFC3;

            [ObservableProperty]
            private double _realMFC4;

            // 射频相关
            [ObservableProperty]
            private double _rfPowerSet; // 射频功率设定

            [ObservableProperty]
            private double _rfPowerActual; // 射频功率实际

            [ObservableProperty]
            private double _rfCurrent; // 射频电流

            [ObservableProperty]
            private double _rfVoltage; // 射频电压

            [ObservableProperty]
            private double _dutyCycle; // 占空比

            [ObservableProperty]
            private double _butterflyValveAngle; // 蝶阀角度

            // 腔体压力
            [ObservableProperty]
            private double _chamberPressureSet; // 腔体压力设定

            [ObservableProperty]
            private double _chamberPressureActual; // 腔体压力实际

            // 辅热相关
            [ObservableProperty]
            private double _auxHeatPowerActual; // 辅热实际功率

            [ObservableProperty]
            private double _auxHeatTempActual; // 辅热实际温度

            [ObservableProperty]
            private double _auxHeatTempSet; // 辅热设定温度

            [ObservableProperty]
            private double _auxHeatCurrentA; // 辅热A相电流

            [ObservableProperty]
            private double _auxHeatCurrentB; // 辅热B相电流

            [ObservableProperty]
            private double _auxHeatCurrentC; // 辅热C相电流

       public RegularCollectDataModel()
        {
            Timestamp = DateTime.Now;
            ProcessTime = TimeSpan.FromMinutes(60); // 工艺时间 60 分钟
            RemainingTime = TimeSpan.FromMinutes(30); // 剩余时间 30 分钟
            StepNumber = 1; // 步号
            ProcessType = "升温"; // 工艺类型
            BoatNumber = "Boat 1"; // 舟号
            StopReason = "正常"; // 停止原因
            SetTempZone1 = 400; // 设定温度区
            SetTempZone2 = 410;
            SetTempZone3 = 420;
            SetTempZone4 = 430;
            SetTempZone5 = 440;
            SetTempZone6 = 450;
            RealTempZone1 = 395; // 实际温度区
            RealTempZone2 = 405;
            RealTempZone3 = 415;
            RealTempZone4 = 425;
            RealTempZone5 = 435;
            RealTempZone6 = 445;
            SetMFC1 = 30; // MFC 设定值
            SetMFC2 = 30;
            SetMFC3 = 30;
            SetMFC4 = 30;
            RealMFC1 = 29; // MFC 实际值
            RealMFC2 = 29;
            RealMFC3 = 29;
            RealMFC4 = 29;
            RfPowerSet = 100; // 射频功率设定
            RfPowerActual = 98; // 射频功率实际
            RfCurrent = 5.0; // 射频电流
            RfVoltage = 50.0; // 射频电压
            DutyCycle = 50.0; // 占空比
            ButterflyValveAngle = 45.0; // 蝶阀角度
            ChamberPressureSet = 40.0; // 腔体压力设定
            ChamberPressureActual = 39.5; // 腔体压力实际
            AuxHeatPowerActual = 60; // 辅热实际功率
            AuxHeatTempActual = 250; // 辅热实际温度
            AuxHeatTempSet = 260; // 辅热设定温度
            AuxHeatCurrentA = 15.0; // 辅热 A 相电流
            AuxHeatCurrentB = 15.0; // 辅热 B 相电流
            AuxHeatCurrentC = 15.0; // 辅热 C 相电流
        }
    }
}
