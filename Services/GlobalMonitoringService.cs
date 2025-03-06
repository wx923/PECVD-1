using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Inovance;
using NPOI.SS.Formula.Functions;
using WpfApp.Services;
using WpfApp4.Models;
using WpfApp4.Services;

namespace WpfApp4.Services
{
    class GlobalMonitoringService
    {
        //创建全局单例服务
        private static readonly Lazy<GlobalMonitoringService> _instance=new Lazy<GlobalMonitoringService> (new GlobalMonitoringService());
        public static GlobalMonitoringService Instance=_instance.Value;

        //创建全局的六炉管全局监控数组
        public ObservableCollection<GlobalMonitoringDataModel> GlobalMonitoringAllData = new ObservableCollection<GlobalMonitoringDataModel>();

        //六个炉管的PLC对象
        Dictionary<int,ModbusTcpNet> _modbusClients=new Dictionary<int, ModbusTcpNet>();


        //创建六个任务管理字典
        Dictionary<int,CancellationTokenSource> _cancellationTokenSources=new Dictionary<int,CancellationTokenSource>();


        //任务管理
        GlobalMonitoringService()
        {
            // 初始化6个炉管的PLC客户端和数据对象
            for (int i = 0; i < 6; i++)
            {
                _modbusClients[i] = PlcCommunicationService.Instance.ModbusTcpClients[(PlcCommunicationService.PlcType)i];
                GlobalMonitoringAllData.Add(new GlobalMonitoringDataModel());
            }

            //当PLC连接了之后才能进行数据更新操作
            PlcCommunicationService.Instance.ConnectionStateChanged += OnPlcConnectionStatusChanged;
        }
        //当连接了之后开始进行相应的PLC操作
        private void OnPlcConnectionStatusChanged(object? sender, (PlcCommunicationService.PlcType PlcType, bool IsConnected) e)
        {
            // 只处理炉管PLC的连接状态（PlcType 0-5）
            if ((int)e.PlcType < 6 && e.IsConnected)
            {
                _ = StartDataUpdate((int)e.PlcType);
            }
        }


        //开始进行数据更新操作
        private object StartDataUpdate(int furnaceIndex)
        {
           if(_cancellationTokenSources.TryGetValue(furnaceIndex, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
            }
           cts=new CancellationTokenSource();
            _cancellationTokenSources.Add(furnaceIndex, cts);

            //创建进程开始进行数据的获取
            Task.Run(async () =>
            {
                while (!_cancellationTokenSources[furnaceIndex].Token.IsCancellationRequested)
                {
                    try 
                    {
                        await UpdatePlcDataAsync(furnaceIndex);
                    }
                    catch (Exception)
                    {

                    }
                }
            });

            return Task.CompletedTask;
        }


