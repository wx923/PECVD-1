using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp4.Models
{
   public  partial  class AlarmInfo:ObservableObject
    {
        // 控制温度极限超温报警状态
        [ObservableProperty]
        public bool _controlTemperatureLimitOverheat;

        // Profile 热偶极限超温报警状态
        [ObservableProperty]
        public bool _profileThermocoupleLimitOverheat;

        // profile-1 报警状态
        [ObservableProperty]
        public bool _profile1;

        // profile-2 报警状态
        [ObservableProperty]
        public bool _profile2;

        // profile-3 报警状态
        [ObservableProperty]
        public bool _profile3;

        // profile-4 报警状态
        [ObservableProperty]
        public bool _profile4;

        // profile-5 报警状态
        [ObservableProperty]
        public bool _profile5;

        // profile-6 报警状态
        [ObservableProperty]
        public bool _profile6;

        // profile-7 报警状态
        [ObservableProperty]
        public bool _profile7;

        // profile-8 报警状态
        [ObservableProperty]
        public bool _profile8;

        // profile-9 报警状态
        [ObservableProperty]
        public bool _profile9;

        // spike-1 报警状态
        [ObservableProperty]
        public bool _spike1;

        // spike-2 报警状态
        [ObservableProperty]
        public bool _spike2;

        // spike-3 报警状态
        [ObservableProperty]
        public bool _spike3;

        // spike-4 报警状态
        [ObservableProperty]
        public bool _spike4;

        // spike-5 报警状态
        [ObservableProperty]
        public bool _spike5;

        // spike-6 报警状态
        [ObservableProperty]
        public bool _spike6;

        // spike-7 报警状态
        [ObservableProperty]
        public bool _spike7;

        // spike-8 报警状态
        [ObservableProperty]
        public bool _spike8;

        // spike-9 报警状态
        [ObservableProperty]
        public bool _spike9;

        // 固态继电器超温报警状态
        [ObservableProperty]
        public bool _solidStateRelayOverheat;

        // 变压器超温报警状态
        [ObservableProperty]
        public bool _transformerOverheat;

        // 辅助加热报警状态
        [ObservableProperty]
        public bool _auxiliaryHeating;

        // 辅助超温报警状态
        [ObservableProperty]
        public bool _auxiliaryOverheat;

        // 水流报警（前法兰）状态
        [ObservableProperty]
        public bool _waterFlowFrontFlange;

        // 水流报警（后法兰）状态
        [ObservableProperty]
        public bool _waterFlowRearFlange;

        // 水温报警（前法兰）状态
        [ObservableProperty]
        public bool _waterTempFrontFlange;

        // 水温报警（后法兰）状态
        [ObservableProperty]
        public bool _waterTempRearFlange;

        // N2流量计报警状态
        [ObservableProperty]
        public bool _n2FlowMeter;

        // SIH4流量计报警状态
        [ObservableProperty]
        public bool _sih4FlowMeter;

        // NH3流量计报警状态
        [ObservableProperty]
        public bool _nh3FlowMeter;

        // N2O流量计报警状态
        [ObservableProperty]
        public bool _n2oFlowMeter;

        // 前极限位报警状态
        [ObservableProperty]
        public bool _frontLimitPosition;

        // 后极限位报警状态
        [ObservableProperty]
        public bool _rearLimitPosition;

        // 上极限位报警状态
        [ObservableProperty]
        public bool _upperLimitPosition;

        // 下极限位报警状态
        [ObservableProperty]
        public bool _lowerLimitPosition;

        // 舟同步报警状态
        [ObservableProperty]
        public bool _boatSynchronization;

        // 压强超差报警状态
        [ObservableProperty]
        public bool _pressureDeviation;

        // 伺服异常报警状态
        [ObservableProperty]
        public bool _servoAnomaly;

        // 伺服通讯报警状态
        [ObservableProperty]
        public bool _servoCommunication;

        // N2 压力报警状态
        [ObservableProperty]
        public bool _n2Pressure;

        // 风压检测（炉口）报警状态
        [ObservableProperty]
        public bool _airPressureFurnaceMouth;

        // 风压检测（炉室）报警状态
        [ObservableProperty]
        public bool _airPressureFurnaceChamber;

        // CDA 报警（机柜）状态
        [ObservableProperty]
        public bool _cdaCabinet;

        // CDA 报警（马达）状态
        [ObservableProperty]
        public bool _cdaMotor;

        // motion stop 报警状态
        [ObservableProperty]
        public bool _motionStop;

        // 射频短路报警状态
        [ObservableProperty]
        public bool _rfShortCircuit;

        // 设备断电报警状态
        [ObservableProperty]
        public bool _equipmentPowerFailure;

        // 射频异常报警状态
        [ObservableProperty]
        public bool _rfAnomaly;

        // 射频温差报警状态
        [ObservableProperty]
        public bool _rfTempDifference;

        // SIH4泄漏报警状态
        [ObservableProperty]
        public bool _sih4Leak;

        // NH3泄漏报警状态
        [ObservableProperty]
        public bool _nh3Leak;

        // N2O泄漏报警状态
        [ObservableProperty]
        public bool _n2oLeak;
        public AlarmInfo()
        {
            _controlTemperatureLimitOverheat = true;
            _profileThermocoupleLimitOverheat = true;
            _profile1 = true;
            _profile2 = true;
            _profile3 = true;
            _profile4 = true;
            _profile5 = true;
            _profile6 = true;
            _profile7 = true;
            _profile8 = true;
            _profile9 = true;
            _spike1 = true;
            _spike2 = true;
            _spike3 = true;
            _spike4 = true;
            _spike5 = true;
            _spike6 = true;
            _spike7 = true;
            _spike8 = true;
            _spike9 = true;
            _solidStateRelayOverheat = true;
            _transformerOverheat = true;
            _auxiliaryHeating = true;
            _auxiliaryOverheat = true;
            _waterFlowFrontFlange = true;
            _waterFlowRearFlange = true;
            _waterTempFrontFlange = true;
            _waterTempRearFlange = true;
            _n2FlowMeter = true;
            _sih4FlowMeter = true;
            _nh3FlowMeter = true;
            _n2oFlowMeter = true;
            _frontLimitPosition = true;
            _rearLimitPosition = true;
            _upperLimitPosition = true;
            _lowerLimitPosition = true;
            _boatSynchronization = true;
            _pressureDeviation = true;
            _servoAnomaly = true;
            _servoCommunication = true;
            _n2Pressure = true;
            _airPressureFurnaceMouth = true;
            _airPressureFurnaceChamber = true;
            _cdaCabinet = true;
            _cdaMotor = true;
            _motionStop = true;
            _rfShortCircuit = true;
            _equipmentPowerFailure = true;
            _rfAnomaly = true;
            _rfTempDifference = true;
            _sih4Leak = true;
            _nh3Leak = true;
            _n2oLeak = true;


        }
    }
}
