using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HslCommunication.ModBus;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Asn1.BC;
using WpfApp.Services;
using WpfApp4.Models;
using WpfApp4.Services.WpfApp4.Services;
using static WpfApp.Services.PlcCommunicationService;

namespace WpfApp4.Services
{
   
namespace WpfApp4.Services
    {
        public class AlarmService : IDisposable
        {
            private static readonly Lazy<AlarmService> _instance = new Lazy<AlarmService>(() => new AlarmService());
            public static AlarmService Instance => _instance.Value;
            private readonly Dictionary<int, ModbusTcpNet> _modbusClients = new Dictionary<int, ModbusTcpNet>();
            public Dictionary<int, bool> _connectionStates = new Dictionary<int, bool>();
            public  Dictionary<int, AlarmInfo> _alarmStates = new Dictionary<int, AlarmInfo>();
            public Dictionary<int, CancellationTokenSource> _readingTasks = new Dictionary<int, CancellationTokenSource>();

            public Dictionary<int, ObservableCollection<AlarmLog>> AlarmLogs = new Dictionary<int, ObservableCollection<AlarmLog>>();
            public Dictionary<int, ObservableCollection<OperationLog>> OperationLogs = new Dictionary<int, ObservableCollection<OperationLog>>();

            private const int READING_INTERVAL = 1000; // 读取间隔（毫秒）
            private const string ALARM_REGISTER_ADDRESS = "40000"; // 假设报警状态从40000地址开始

            public AlarmService()
            {
                // 初始化所有炉管的集合
                for (int i = 0; i < 6; i++)
                {
                    AlarmLogs[i] = new ObservableCollection<AlarmLog>();
                    OperationLogs[i] = new ObservableCollection<OperationLog>();
                    _connectionStates[i] = false;
                    _alarmStates[i] = new AlarmInfo();
                }

                // 初始化PLC连接
                InitializePlcConnections();

                // 订阅PLC状态变更事件
                PlcCommunicationService.Instance.ConnectionStateChanged += OnPlcConnectionStateChanged;

                // 启动所有炉管的读取任务
                StartReadingAllFurnaces();
            }
            #region PLC通信
            private void InitializePlcConnections()
            {
                for (int i = 0; i < 6; i++)
                {
                    if (PlcCommunicationService.Instance.ConnectionStates.TryGetValue((PlcType)i, out bool isConnected) && isConnected)
                    {
                        _modbusClients[i] = PlcCommunicationService.Instance.ModbusTcpClients[(PlcType)i];
                        _connectionStates[i] = true;
                        OperationLogs[i].Add(new OperationLog
                        {
                            Timestamp = DateTime.Now,
                            UserName = "System",
                            Details = $"炉管 {i + 1} PLC 初始化连接成功"
                        });
                    }
                    else
                    {
                        _connectionStates[i] = false;
                        AlarmLogs[i].Add(new AlarmLog
                        {
                            Level = AlarmLevel.Warning,
                            Message = $"炉管 {i + 1} PLC 未连接",
                            Timestamp = DateTime.Now,
                            Source = "PLC Communication"
                        });
                    }
                }
            }

            private void StartReadingAllFurnaces()
            {
                for (int i = 0; i < 6; i++)
                {
                    StartReadingFurnace(i);
                }
            }

            private void StartReadingFurnace(int furnaceId)
            {
                if (_readingTasks.ContainsKey(furnaceId))
                {
                    _readingTasks[furnaceId].Cancel();
                    _readingTasks[furnaceId].Dispose();
                    _readingTasks.Remove(furnaceId);
                }

                var cts = new CancellationTokenSource();
                _readingTasks[furnaceId] = cts;

                Task.Run(async () => await ReadPlcDataAsync(furnaceId, cts.Token), cts.Token);
            }

            private async Task ReadPlcDataAsync(int furnaceId, CancellationToken token)
            {
                while (!token.IsCancellationRequested)
                {
                    if (_modbusClients.ContainsKey(furnaceId))
                    {
                        try
                        {
                            var client = _modbusClients[furnaceId];
                            var result = client.ReadCoil(ALARM_REGISTER_ADDRESS, 56);
                            if (result.IsSuccess)
                            {
                               await Application.Current.Dispatcher.InvokeAsync(()=>UpdateAlarmStates(furnaceId, result.Content));
                            }
                            else
                            {
                                AddAlarmLog(furnaceId, AlarmLevel.Warning, "读取PLC数据失败");
                            }
                        }
                        catch (Exception ex)
                        {
                            AddAlarmLog(furnaceId, AlarmLevel.Critical, $"PLC读取异常: {ex.Message}");
                        }
                    }
                    await Task.Delay(READING_INTERVAL, token);
                }
            }