        //获取数据操作
        private async Task UpdatePlcDataAsync(int furnaceIndex)
        {

            var data = new GlobalMonitoringDataModel();
            int addr = 1;//从基础地址开始设定
            var client=_modbusClients[furnaceIndex];
                         // 读取阀门状态（假设为线圈数据）
            data.ValveV1 = client.ReadCoil($"{addr++}").Content ;
            data.ValveV2 = client.ReadCoil($"{addr++}").Content ;
            data.ValveV3 = client.ReadCoil($"{addr++}").Content;
            data.ValveV4 = client.ReadCoil($"{addr++}").Content;
            data.ValveV5 = client.ReadCoil($"{addr++}").Content;
            data.ValveV6 = client.ReadCoil($"{addr++}").Content;
            data.ValveV7 = client.ReadCoil($"{addr++}").Content;
            data.ValveV8 = client.ReadCoil($"{addr++}").Content;
            data.ValveV9 = client.ReadCoil($"{addr++}").Content;
            data.ValveV10 = client.ReadCoil($"{addr++}").Content;
            data.ValveV11 = client.ReadCoil($"{addr++}").Content;
            data.ValveV12 = client.ReadCoil($"{addr++}").Content;
            data.ValveV13 = client.ReadCoil($"{addr++}").Content;
            data.ValveV20 = client.ReadCoil($"{addr++}").Content;

            // 读取其他数据（假设为寄存器数据）
            data.Mfc1Setpoint = client.ReadFloat($"{addr++}").Content;
            data.Mfc1ActualValue = client.ReadFloat($"{addr++}").Content;
            data.Mfc2Setpoint = client.ReadFloat($"{addr++}").Content;
            data.Mfc2ActualValue = client.ReadFloat($"{addr++}").Content;
            data.Mfc3Setpoint = client.ReadFloat($"{addr++}").Content;
            data.Mfc3ActualValue = client.ReadFloat($"{addr++}").Content;
            data.Mfc4Setpoint = client.ReadFloat($"{addr++}").Content;
            data.Mfc4ActualValue = client.ReadFloat($"{addr++}").Content;

            data.VacuumGaugeActualPressure = client.ReadDouble($"{addr++}").Content;
            data.VacuumGaugeSetPressure = client.ReadDouble($"{addr++}").Content;

            data.ButterflyValveOpening = client.ReadInt16($"{addr++}").Content;

            data.SetpointTemperatureT1 = client.ReadFloat($"{addr++}").Content;
            data.SetpointTemperatureT2 = client.ReadFloat($"{addr++}").Content;
            data.SetpointTemperatureT3 = client.ReadFloat($"{addr++}").Content;
            data.SetpointTemperatureT4 = client.ReadFloat($"{addr++}").Content;
            data.SetpointTemperatureT5 = client.ReadFloat($"{addr++}").Content;
            data.SetpointTemperatureT6 = client.ReadFloat($"{addr++}").Content;

            data.ThermocoupleInternalT1 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleInternalT2 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleInternalT3 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleInternalT4 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleInternalT5 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleInternalT6 = client.ReadFloat($"{addr++}").Content;

            data.ThermocoupleExternalT1 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleExternalT2 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleExternalT3 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleExternalT4 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleExternalT5 = client.ReadFloat($"{addr++}").Content;
            data.ThermocoupleExternalT6 = client.ReadFloat($"{addr++}").Content;

            data.AuxiliaryHeatingACurrent = client.ReadFloat($"{addr++}").Content;
            data.AuxiliaryHeatingAVoltage = client.ReadFloat($"{addr++}").Content;
            data.AuxiliaryHeatingBCurrent = client.ReadFloat($"{addr++}").Content;
            data.AuxiliaryHeatingBVoltage = client.ReadFloat($"{addr++}").Content;
            data.AuxiliaryHeatingCCurrent = client.ReadFloat($"{addr++}").Content;
            data.AuxiliaryHeatingCVoltage = client.ReadFloat($"{addr++}").Content;

            data.RfVoltage = client.ReadFloat($"{addr++}").Content;
            data.RfCurrent = client.ReadFloat($"{addr++}").Content;
            data.RfPower = client.ReadFloat($"{addr++}").Content;

            data.AuxiliaryHeatingValveOpening = client.ReadInt16($"{addr++}").Content;
            data.AuxiliaryHeatingActualTemperature = client.ReadInt16($"{addr++}").Content;
            data.AuxiliaryHeatingSetTemperature = client.ReadInt16($"{addr++}").Content;
            data.AuxiliaryHeatingWireCurrent = client.ReadInt16($"{addr++}").Content;
            Application.Current.Dispatcher.Invoke(() =>
            {
                GlobalMonitoringAllData[furnaceIndex].ValveV1 = data.ValveV1;
                GlobalMonitoringAllData[furnaceIndex].ValveV2 = data.ValveV2;
                GlobalMonitoringAllData[furnaceIndex].ValveV3 = data.ValveV3;
                GlobalMonitoringAllData[furnaceIndex].ValveV4 = data.ValveV4;
                GlobalMonitoringAllData[furnaceIndex].ValveV5 = data.ValveV5;
                GlobalMonitoringAllData[furnaceIndex].ValveV6 = data.ValveV6;
                GlobalMonitoringAllData[furnaceIndex].ValveV7 = data.ValveV7;
                GlobalMonitoringAllData[furnaceIndex].ValveV8 = data.ValveV8;
                GlobalMonitoringAllData[furnaceIndex].ValveV9 = data.ValveV9;
                GlobalMonitoringAllData[furnaceIndex].ValveV10 = data.ValveV10;
                GlobalMonitoringAllData[furnaceIndex].ValveV11 = data.ValveV11;
                GlobalMonitoringAllData[furnaceIndex].ValveV12 = data.ValveV12;
                GlobalMonitoringAllData[furnaceIndex].ValveV13 = data.ValveV13;
                GlobalMonitoringAllData[furnaceIndex].ValveV20 = data.ValveV20;


                GlobalMonitoringAllData[furnaceIndex].Mfc1Setpoint = data.Mfc1Setpoint;
                GlobalMonitoringAllData[furnaceIndex].Mfc2Setpoint = data.Mfc2Setpoint;
                GlobalMonitoringAllData[furnaceIndex].Mfc3Setpoint = data.Mfc3Setpoint;
                GlobalMonitoringAllData[furnaceIndex].Mfc4Setpoint = data.Mfc4Setpoint;

                GlobalMonitoringAllData[furnaceIndex].Mfc1ActualValue = data.Mfc1ActualValue;
                GlobalMonitoringAllData[furnaceIndex].Mfc2ActualValue = data.Mfc2ActualValue;
                GlobalMonitoringAllData[furnaceIndex].Mfc3ActualValue = data.Mfc3ActualValue;
                GlobalMonitoringAllData[furnaceIndex].Mfc4ActualValue = data.Mfc4ActualValue;


                GlobalMonitoringAllData[furnaceIndex].VacuumGaugeActualPressure = data.Mfc4ActualValue;
                GlobalMonitoringAllData[furnaceIndex].VacuumGaugeSetPressure = data.Mfc4ActualValue;


                GlobalMonitoringAllData[furnaceIndex].ButterflyValveOpening = data.ButterflyValveOpening;

                GlobalMonitoringAllData[furnaceIndex].SetpointTemperatureT1 = data.SetpointTemperatureT1;
                GlobalMonitoringAllData[furnaceIndex].SetpointTemperatureT2 = data.SetpointTemperatureT2;
                GlobalMonitoringAllData[furnaceIndex].SetpointTemperatureT3 = data.SetpointTemperatureT3;
                GlobalMonitoringAllData[furnaceIndex].SetpointTemperatureT4 = data.SetpointTemperatureT4;
                GlobalMonitoringAllData[furnaceIndex].SetpointTemperatureT5 = data.SetpointTemperatureT5;
                GlobalMonitoringAllData[furnaceIndex].SetpointTemperatureT6 = data.SetpointTemperatureT6;

                GlobalMonitoringAllData[furnaceIndex].ThermocoupleInternalT1 = data.ThermocoupleInternalT1;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleInternalT2 = data.ThermocoupleInternalT2;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleInternalT3 = data.ThermocoupleInternalT3;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleInternalT4 = data.ThermocoupleInternalT4;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleInternalT5 = data.ThermocoupleInternalT5;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleInternalT6 = data.ThermocoupleInternalT6;

                GlobalMonitoringAllData[furnaceIndex].ThermocoupleExternalT1 = data.ThermocoupleExternalT1;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleExternalT2 = data.ThermocoupleExternalT2;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleExternalT3 = data.ThermocoupleExternalT3;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleExternalT4 = data.ThermocoupleExternalT4;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleExternalT5 = data.ThermocoupleExternalT5;
                GlobalMonitoringAllData[furnaceIndex].ThermocoupleExternalT6 = data.ThermocoupleExternalT6;


                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingACurrent = data.AuxiliaryHeatingACurrent;
                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingAVoltage = data.AuxiliaryHeatingAVoltage;
                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingBCurrent = data.AuxiliaryHeatingBCurrent;
                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingBVoltage = data.AuxiliaryHeatingBVoltage;
                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingCCurrent = data.AuxiliaryHeatingCCurrent;
                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingCVoltage = data.AuxiliaryHeatingCVoltage;


                GlobalMonitoringAllData[furnaceIndex].RfVoltage = data.RfVoltage;
                GlobalMonitoringAllData[furnaceIndex].RfCurrent = data.RfCurrent;
                GlobalMonitoringAllData[furnaceIndex].RfPower = data.RfPower;


                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingValveOpening = data.AuxiliaryHeatingValveOpening;
                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingActualTemperature = data.AuxiliaryHeatingActualTemperature;
                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingSetTemperature = data.AuxiliaryHeatingSetTemperature;
                GlobalMonitoringAllData[furnaceIndex].AuxiliaryHeatingWireCurrent = data.AuxiliaryHeatingWireCurrent;
            });

            // 每隔一段时间更新一次数据
            await Task.Delay(1000); // 每秒更新一次


        }
    }
}
