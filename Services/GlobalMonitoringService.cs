using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using HslCommunication.ModBus;
using HslCommunication.Profinet.Inovance;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using WpfApp.Services;
using WpfApp4.Models;
using WpfApp4.Services;

namespace WpfApp4.Services
{
    public partial class  GlobalMonitoringService: ObservableObject
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

        //六个炉管的监控数据采集对象
        public ObservableCollection<RegularCollectDataModel>[] _plcDataExcelLists=new ObservableCollection<RegularCollectDataModel>[6];
        private DispatcherTimer[] _plcExcelTimers = new DispatcherTimer[6];
        //任务管理

        // 每个炉管的暂停状态
        private bool[] _isPaused = new bool[6];

        //管理六个炉管的是否在工艺
        [ObservableProperty]
        private ObservableCollection<bool> _furStates = new ObservableCollection<bool>(Enumerable.Repeat(false, 6));
        GlobalMonitoringService()
        {
            // 初始化6个炉管的PLC客户端和数据对象
            for (int i = 0; i < 6; i++)
            {
                _modbusClients[i] = PlcCommunicationService.Instance.ModbusTcpClients[(PlcCommunicationService.PlcType)i];
                GlobalMonitoringAllData.Add(new GlobalMonitoringDataModel());

                _plcDataExcelLists[i]=new ObservableCollection<RegularCollectDataModel>();
                _plcExcelTimers[i] = new DispatcherTimer
                {
                    Interval = TimeSpan.FromSeconds(3),
                    Tag=i
                };
                _plcExcelTimers[i].Tick += PlcTimer_Tick;
                _isPaused[i] = false; // 初始化为未暂停
                FurStates[i]=false; //初始化为炉管未工艺的状态
            }

            //当PLC连接了之后才能进行数据更新操作
            PlcCommunicationService.Instance.ConnectionStateChanged += OnPlcConnectionStatusChanged;
        }


        #region 辅助函数
        //当连接了之后开始进行相应的PLC操作
        private void OnPlcConnectionStatusChanged(object? sender, (PlcCommunicationService.PlcType PlcType, bool IsConnected) e)
        {
            // 只处理炉管PLC的连接状态（PlcType 0-5）
            if ((int)e.PlcType < 6 && e.IsConnected)
            {
                _ = StartDataUpdate((int)e.PlcType);
            }
        }

        #endregion



        #region 通过PLC获取六个炉管工艺工程监控数据
        //开始进行数据更新操作
        private object StartDataUpdate(int furnaceIndex)
        {
            if (_cancellationTokenSources.TryGetValue(furnaceIndex, out var cts))
            {
                cts.Cancel();
                cts.Dispose();
            }
            cts = new CancellationTokenSource();
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
            var client = _modbusClients[furnaceIndex];
            // 读取阀门状态（假设为线圈数据）
            data.ValveV1 = client.ReadCoil($"{addr++}").Content;
            data.ValveV2 = client.ReadCoil($"{addr++}").Content;
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
        #endregion

        #region 通过PLC获取六个炉管的工艺监控数据 
        //定时器回调函数
        private async void PlcTimer_Tick(object? sender, EventArgs e)
        {
            var timer = (DispatcherTimer)sender;
            int tag = (int)timer.Tag;
            var data = await ReadPlcDataAsync(tag);
            _plcDataExcelLists[tag].Add(data);
        }

        //读取plc的数据
        private async Task<RegularCollectDataModel> ReadPlcDataAsync(int tag)
        {
            //通过modbusTCP进行采集
            return new RegularCollectDataModel();
        }


        //开始工艺监控数据采集
        internal void StartPlcDataCollection(int tubeNumber, bool resetCollection = true)
        {
            if (tubeNumber < 0 || tubeNumber >= 6) throw new ArgumentOutOfRangeException(nameof(tubeNumber), "炉管编号必须在 0-5 之间");
            if (resetCollection) {
                _plcDataExcelLists[tubeNumber].Clear();
            }
            _isPaused[tubeNumber] = false; // 启动时确保未暂停
            FurStates[tubeNumber] = true;
            OnPropertyChanged(nameof(FurStates));
            if (!_plcExcelTimers[tubeNumber].IsEnabled) _plcExcelTimers[tubeNumber].Start();
        }

