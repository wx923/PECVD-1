using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp4.Models
{
    public partial class HomePageModel : ObservableObject
    {
        // 内偶温度 (6个炉管的值)
        [ObservableProperty]
        private double _innerTemperatures1;
        [ObservableProperty]
        private double _innerTemperatures2;
        [ObservableProperty]
        private double _innerTemperatures3;
        [ObservableProperty]
        private double _innerTemperatures4;
        [ObservableProperty]
        private double _innerTemperatures5;
        [ObservableProperty]
        private double _innerTemperatures6;
        [ObservableProperty]
        private double _innerTemperatures7;
        [ObservableProperty]
        private double _innerTemperatures8;
        [ObservableProperty]
        private double _innerTemperatures9;

        // 外偶温度 (6个炉管的值)
        [ObservableProperty]
        private double _outTemperatures1;
        [ObservableProperty]
        private double _outTemperatures2;
        [ObservableProperty]
        private double _outTemperatures3;
        [ObservableProperty]
        private double _outTemperatures4;
        [ObservableProperty]
        private double _outTemperatures5;
        [ObservableProperty]
        private double _outTemperatures6;
        [ObservableProperty]
        private double _outTemperatures7;
        [ObservableProperty]
        private double _outTemperatures8;
        [ObservableProperty]
        private double _outTemperatures9;

        // N2 流量
        [ObservableProperty]
        private double _n2FlowRate;

        // NH3 流量
        [ObservableProperty]
        private double _nh3FlowRate;

        // SiH4 流量
        [ObservableProperty]
        private double _siH4FlowRate;

        // N2O 流量
        [ObservableProperty]
        private double _n2OFlowRate;

        // 射频功率
        [ObservableProperty]
        private double _rfPower;

        // 压力值
        [ObservableProperty]
        private double _pressure;

        // 辅热温度
        [ObservableProperty]
        private double _auxiliaryHeatTemperature;

        // 工艺状态
        [ObservableProperty]
        private bool _processStatus;

        public HomePageModel()
        {
            _innerTemperatures1 = 1;
            _innerTemperatures2 = 2;
            _innerTemperatures3 = 3;
        }
    }
}