            private void UpdateAlarmStates(int furnaceId, bool[] states)
            {
                var alarmInfo = _alarmStates[furnaceId];
                int index = 0;

                // 直接赋值并检查新值是否为 true
                alarmInfo.ControlTemperatureLimitOverheat = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 控制温度极限超温报警 触发");

                alarmInfo.ProfileThermocoupleLimitOverheat = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile热偶极限超温报警 触发");

                alarmInfo.Profile1 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-1报警 触发");

                alarmInfo.Profile2 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-2报警 触发");

                alarmInfo.Profile3 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-3报警 触发");

                alarmInfo.Profile4 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-4报警 触发");

                alarmInfo.Profile5 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-5报警 触发");

                alarmInfo.Profile6 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-6报警 触发");

                alarmInfo.Profile7 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-7报警 触发");

                alarmInfo.Profile8 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-8报警 触发");

                alarmInfo.Profile9 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Profile-9报警 触发");

                alarmInfo.Spike1 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-1报警 触发");

                alarmInfo.Spike2 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-2报警 触发");

                alarmInfo.Spike3 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-3报警 触发");

                alarmInfo.Spike4 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-4报警 触发");

                alarmInfo.Spike5 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-5报警 触发");

                alarmInfo.Spike6 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-6报警 触发");

                alarmInfo.Spike7 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-7报警 触发");

                alarmInfo.Spike8 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-8报警 触发");

                alarmInfo.Spike9 = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Spike-9报警 触发");

                alarmInfo.SolidStateRelayOverheat = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 固态继电器超温报警 触发");

                alarmInfo.TransformerOverheat = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 变压器超温报警 触发");

                alarmInfo.AuxiliaryHeating = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 辅助加热报警 触发");

                alarmInfo.AuxiliaryOverheat = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 辅助超温报警 触发");

                alarmInfo.WaterFlowFrontFlange = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 水流(前法兰)报警 触发");

                alarmInfo.WaterFlowRearFlange = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 水流(后法兰)报警 触发");

                alarmInfo.WaterTempFrontFlange = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 水温(前法兰)报警 触发");

                alarmInfo.WaterTempRearFlange = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 水温(后法兰)报警 触发");

                alarmInfo.N2FlowMeter = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - N2流量计报警 触发");

                alarmInfo.Sih4FlowMeter = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - SIH4流量计报警 触发");

                alarmInfo.Nh3FlowMeter = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - NH3流量计报警 触发");

                alarmInfo.N2oFlowMeter = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - N2O流量计报警 触发");

                alarmInfo.FrontLimitPosition = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 前极限位报警 触发");

                alarmInfo.RearLimitPosition = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 后极限位报警 触发");

                alarmInfo.UpperLimitPosition = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 上极限位报警 触发");

                alarmInfo.LowerLimitPosition = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 下极限位报警 触发");

                alarmInfo.BoatSynchronization = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 舟同步报警 触发");

                alarmInfo.PressureDeviation = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 压强超差报警 触发");

                alarmInfo.ServoAnomaly = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 伺服异常报警 触发");

                alarmInfo.ServoCommunication = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 伺服通讯报警 触发");

                alarmInfo.N2Pressure = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - N2压力报警 触发");

                alarmInfo.AirPressureFurnaceMouth = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 风压(炉口)报警 触发");

                alarmInfo.AirPressureFurnaceChamber = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 风压(炉室)报警 触发");

                alarmInfo.CdaCabinet = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - CDA(机柜)报警 触发");

                alarmInfo.CdaMotor = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - CDA(马达)报警 触发");

                alarmInfo.MotionStop = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - Motion Stop报警 触发");

                alarmInfo.RfShortCircuit = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 射频短路报警 触发");

                alarmInfo.EquipmentPowerFailure = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 设备断电报警 触发");

                alarmInfo.RfAnomaly = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 射频异常报警 触发");

                alarmInfo.RfTempDifference = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - 射频温差报警 触发");

                alarmInfo.Sih4Leak = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - SIH4泄漏报警 触发");

                alarmInfo.Nh3Leak = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - NH3泄漏报警 触发");

                alarmInfo.N2oLeak = states[index];
                if (states[index++]) AddAlarmLog(furnaceId, AlarmLevel.Warning, $"炉管 {furnaceId + 1} - N2O泄漏报警 触发");
            }

            #endregion

            #region 报警日志
            private void AddAlarmLog(int furnaceId, AlarmLevel level, string message)
            {
                AlarmLogs[furnaceId].Add(new AlarmLog
                {
                    Level = level,
                    Message = message,
                    Timestamp = DateTime.Now,
                    Source = "PLC Alarm"
                });
            }
            #endregion
            #region 辅助函数
            private void OnPlcConnectionStateChanged(object sender, (PlcType PlcType, bool IsConnected) e)
            {
                int index = (int)e.PlcType;
                _connectionStates[index] = e.IsConnected;

                if (e.IsConnected)
                {
                    _modbusClients[index] = PlcCommunicationService.Instance.ModbusTcpClients[e.PlcType];
                    StartReadingFurnace(index);
                    OperationLogs[index].Add(new OperationLog
                    {
                        Timestamp = DateTime.Now,
                        UserName = "System",
                        Details = $"炉管 {index + 1} PLC 连接成功"
                    });
                }
                else
                {
                    _modbusClients.Remove(index);
                    if (_readingTasks.ContainsKey(index))
                    {
                        _readingTasks[index].Cancel();
                        _readingTasks[index].Dispose();
                        _readingTasks.Remove(index);
                    }
                    AlarmLogs[index].Add(new AlarmLog
                    {
                        Level = AlarmLevel.Warning,
                    Message = $"炉管 {index + 1} PLC 连接断开",
                    Timestamp = DateTime.Now,
                    Source = "PLC Communication"
                });
                }
            }

            public void Dispose()
            {
                PlcCommunicationService.Instance.ConnectionStateChanged -= OnPlcConnectionStateChanged;
                foreach (var cts in _readingTasks.Values)
                {
                    cts.Cancel();
                }
                _readingTasks.Clear();
            }
            #endregion
        }
    }
}