        //停止PLC数据采集
        internal void StopPlcDataCollection(int tubeNumber)
        {
            if (tubeNumber < 0 || tubeNumber >= 6) throw new ArgumentOutOfRangeException(nameof(tubeNumber), "炉管编号必须在 0-5 之间");
            if (_plcExcelTimers[tubeNumber].IsEnabled) _plcExcelTimers[tubeNumber].Stop();
            _isPaused[tubeNumber] = false; // 停止时重置暂停状态
        }

        //暂停PLC数据采集
        internal void PausePlcDataCollection(int tubeNumber)
        {
            if (tubeNumber < 0 || tubeNumber >= 6) throw new ArgumentOutOfRangeException(nameof(tubeNumber), "炉管编号必须在 0-5 之间");
            _isPaused[tubeNumber] = true; // 设置暂停状态
            if (_plcExcelTimers[tubeNumber].IsEnabled) _plcExcelTimers[tubeNumber].Stop(); // 暂停定时器
        }

        //恢复plc数据采集
        internal void ResumePlcDataCollection(int tubeNumber)
        {
            if (tubeNumber < 0 || tubeNumber >= 6) throw new ArgumentOutOfRangeException(nameof(tubeNumber), "炉管编号必须在 0-5 之间");
            _isPaused[tubeNumber] = false; // 恢复状态
            if (!_plcExcelTimers[tubeNumber].IsEnabled) _plcExcelTimers[tubeNumber].Start(); // 恢复定时器
        }

        public ObservableCollection<RegularCollectDataModel> GetPlcDataList(int tubeNumber)
        {
            if (tubeNumber < 0 || tubeNumber >= 6) throw new ArgumentOutOfRangeException(nameof(tubeNumber));
            return _plcDataExcelLists[tubeNumber];
        }

