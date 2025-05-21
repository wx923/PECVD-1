using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp4.ViewModel
{
    public partial class GlobalMonitoringViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TemperatureData> _temperatureData;

        public GlobalMonitoringViewModel()
        {
            InitializeTemperatureData();
        }

        private void InitializeTemperatureData()
        {
            TemperatureData = new ObservableCollection<TemperatureData>
            {
                new TemperatureData { ParameterName = "温度" },
                new TemperatureData { ParameterName = "外偶" },
                new TemperatureData { ParameterName = "内偶" },
                new TemperatureData { ParameterName = "SV" },
                new TemperatureData { ParameterName = "电流" }
            };
        }
    }

    public class TemperatureData : ObservableObject
    {
        private string _parameterName;
        public string ParameterName
        {
            get => _parameterName;
            set => SetProperty(ref _parameterName, value);
        }

        private string _t1Value;
        public string T1Value
        {
            get => _t1Value;
            set => SetProperty(ref _t1Value, value);
        }

        private string _t2Value;
        public string T2Value
        {
            get => _t2Value;
            set => SetProperty(ref _t2Value, value);
        }

        private string _t3Value;
        public string T3Value
        {
            get => _t3Value;
            set => SetProperty(ref _t3Value, value);
        }

        private string _t4Value;
        public string T4Value
        {
            get => _t4Value;
            set => SetProperty(ref _t4Value, value);
        }

        private string _t5Value;
        public string T5Value
        {
            get => _t5Value;
            set => SetProperty(ref _t5Value, value);
        }

        private string _t6Value;
        public string T6Value
        {
            get => _t6Value;
            set => SetProperty(ref _t6Value, value);
        }

        private string _t7Value;
        public string T7Value
        {
            get => _t7Value;
            set => SetProperty(ref _t7Value, value);
        }

        private string _t8Value;
        public string T8Value
        {
            get => _t8Value;
            set => SetProperty(ref _t8Value, value);
        }

        private string _t9Value;
        public string T9Value
        {
            get => _t9Value;
            set => SetProperty(ref _t9Value, value);
        }
    }
} 