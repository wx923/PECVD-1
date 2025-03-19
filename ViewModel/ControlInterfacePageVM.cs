using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HslCommunication.ModBus;
using MathNet.Numerics.LinearAlgebra.Factorization;
using WpfApp.Services;
using WpfApp4.page.usepage;
using WpfApp4.Services;

namespace WpfApp4.ViewModel
{
     public partial class ControlInterfacePageVM:ObservableObject
    {
        #region 阀门状态
        [ObservableProperty]
        private bool _isValve1On; // 阀门1状态

        [ObservableProperty]
        private bool _isValve2On; // 阀门2状态

        [ObservableProperty]
        private bool _isValve3On; // 阀门3状态

        [ObservableProperty]
        private bool _isValve4On; // 阀门4状态

        [ObservableProperty]
        private bool _isValve5On; // 阀门5状态

        [ObservableProperty]
        private bool _isValve6On; // 阀门6状态

        [ObservableProperty]
        private bool _isValve7On; // 阀门7状态

        [ObservableProperty]
        private bool _isValve8On; // 阀门8状态

        [ObservableProperty]
        private bool _isValve9On; // 阀门9状态

        [ObservableProperty]
        private bool _isValve10On; // 阀门10状态

        [ObservableProperty]
        private bool _isValve11On; // 阀门11状态

        [ObservableProperty]
        private bool _isValve12On; // 阀门12状态

        [ObservableProperty]
        private bool _isValve13On; // 阀门13状态

        [ObservableProperty]
        private bool _isValve20On; // 阀门20状态
        #endregion

        #region 温度设定
        [ObservableProperty]
        private ObservableCollection<int> _setTemperatures = new ObservableCollection<int>(new int[6]); // 设定温度（6个温区）

        [ObservableProperty]
        private ObservableCollection<int> _standbyTemperatures = new ObservableCollection<int>(new int[6]); // 待机温度（6个温区）

        [ObservableProperty]
        private double _pulsePower; // 脉冲功率

        [ObservableProperty]
        private double _dutyCycle; // 占空比

        [ObservableProperty]
        private double _heatingSlope; // 升温斜率

        [ObservableProperty]
        private double _coolingSlope; // 降温斜率

        [ObservableProperty]
        private double _auxiliaryHeatTemperature; // 辅助加热温度
        #endregion

        #region MFC 值
        [ObservableProperty]
        private double _mfc1Value; // MFC1 值

        [ObservableProperty]
        private double _mfc2Value; // MFC2 值

        [ObservableProperty]
        private double _mfc3Value; // MFC3 值

        [ObservableProperty]
        private double _mfc4Value; // MFC4 值
        #endregion

        #region 其他控制值
        [ObservableProperty]
        private double _flowMeterValue; // 流量计值

        [ObservableProperty]
        private double _pressureGaugeValue; // 压力计值

        [ObservableProperty]
        private double _butterflyValveAngle; // 蝶阀角度
        #endregion

        #region 设备状态
        [ObservableProperty]
        private bool _isTempControllerOn; // 温度控制器状态

        [ObservableProperty]
        private bool _isAuxHeatControllerOn; // 辅热控制器状态

        [ObservableProperty]
        private bool _isPumpOn; // 泵状态

        [ObservableProperty]
        private bool _isPurificationFanOn; // 净化风机状态

        [ObservableProperty]
        private bool _isButterflyValveOn; // 蝶阀状态

        [ObservableProperty]
        private bool _isRfOn; // 射频状态

        [ObservableProperty]
        private bool _isFurnaceDoorOn; // 炉门状态
        #endregion


        //当前页面的炉管号
        public int currentTubeNum;

        //是否在工作
        [ObservableProperty]
        public bool _isLocked;

