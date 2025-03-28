using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication.ModBus;
using Org.BouncyCastle.Asn1.BC;
using WpfApp.Services;
using WpfApp4.Models;
using static WpfApp.Services.PlcCommunicationService;

namespace WpfApp4.Services
{
    public  class AlarmService
    {
        //六个炉管的PLC连接对象
        Dictionary<int,ModbusTcpNet> _modbusClients = new Dictionary<int,ModbusTcpNet>();
        //六个炉管的连接对象
        Dictionary<int, bool> _connectionStates = new Dictionary<int, bool>();

        //每个炉管的报警日志集合
        public Dictionary<int, ObservableCollection<AlarmLog>> AlarmLogs = new Dictionary<int, ObservableCollection<AlarmLog>>();

        // 每个炉管的操作日志集合
        public Dictionary<int, ObservableCollection<OperationLog>> OperationLogs = new Dictionary<int, ObservableCollection<OperationLog>>();
        public AlarmService(int furNum)
        {

            //判定PLC是否连接
            for (var i = 0; i < 6; i++)
            {
                if (PlcCommunicationService.Instance.ConnectionStates[(PlcType)i])
                {
                    _modbusClients[i] = PlcCommunicationService.Instance.ModbusTcpClients[(PlcType)i];
                }
                else
                {
                    // PLC未连接时记录报警
                    AlarmLogs[i].Add(new AlarmLog
                    {
                        Level = AlarmLevel.Warning,
                        Message = $"炉管 {i + 1} PLC 未连接",
                        Timestamp = DateTime.Now,
                        Source = "PLC Communication"
                    });
                }
            }
                //添加PLC状态变更事件
                PlcCommunicationService.Instance.ConnectionStateChanged += OnPlcConnectionStateChanged;

        }
        #region PLC相关操作区
        
        
        //当PLC状态发生更新之后，需要做出的反应
        private void OnPlcConnectionStateChanged(object sender, (PlcType PlcType, bool IsConnected) e)
        {
            int index = (int)e.PlcType; // 将 PlcType 转换为索引
            bool isConnected = e.IsConnected;

            if (isConnected)
            {
                // PLC连接成功
                _modbusClients[index] = PlcCommunicationService.Instance.ModbusTcpClients[e.PlcType];
                OperationLogs[index].Add(new OperationLog
                {
                    Timestamp = DateTime.Now,
                    UserName = "System", // 假设系统自动操作
                    Details = $"炉管 {index + 1} PLC 连接成功"
                });
            }
            else
            {
                // PLC断开连接
                _modbusClients.Remove(index);
                AlarmLogs[index].Add(new AlarmLog
                {
                    Level = AlarmLevel.Warning,
                    Message = $"炉管 {index + 1} PLC 连接断开",
                    Timestamp = DateTime.Now,
                    Source = "PLC Communication"
                });
            }
        }
        #endregion
        #region 用户操作对象区

        #endregion
        #region 报警信息区

        #endregion
    }
}