        internal async Task ExportPlcDataToExcelAsync(int tubeNum)
        {
            if(tubeNum < 0 || tubeNum >= 6) throw new ArgumentOutOfRangeException(nameof(tubeNum));
            FurStates[tubeNum] = false;
            OnPropertyChanged(nameof(FurStates));
            //配置eeplus许可
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //打开excle文件
            using (var package=new ExcelPackage()) {
                var worksheet = package.Workbook.Worksheets.Add($"Tube {tubeNum + 1} 监控数据");
                // 设置中文表头
                worksheet.Cells[1, 1].Value = "时间戳";
                worksheet.Cells[1, 2].Value = "工艺时间";
                worksheet.Cells[1, 3].Value = "剩余时间";
                worksheet.Cells[1, 4].Value = "步号";
                worksheet.Cells[1, 5].Value = "工艺类型";
                worksheet.Cells[1, 6].Value = "舟号";
                worksheet.Cells[1, 7].Value = "停止原因";
                worksheet.Cells[1, 8].Value = "设定温度区1";
                worksheet.Cells[1, 9].Value = "设定温度区2";
                worksheet.Cells[1, 10].Value = "设定温度区3";
                worksheet.Cells[1, 11].Value = "设定温度区4";
                worksheet.Cells[1, 12].Value = "设定温度区5";
                worksheet.Cells[1, 13].Value = "设定温度区6";
                worksheet.Cells[1, 14].Value = "实际温度设定1";
                worksheet.Cells[1, 15].Value = "实际温度设定2";
                worksheet.Cells[1, 16].Value = "实际温度设定3";
                worksheet.Cells[1, 17].Value = "实际温度设定4";
                worksheet.Cells[1, 18].Value = "实际温度设定5";
                worksheet.Cells[1, 19].Value = "实际温度设定6";
                worksheet.Cells[1, 20].Value = "SetMFC1";
                worksheet.Cells[1, 21].Value = "SetMFC2";
                worksheet.Cells[1, 22].Value = "SetMFC3";
                worksheet.Cells[1, 23].Value = "SetMFC4";
                worksheet.Cells[1, 24].Value = "RealMFC1";
                worksheet.Cells[1, 25].Value = "RealMFC2";
                worksheet.Cells[1, 26].Value = "RealMFC3";
                worksheet.Cells[1, 27].Value = "RealMFC4";
                worksheet.Cells[1, 28].Value = "射频功率设定";
                worksheet.Cells[1, 29].Value = "射频功率实际";
                worksheet.Cells[1, 30].Value = "射频电流";
                worksheet.Cells[1, 31].Value = "射频电压";
                worksheet.Cells[1, 32].Value = "占空比";
                worksheet.Cells[1, 33].Value = "蝶阀角度";
                worksheet.Cells[1, 34].Value = "腔体压力设定";
                worksheet.Cells[1, 35].Value = "腔体压力实际";
                worksheet.Cells[1, 36].Value = "辅热实际功率";
                worksheet.Cells[1, 37].Value = "辅热实际温度";
                worksheet.Cells[1, 38].Value = "辅热设定温度";
                worksheet.Cells[1, 39].Value = "辅热A相电流";
                worksheet.Cells[1, 40].Value = "辅热B相电流";
                worksheet.Cells[1, 41].Value = "辅热C相电流";

                // 写入数据
                var dataList = _plcDataExcelLists[tubeNum];
                for (int i = 0; i < dataList.Count; i++)
                {
                    var row = i + 2;
                    worksheet.Cells[row, 1].Value = dataList[i].Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cells[row, 2].Value = dataList[i].ProcessTime.ToString(@"hh\:mm\:ss");
                    worksheet.Cells[row, 3].Value = dataList[i].RemainingTime.ToString(@"hh\:mm\:ss");
                    worksheet.Cells[row, 4].Value = dataList[i].StepNumber;
                    worksheet.Cells[row, 5].Value = dataList[i].ProcessType;
                    worksheet.Cells[row, 6].Value = dataList[i].BoatNumber;
                    worksheet.Cells[row, 7].Value = dataList[i].StopReason;
                    worksheet.Cells[row, 8].Value = dataList[i].SetTempZone1;
                    worksheet.Cells[row, 9].Value = dataList[i].SetTempZone2;
                    worksheet.Cells[row, 10].Value = dataList[i].SetTempZone3;
                    worksheet.Cells[row, 11].Value = dataList[i].SetTempZone4;
                    worksheet.Cells[row, 12].Value = dataList[i].SetTempZone5;
                    worksheet.Cells[row, 13].Value = dataList[i].SetTempZone6;
                    worksheet.Cells[row, 14].Value = dataList[i].RealTempZone1;
                    worksheet.Cells[row, 15].Value = dataList[i].RealTempZone2;
                    worksheet.Cells[row, 16].Value = dataList[i].RealTempZone3;
                    worksheet.Cells[row, 17].Value = dataList[i].RealTempZone4;
                    worksheet.Cells[row, 18].Value = dataList[i].RealTempZone5;
                    worksheet.Cells[row, 19].Value = dataList[i].RealTempZone6;
                    worksheet.Cells[row, 20].Value = dataList[i].SetMFC1;
                    worksheet.Cells[row, 21].Value = dataList[i].SetMFC2;
                    worksheet.Cells[row, 22].Value = dataList[i].SetMFC3;
                    worksheet.Cells[row, 23].Value = dataList[i].SetMFC4;
                    worksheet.Cells[row, 24].Value = dataList[i].RealMFC1;
                    worksheet.Cells[row, 25].Value = dataList[i].RealMFC2;
                    worksheet.Cells[row, 26].Value = dataList[i].RealMFC3;
                    worksheet.Cells[row, 27].Value = dataList[i].RealMFC4;
                    worksheet.Cells[row, 28].Value = dataList[i].RfPowerSet;
                    worksheet.Cells[row, 29].Value = dataList[i].RfPowerActual;
                    worksheet.Cells[row, 30].Value = dataList[i].RfCurrent;
                    worksheet.Cells[row, 31].Value = dataList[i].RfVoltage;
                    worksheet.Cells[row, 32].Value = dataList[i].DutyCycle;
                    worksheet.Cells[row, 33].Value = dataList[i].ButterflyValveAngle;
                    worksheet.Cells[row, 34].Value = dataList[i].ChamberPressureSet;
                    worksheet.Cells[row, 35].Value = dataList[i].ChamberPressureActual;
                    worksheet.Cells[row, 36].Value = dataList[i].AuxHeatPowerActual;
                    worksheet.Cells[row, 37].Value = dataList[i].AuxHeatTempActual;
                    worksheet.Cells[row, 38].Value = dataList[i].AuxHeatTempSet;
                    worksheet.Cells[row, 39].Value = dataList[i].AuxHeatCurrentA;
                    worksheet.Cells[row, 40].Value = dataList[i].AuxHeatCurrentB;
                    worksheet.Cells[row, 41].Value = dataList[i].AuxHeatCurrentC;
                }

                //找到保存文件的excel目录地址
                string folderPath = @"D:\ProcessMonitoringExport";
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                string filePath = Path.Combine(folderPath, $"Tube{tubeNum + 1}_监控数据_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                await Task.Run(() => package.SaveAs(new FileInfo(filePath)));
            }
        }
        #endregion
    }
}
