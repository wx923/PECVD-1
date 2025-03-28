using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp4.Models
{
    partial class AlarmInfo
    {
        // 控制温度极限超温报警状态
        [ObservableProperty]
        private bool controlTemperatureLimitOverheat;

        // Profile 热偶极限超温报警状态
        [ObservableProperty]
        private bool profileThermocoupleLimitOverheat;

        // profile-1 报警状态
        [ObservableProperty]
        private bool profile1;

        // profile-2 报警状态
        [ObservableProperty]
        private bool profile2;

        // profile-3 报警状态
        [ObservableProperty]
        private bool profile3;

        // profile-4 报警状态
        [ObservableProperty]
        private bool profile4;

        // profile-5 报警状态
        [ObservableProperty]
        private bool profile5;

        // profile-6 报警状态
        [ObservableProperty]
        private bool profile6;

        // profile-7 报警状态
        [ObservableProperty]
        private bool profile7;

        // profile-8 报警状态
        [ObservableProperty]
        private bool profile8;

        // profile-9 报警状态
        [ObservableProperty]
        private bool profile9;

        // spike-1 报警状态
        [ObservableProperty]
        private bool spike1;

        // spike-2 报警状态
        [ObservableProperty]
        private bool spike2;

        // spike-3 报警状态
        [ObservableProperty]
        private bool spike3;

        // spike-4 报警状态
        [ObservableProperty]
        private bool spike4;

        // spike-5 报警状态
        [ObservableProperty]
        private bool spike5;

        // spike-6 报警状态
        [ObservableProperty]
        private bool spike6;

        // spike-7 报警状态
        [ObservableProperty]
        private bool spike7;

        // spike-8 报警状态
        [ObservableProperty]
        private bool spike8;

        // spike-9 报警状态
        [ObservableProperty]
        private bool spike9;

        // 固态继电器超温报警状态
        [ObservableProperty]
        private bool solidStateRelayOverheat;

        // 变压器超温报警状态
        [ObservableProperty]
        private bool transformerOverheat;

        // 辅助加热报警状态
        [ObservableProperty]
        private bool auxiliaryHeating;

        // 辅助超温报警状态
        [ObservableProperty]
        private bool auxiliaryOverheat;

        // 水流报警（前法兰）状态
        [ObservableProperty]
        private bool waterFlowFrontFlange;

        // 水流报警（后法兰）状态
        [ObservableProperty]
        private bool waterFlowRearFlange;

        // 水温报警（前法兰）状态
        [ObservableProperty]
        private bool waterTempFrontFlange;

        // 水温报警（后法兰）状态
        [ObservableProperty]
        private bool waterTempRearFlange;

        // N2流量计报警状态
        [ObservableProperty]
        private bool n2FlowMeter;

        // SIH4流量计报警状态
        [ObservableProperty]
        private bool sih4FlowMeter;

        // NH3流量计报警状态
        [ObservableProperty]
        private bool nh3FlowMeter;

        // N2O流量计报警状态
        [ObservableProperty]
        private bool n2oFlowMeter;

        // 前极限位报警状态
        [ObservableProperty]
        private bool frontLimitPosition;

        // 后极限位报警状态
        [ObservableProperty]
        private bool rearLimitPosition;

        // 上极限位报警状态
        [ObservableProperty]
        private bool upperLimitPosition;

        // 下极限位报警状态
        [ObservableProperty]
        private bool lowerLimitPosition;

        // 舟同步报警状态
        [ObservableProperty]
        private bool boatSynchronization;

        // 压强超差报警状态
        [ObservableProperty]
        private bool pressureDeviation;

        // 伺服异常报警状态
        [ObservableProperty]
        private bool servoAnomaly;

        // 伺服通讯报警状态
        [ObservableProperty]
        private bool servoCommunication;

        // N2 压力报警状态
        [ObservableProperty]
        private bool n2Pressure;

        // 风压检测（炉口）报警状态
        [ObservableProperty]
        private bool airPressureFurnaceMouth;

        // 风压检测（炉室）报警状态
        [ObservableProperty]
        private bool airPressureFurnaceChamber;

        // CDA 报警（机柜）状态
        [ObservableProperty]
        private bool cdaCabinet;

        // CDA 报警（马达）状态
        [ObservableProperty]
        private bool cdaMotor;

        // motion stop 报警状态
        [ObservableProperty]
        private bool motionStop;

        // 射频短路报警状态
        [ObservableProperty]
        private bool rfShortCircuit;

        // 设备断电报警状态
        [ObservableProperty]
        private bool equipmentPowerFailure;

        // 射频异常报警状态
        [ObservableProperty]
        private bool rfAnomaly;

        // 射频温差报警状态
        [ObservableProperty]
        private bool rfTempDifference;

        // SIH4泄漏报警状态
        [ObservableProperty]
        private bool sih4Leak;

        // NH3泄漏报警状态
        [ObservableProperty]
        private bool nh3Leak;

        // N2O泄漏报警状态
        [ObservableProperty]
        private bool n2oLeak;
    }
}
