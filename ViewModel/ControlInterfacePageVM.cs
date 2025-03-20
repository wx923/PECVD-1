using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HslCommunication.ModBus;
using MathNet.Numerics.LinearAlgebra.Factorization;
using Org.BouncyCastle.Asn1.Cms;
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

        //创建定时器对象
        private readonly Timer _timerState;
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
            _timerState = new Timer(1000);
            _timerState.Elapsed += async (s,e) => { await UpdateDeviceStatusAsync(); };
            _timerState.AutoReset = true;
            _timerState.Start();
        }


        #region 批量读取数据方法
        private async Task UpdateDeviceStatusAsync()
        {
            try
            {
                // 读取 Modbus 线圈数据（假设地址从 200 开始）
                var tempController = (await _modbusTcp.ReadCoilAsync("200")).Content; // 温度控制器
                var auxHeatController = (await _modbusTcp.ReadCoilAsync("201")).Content; // 辅热控制器
                var pump = (await _modbusTcp.ReadCoilAsync("202")).Content; // 泵
                var purificationFan = (await _modbusTcp.ReadCoilAsync("203")).Content; // 净化风机
                var butterflyValve = (await _modbusTcp.ReadCoilAsync("204")).Content; // 蝶阀
                var rf = (await _modbusTcp.ReadCoilAsync("205")).Content; // 射频
                var furnaceDoor = (await _modbusTcp.ReadCoilAsync("206")).Content; // 炉门

                // 在 UI 线程上更新属性
                Application.Current.Dispatcher.Invoke(() =>
                {
                    IsTempControllerOn = tempController;
                    IsAuxHeatControllerOn = auxHeatController;
                    IsPumpOn = pump;
                    IsPurificationFanOn = purificationFan;
                    IsButterflyValveOn = butterflyValve;
                    IsRfOn = rf;
                    IsFurnaceDoorOn = furnaceDoor;
                });
            }
            catch(Exception ex)
            {
                // 处理读取失败的情况，记录日志
                System.Diagnostics.Debug.WriteLine($"设备状态更新失败：{ex.Message}");
            }
        }
        #endregion

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

            // 检查蝶阀角度是否在 0-100 度范围内
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
        private async void SetRfValues()
        {
            // 实现设定射频值的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 检查 PulsePower 是否在 0-1000 W 范围内
            const double maxPulsePower = 1000.0; // 最大脉冲功率
            if (PulsePower < 0 || PulsePower > maxPulsePower)
            {
                MessageBox.Show($"脉冲功率 {PulsePower} W 超出范围（0 到 {maxPulsePower} W）！",
                               "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 检查 DutyCycle 是否在 0-100% 范围内
            const double maxDutyCycle = 100.0; // 最大占空比
            if (DutyCycle < 0 || DutyCycle > maxDutyCycle)
            {
                MessageBox.Show($"占空比 {DutyCycle}% 超出范围（0 到 {maxDutyCycle}%）！",
                               "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // 通过 Modbus TCP 下发数据
                await _modbusTcp.WriteAsync("200", (float)PulsePower); // 写入脉冲功率，地址 230
                await _modbusTcp.WriteAsync("200", (float)DutyCycle);  // 写入占空比，地址 231
                MessageBox.Show("射频参数数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"射频参数数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetTemperatureSlope()
        {
            // 实现设定温度斜率的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 检查 HeatingSlope 是否在 0-20 °C/min 范围内
            const double maxSlope = 20.0; // 最大斜率
            if (HeatingSlope < 0 || HeatingSlope > maxSlope)
            {
                MessageBox.Show($"升温斜率 {HeatingSlope} °C/min 超出范围（0 到 {maxSlope} °C/min）！",
                               "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 检查 CoolingSlope 是否在 0-20 °C/min 范围内
            if (CoolingSlope < 0 || CoolingSlope > maxSlope)
            {
                MessageBox.Show($"降温斜率 {CoolingSlope} °C/min 超出范围（0 到 {maxSlope} °C/min）！",
                               "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // 通过 Modbus TCP 下发数据
                await _modbusTcp.WriteAsync("200", (float)HeatingSlope); // 写入升温斜率，地址 200
                await _modbusTcp.WriteAsync("200", (float)CoolingSlope); // 写入降温斜率，地址 200
                MessageBox.Show("温度斜率数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"温度斜率数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async void SetAuxiliaryHeatValue()
        {
            // 实现设定辅助加热值的逻辑
            if (IsLocked)
            {
                MessageBox.Show("当前炉管正在工艺，无法进行手动控制", "失败", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // 检查 AuxiliaryHeatTemperature 是否在 0-1000 °C 范围内
            const double maxAuxHeatTemp = 1000.0; // 最大辅助加热温度
            if (AuxiliaryHeatTemperature < 0 || AuxiliaryHeatTemperature > maxAuxHeatTemp)
            {
                MessageBox.Show($"辅助加热温度 {AuxiliaryHeatTemperature} °C 超出范围（0 到 {maxAuxHeatTemp} °C）！",
                               "输入值错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // 通过 Modbus TCP 下发数据，假设写入寄存器地址 200
                await _modbusTcp.WriteAsync("200", (float)AuxiliaryHeatTemperature); // 根据实际需求调整类型和地址
                MessageBox.Show("辅助加热温度数据下发成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"辅助加热温度数据下发失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


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

        #region 阀门切换命令
        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve1Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("100", IsValve1On ? (short)1 : (short)0); // V1 地址 100
                MessageBox.Show($"阀门 V1 已切换到 {(IsValve1On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve1On = !IsValve1On; // 失败时回滚状态
                MessageBox.Show($"阀门 V1 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve2Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("101", IsValve2On ? (short)1 : (short)0); // V2 地址 101
                MessageBox.Show($"阀门 V2 已切换到 {(IsValve2On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve2On = !IsValve2On;
                MessageBox.Show($"阀门 V2 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve3Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("102", IsValve3On ? (short)1 : (short)0); // V3 地址 102
                MessageBox.Show($"阀门 V3 已切换到 {(IsValve3On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve3On = !IsValve3On;
                MessageBox.Show($"阀门 V3 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve4Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("103", IsValve4On ? (short)1 : (short)0); // V4 地址 103
                MessageBox.Show($"阀门 V4 已切换到 {(IsValve4On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve4On = !IsValve4On;
                MessageBox.Show($"阀门 V4 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve5Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("104", IsValve5On ? (short)1 : (short)0); // V5 地址 104
                MessageBox.Show($"阀门 V5 已切换到 {(IsValve5On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve5On = !IsValve5On;
                MessageBox.Show($"阀门 V5 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve6Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("105", IsValve6On ? (short)1 : (short)0); // V6 地址 105
                MessageBox.Show($"阀门 V6 已切换到 {(IsValve6On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve6On = !IsValve6On;
                MessageBox.Show($"阀门 V6 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve7Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("106", IsValve7On ? (short)1 : (short)0); // V7 地址 106
                MessageBox.Show($"阀门 V7 已切换到 {(IsValve7On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve7On = !IsValve7On;
                MessageBox.Show($"阀门 V7 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve8Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("107", IsValve8On ? (short)1 : (short)0); // V8 地址 107
                MessageBox.Show($"阀门 V8 已切换到 {(IsValve8On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve8On = !IsValve8On;
                MessageBox.Show($"阀门 V8 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve9Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("108", IsValve9On ? (short)1 : (short)0); // V9 地址 108
                MessageBox.Show($"阀门 V9 已切换到 {(IsValve9On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve9On = !IsValve9On;
                MessageBox.Show($"阀门 V9 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve10Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("109", IsValve10On ? (short)1 : (short)0); // V10 地址 109
                MessageBox.Show($"阀门 V10 已切换到 {(IsValve10On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve10On = !IsValve10On;
                MessageBox.Show($"阀门 V10 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve11Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("110", IsValve11On ? (short)1 : (short)0); // V11 地址 110
                MessageBox.Show($"阀门 V11 已切换到 {(IsValve11On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve11On = !IsValve11On;
                MessageBox.Show($"阀门 V11 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve12Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("111", IsValve12On ? (short)1 : (short)0); // V12 地址 111
                MessageBox.Show($"阀门 V12 已切换到 {(IsValve12On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve12On = !IsValve12On;
                MessageBox.Show($"阀门 V12 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve13Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("112", IsValve13On ? (short)1 : (short)0); // V13 地址 112
                MessageBox.Show($"阀门 V13 已切换到 {(IsValve13On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve13On = !IsValve13On;
                MessageBox.Show($"阀门 V13 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleValve20Async()
        {
            try
            {
                await _modbusTcp.WriteAsync("119", IsValve20On ? (short)1 : (short)0); // V20 地址 119
                MessageBox.Show($"阀门 V20 已切换到 {(IsValve20On ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                IsValve20On = !IsValve20On;
                MessageBox.Show($"阀门 V20 切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region 判断逻辑（预留区域）
        private bool CanToggleTempController()
        {
            // 温控器开启条件
            return true; 
        }

        private bool CanToggleAuxHeatController()
        {
            // 辅热控制器开启条件
            return true; 
        }

        private bool CanTogglePump()
        {
            // 泵开启条件
            return true; 
        }

        private bool CanTogglePurificationFan()
        {
            // 净化风机开启条件
            return true; 
        }

        private bool CanToggleButterflyValve()
        {
            // 蝶阀开启条件
            return true;
        }

        private bool CanToggleRf()
        {
            // 射频开启条件
            return true;
        }

        private bool CanToggleFurnaceDoor()
        {
            //炉门开启条件
            return true;
        }
        #endregion

        #region 设备状态切换命令
        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleTempController()
        {
            // 判断是否可以操作（预留区域）
            if (!CanToggleTempController())
            {
                MessageBox.Show("当前无法操作温度控制器", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool newState = !IsTempControllerOn; // 目标状态
            try
            {
                // 通过 Modbus TCP 下发命令（假设地址 200）
                await _modbusTcp.WriteAsync("200", newState);
                // 下发成功，更新状态
                IsTempControllerOn = newState;
                MessageBox.Show($"温度控制器已切换到 {(newState ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                // 下发失败，保持原状态并提示
                MessageBox.Show($"温度控制器切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleAuxHeatController()
        {
            if (!CanToggleAuxHeatController())
            {
                MessageBox.Show("当前无法操作辅热控制器", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool newState = !IsAuxHeatControllerOn;
            try
            {
                await _modbusTcp.WriteAsync("201", newState);
                IsAuxHeatControllerOn = newState;
                MessageBox.Show($"辅热控制器已切换到 {(newState ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"辅热控制器切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task TogglePump()
        {
            if (!CanTogglePump())
            {
                MessageBox.Show("当前无法操作泵", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool newState = !IsPumpOn;
            try
            {
                await _modbusTcp.WriteAsync("202", newState);
                IsPumpOn = newState;
                MessageBox.Show($"泵已切换到 {(newState ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"泵切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task TogglePurificationFan()
        {
            if (!CanTogglePurificationFan())
            {
                MessageBox.Show("当前无法操作净化风机", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool newState = !IsPurificationFanOn;
            try
            {
                await _modbusTcp.WriteAsync("203", newState);
                IsPurificationFanOn = newState;
                MessageBox.Show($"净化风机已切换到 {(newState ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"净化风机切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleButterflyValve()
        {
            if (!CanToggleButterflyValve())
            {
                MessageBox.Show("当前无法操作蝶阀", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool newState = !IsButterflyValveOn;
            try
            {
                await _modbusTcp.WriteAsync("204", newState);
                IsButterflyValveOn = newState;
                MessageBox.Show($"蝶阀已切换到 {(newState ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"蝶阀切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleRf()
        {
            if (!CanToggleRf())
            {
                MessageBox.Show("当前无法操作射频", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool newState = !IsRfOn;
            try
            {
                await _modbusTcp.WriteAsync("205", newState);
                IsRfOn = newState;
                MessageBox.Show($"射频已切换到 {(newState ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"射频切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand(CanExecute = nameof(CanExecuteWhenUnlocked))]
        private async Task ToggleFurnaceDoor()
        {
            if (!CanToggleFurnaceDoor())
            {
                MessageBox.Show("当前无法操作炉门", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool newState = !IsFurnaceDoorOn;
            try
            {
                await _modbusTcp.WriteAsync("206", newState);
                IsFurnaceDoorOn = newState;
                MessageBox.Show($"炉门已切换到 {(newState ? "开" : "关")}", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"炉门切换失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region 清除资源
        public void Dispose()
        {
            _timerState.Stop();
            _timerState.Dispose();
        }
        #endregion
    }
}