        //数据库连接对象
        ModbusTcpNet _modbusTcp;
        //初始化函数
        public ControlInterfacePageVM(int tubeNum)
        {

            currentTubeNum = tubeNum;
            IsLocked = GlobalMonitoringService.Instance.FurStates[currentTubeNum];
            _modbusTcp = PlcCommunicationService.Instance.ModbusTcpClients[(PlcCommunicationService.PlcType)currentTubeNum];
            GlobalMonitoringService.Instance.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(GlobalMonitoringService.FurStates))
                {
                    IsLocked = GlobalMonitoringService.Instance.FurStates[currentTubeNum];
                }
            };
        }

        #region 命令
        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetTemperature()
        {
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool hasVaildInput = false;
            // 实现设定温度的逻辑
            for (var i = 0; i < 6; i++)
            {
                int temp = SetTemperatures[i];
                if (temp != 0)
                {
                    if (temp < 0 || temp > 1000)
                    {
                        MessageBox.Show($"温区{i + 1}的温度值{temp}不符合规定值", "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    hasVaildInput = true;
                }
            }

                if (!hasVaildInput)
                {
                    MessageBox.Show("需要输入至少一个合法值", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
                }
                try
                {
                    for(var i=0;i<6; i++)
                    {
                        await _modbusTcp.WriteAsync($"{100 + i}", SetTemperatures[i]);
                    }
                    MessageBox.Show("温度数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"温度数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetStandbyTemperature()
        {
            // 实现设定待机温度的逻辑
            bool hasVaildInput = false;
            // 实现设定温度的逻辑
            for (var i = 0; i < 6; i++)
            {
                int temp = StandbyTemperatures[i];
                if (temp != 0)
                {
                    if (temp < 0 || temp > 1000)
                    {
                        MessageBox.Show($"温区{i + 1}的待机温度值{temp}不符合规定值", "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    hasVaildInput = true;
                }
            }

            if (!hasVaildInput)
            {
                MessageBox.Show("需要输入至少一个合法值", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                for (var i = 0; i < 6; i++)
                {
                    await _modbusTcp.WriteAsync($"{100 + i}", StandbyTemperatures[i]);
                }
                MessageBox.Show("待机温度数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"待机温度数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetMfc1Value()
        {
            // 实现设定 MFC1 值的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Mfc1Value < 0 || Mfc1Value > 100000)
            {
                MessageBox.Show($"MFC1 值 {Mfc1Value} 超出范围（0-100000）！", "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //下发ModbusTCP数据
            try
            {
                await _modbusTcp.WriteAsync("200", (float)Mfc1Value);
                MessageBox.Show("MFC1 数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"MFC1 数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 为所有命令添加 CanExecute
        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetMfc2Value()
        {
            // 实现设定 MFC2 值的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Mfc2Value < 0 || Mfc2Value > 100000)
            {
                MessageBox.Show($"MFC2 值 {Mfc2Value} 超出范围（0-100000）！", "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //下发ModbusTCP数据
            try
            {
                await _modbusTcp.WriteAsync("200", (float)Mfc2Value);
                MessageBox.Show("MFC2 数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MFC2 数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetMfc3Value()
        {
            // 实现设定 MFC3 值的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Mfc3Value < 0 || Mfc3Value > 100000)
            {
                MessageBox.Show($"MFC3 值 {Mfc3Value} 超出范围（0-100000）！", "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //下发ModbusTCP数据
            try
            {
                await _modbusTcp.WriteAsync("200", (float)Mfc3Value);
                MessageBox.Show("MFC3 数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MFC3 数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetMfc4Value()
        {
            // 实现设定 MFC4 值的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (Mfc4Value < 0 || Mfc4Value > 100000)
            {
                MessageBox.Show($"MFC4 值 {Mfc4Value} 超出范围（0-100000）！", "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //下发ModbusTCP数据
            try
            {
                await _modbusTcp.WriteAsync("200", (float)Mfc4Value);
                MessageBox.Show("MFC1 数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MFC1 数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetPressureGaugeValue()
        {
            // 实现设定压力计值的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 检查压力值是否在真空到大气压范围内（0 到 101325 Pa）
            const double atmosphericPressure = 101325.0; // 大气压，单位：Pa
            if (PressureGaugeValue < 0 || PressureGaugeValue > atmosphericPressure)
            {
                MessageBox.Show($"压力值 {PressureGaugeValue} Pa 超出范围（0 到 {atmosphericPressure} Pa）！",
                               "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // 通过 Modbus TCP 下发数据，假设写入寄存器地址 200
                await _modbusTcp.WriteAsync("200", (float)PressureGaugeValue); // 根据实际需求调整类型和地址
                MessageBox.Show("压力数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"压力数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetButterflyValveAngle()
        {
            // 实现设定蝶阀角度的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 检查蝶阀角度是否在 0-90 度范围内
            const double maxAngle = 100.0; // 最大角度
            if (ButterflyValveAngle < 0 || ButterflyValveAngle > maxAngle)
            {
                MessageBox.Show($"蝶阀角度 {ButterflyValveAngle}° 超出范围（0 到 {maxAngle}°）！",
                               "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // 通过 Modbus TCP 下发数据，假设写入寄存器地址 200
                await _modbusTcp.WriteAsync("200", (float)ButterflyValveAngle); // 根据实际需求调整类型和地址
                MessageBox.Show("蝶阀角度数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"蝶阀角度数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void SetRfValues()
        {
            // 实现设定射频值的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void SetTemperatureSlope()
        {
            // 实现设定温度斜率的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void SetAuxiliaryHeatValue()
        {
            // 实现设定辅助加热值的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ToggleTempController()
        {
            IsTempControllerOn = !IsTempControllerOn;
        }

        // 其他命令同样添加 CanExecuteWhenUnlocked
        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ToggleAuxHeatController() => IsAuxHeatControllerOn = !IsAuxHeatControllerOn;

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void TogglePump() => IsPumpOn = !IsPumpOn;

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ToggleFurnaceDoor() => IsFurnaceDoorOn = !IsFurnaceDoorOn;

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void TogglePurificationFan() => IsPurificationFanOn = !IsPurificationFanOn;

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ToggleButterflyValve() => IsButterflyValveOn = !IsButterflyValveOn;

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ToggleRf() => IsRfOn = !IsRfOn;

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ScrubberConfirmRecovery()
        {
            // 实现 Scrubber 确认恢复的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ConfirmExhaustTempRecovery()
        {
            // 实现确认尾排温度恢复的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ManualCutExhaustSilane()
        {
            // 实现手动切尾排硅烷的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void StartScrubberPipeCleaning()
        {
            // 实现启动 Scrubber 管道清洗的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void StopScrubberPipeCleaning()
        {
            // 实现停止 Scrubber 管道清洗的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ConfirmZoneCurrentRecovery()
        {
            // 实现确认温区电流恢复的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void CleanFlowMeter()
        {
            // 实现清洗流量计的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void CleanFurnaceTube()
        {
            // 实现清洗炉管的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void OpenPrePumpValve()
        {
            // 实现打开预抽阀的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void OpenMainPumpValve()
        {
            // 实现打开主抽阀的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void ClosePrePumpValve()
        {
            // 实现关闭预抽阀的逻辑
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private void CloseMainPumpValve()
        {
            // 实现关闭主抽阀的逻辑
        }

        private bool CanExecuteWhenUnlocked(){
            return !IsLocked;
        } // 当 IsLocked 为 false 时允许执行
        #endregion
    }
}
