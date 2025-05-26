using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp.Services;
using WpfApp4.Models;
using WpfApp4.Services;

namespace WpfApp4.ViewModel
{
    public partial class HomePageViewModel : ObservableObject
    {
        private Dictionary<int, bool> _furnaceConnectionStatus;
        private Dictionary<int, HomePageModel> _furnaceData;
        public Dictionary<int, bool> FurnaceConnectionStatus
        {
            get => _furnaceConnectionStatus;
            private set => SetProperty(ref _furnaceConnectionStatus, value); // 可通知属性
        }

        public Dictionary<int, HomePageModel> FurnaceData
        {
            get => _furnaceData;
            private set => SetProperty(ref _furnaceData, value);
        }

        public HomePageViewModel()
        {
            // 初始化
            _furnaceConnectionStatus = new Dictionary<int, bool>();
            _furnaceData = HomePageService.Instance._viewModel;
            SyncFurnaceStatus();

            // 订阅事件
            PlcCommunicationService.Instance.ConnectionStateChanged += OnPlcConnectionStateChanged;
        }

        private void SyncFurnaceStatus()
        {
            var states = PlcCommunicationService.Instance.ConnectionStates;
            var newStatus = new Dictionary<int, bool>();
            for (int i = 0; i < 6; i++)
            {
                newStatus[i] = states[(PlcCommunicationService.PlcType)i];
            }
            FurnaceConnectionStatus = newStatus; // 赋值新字典，触发通知
        }

        private void OnPlcConnectionStateChanged(object sender, (PlcCommunicationService.PlcType PlcType, bool IsConnected) e)
        {
            int plcIndex = (int)e.PlcType;
            if (plcIndex < 6) // 只处理炉管 PLC (0-5)
            {
                _furnaceConnectionStatus[plcIndex] = e.IsConnected;
                // 这里可以选择不直接赋值新字典，而是手动通知
                OnPropertyChanged(nameof(FurnaceConnectionStatus));
            }
        }

        [RelayCommand]
        private void ToggleFurnaceStatus(int furnaceIndex)
        {
            _furnaceConnectionStatus[furnaceIndex] = true;
            FurnaceConnectionStatus = new Dictionary<int, bool>(_furnaceConnectionStatus); // 强制替换引用
            MessageBox.Show($"炉管 {furnaceIndex} 状态已切换为: {_furnaceConnectionStatus[furnaceIndex]}");
        }

    }
} 