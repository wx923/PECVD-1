using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfApp4.Models;
using HslCommunication.ModBus;
using WpfApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HslCommunication.Profinet.Inovance;
using System.Text;

namespace WpfApp4.Services
{
    /// <summary>
    /// 炉管服务类，负责管理和监控炉管数据
    /// </summary>
    public class FurnaceService
    {
        private static readonly Lazy<FurnaceService> _instance = new Lazy<FurnaceService>(() => new FurnaceService());
        /// <summary>
        /// 获取FurnaceService的单例实例
        /// </summary>
        public static FurnaceService Instance => _instance.Value;

        private Dictionary<int, ModbusTcpNet> _modbusClients;

        //创建用于任务取消的字典
        public Dictionary<int, CancellationTokenSource> _cancellationTokenSources = new Dictionary<int, CancellationTokenSource>();

        //创建炉管所需要的数据
        public ObservableCollection<FurnaceData> Furnaces { get; private set; }

        private int _selectedFurnaceIndex = 0;
        /// <summary>
        /// 获取或设置当前选中的炉管索引
        /// </summary>
        public int SelectedFurnaceIndex
        {
            get => _selectedFurnaceIndex;
            set
            {
                if (value >= 0 && value < Furnaces.Count)
                {
                    _selectedFurnaceIndex = value;
                }
            }
        }

        /// <summary>
        /// 私有构造函数，初始化炉管服务
        /// </summary>
        private FurnaceService()
        {
            
            // 初始化炉管数据集合
            Furnaces = new ObservableCollection<FurnaceData>();
            for (int i = 0; i < 6; i++)
            {
                Furnaces.Add(new FurnaceData());
            }

            // 获取所有炉管的ModbusTcp客户端
            _modbusClients = new Dictionary<int, ModbusTcpNet>();
            for (int i = 0; i < 6; i++)
            {
                _modbusClients[i] = PlcCommunicationService.Instance.ModbusTcpClients[(PlcCommunicationService.PlcType)i];
            }

            // 订阅每个炉管PLC的连接状态改变事件
            PlcCommunicationService.Instance.ConnectionStateChanged += OnPlcConnectionStateChanged;
        }

        /// <summary>
        /// 处理PLC连接状态改变事件
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">包含PLC类型和连接状态的事件参数</param>
        private void OnPlcConnectionStateChanged(object sender, (PlcCommunicationService.PlcType PlcType, bool IsConnected) e)
        {
            // 只处理炉管PLC的连接状态（PlcType 0-5）
            if ((int)e.PlcType < 6 && e.IsConnected)
            {
                _ = StartDataUpdate((int)e.PlcType);
            }
        }

        /// <summary>
        /// 启动指定炉管的数据更新任务
        /// </summary>
        /// <param name="furnaceIndex">炉管索引</param>
        /// <returns>表示异步操作的任务</returns>
        private Task StartDataUpdate(int furnaceIndex)
        {
            // 确保有效的炉管索引
            if (furnaceIndex < 0 || furnaceIndex >= 6)
            {
                return Task.CompletedTask;
            }

            // 如果已经有该炉管的更新任务在运行，先取消它
            if (_cancellationTokenSources[furnaceIndex] != null)
            {
                _cancellationTokenSources[furnaceIndex].Cancel();
            }
            _cancellationTokenSources[furnaceIndex] = new CancellationTokenSource();
            
            Task.Run(async () => 
            {
                while (!_cancellationTokenSources[furnaceIndex].Token.IsCancellationRequested)
                {
                    try
                    {
                        await UpdateFurnaceDataAsync(furnaceIndex);
                        await Task.Delay(100, _cancellationTokenSources[furnaceIndex].Token);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"炉管{furnaceIndex}数据更新异常: {ex.Message}");
                        await Task.Delay(1000, _cancellationTokenSources[furnaceIndex].Token);
                    }
                }
            }, _cancellationTokenSources[furnaceIndex].Token);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 异步更新炉管数据
        /// </summary>
        /// <param name="furnaceIndex">炉管索引</param>
        /// <returns>表示异步操作的任务</returns>
        private async Task UpdateFurnaceDataAsync(int furnaceIndex)
        {
            try
            {
                var data = new FurnaceData();
                int offset = furnaceIndex * 100;
                var modbusClient = _modbusClients[furnaceIndex];

                // 读取所有测量数据并映射到ProcessExcelModel的属性
                data.Step = (int)modbusClient.ReadFloat($"{offset + 0}").Content; // 步数
                data.Name = modbusClient.ReadString($"{offset + 1}", 10, Encoding.UTF8).Content; // 名称，假设是字符串类型
                data.Time = (int)modbusClient.ReadFloat($"{offset + 2}").Content; // 时间（s）

                // 温度参数 T1-T6
                data.T1 = (int)modbusClient.ReadFloat($"{offset + 3}").Content;
                data.T2 = (int)modbusClient.ReadFloat($"{offset + 4}").Content;
                data.T3 = (int)modbusClient.ReadFloat($"{offset + 5}").Content;
                data.T4 = (int)modbusClient.ReadFloat($"{offset + 6}").Content;
                data.T5 = (int)modbusClient.ReadFloat($"{offset + 7}").Content;
                data.T6 = (int)modbusClient.ReadFloat($"{offset + 8}").Content;

                // 气体参数
                data.N2 = (int)modbusClient.ReadFloat($"{offset + 9}").Content;
                data.Sih4 = (int)modbusClient.ReadFloat($"{offset + 10}").Content;
                data.N2o = (int)modbusClient.ReadFloat($"{offset + 11}").Content;
                data.Nh3 = (int)modbusClient.ReadFloat($"{offset + 12}").Content; // 注意字段名称为NH3

                // 压力参数
                data.PressureValue = (int)modbusClient.ReadFloat($"{offset + 13}").Content;

                // 功率参数
                data.Power = (int)modbusClient.ReadFloat($"{offset + 14}").Content;

                // 脉冲参数
                data.PulseOn = (int)modbusClient.ReadFloat($"{offset + 15}").Content;
                data.PulseOff = (int)modbusClient.ReadFloat($"{offset + 16}").Content;

                // 运动参数
                data.MoveSpeed = (int)modbusClient.ReadFloat($"{offset + 17}").Content;
                data.RetreatSpeed = (int)modbusClient.ReadFloat($"{offset + 18}").Content;

                // 辅热参数
                data.AuxiliaryHeatTemperature = (int)modbusClient.ReadFloat($"{offset + 19}").Content;

                // 垂直速度
                data.VerticalSpeed = (int)modbusClient.ReadFloat($"{offset + 20}").Content;


                // 在UI线程更新数据
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var furnace = Furnaces[furnaceIndex];
                    furnace.Step = data.Step; // 步数
                    furnace.Name = data.Name; // 名称
                    furnace.Time = data.Time; // 时间（s）

                    // 温度参数 T1-T6
                    furnace.T1 = data.T1;
                    furnace.T2 = data.T2;
                    furnace.T3 = data.T3;
                    furnace.T4 = data.T4;
                    furnace.T5 = data.T5;
                    furnace.T6 = data.T6;

                    // 气体参数
                    furnace.N2 = data.N2;
                    furnace.Sih4 = data.Sih4;
                    furnace.N2o = data.N2o;
                    furnace.Nh3 = data.Nh3;

                    // 压力值
                    furnace.PressureValue = data.PressureValue;

                    // 功率参数
                    furnace.Power = data.Power;

                    // 脉冲参数
                    furnace.PulseOn = data.PulseOn;
                    furnace.PulseOff = data.PulseOff;

                    // 运动参数
                    furnace.MoveSpeed = data.MoveSpeed;
                    furnace.RetreatSpeed = data.RetreatSpeed;

                    // 辅热参数
                    furnace.AuxiliaryHeatTemperature = data.AuxiliaryHeatTemperature;

                    // 垂直速度
                    furnace.VerticalSpeed = data.VerticalSpeed;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"炉管{furnaceIndex} Modbus通讯异常: {ex.Message}");
                throw;
            }
        }


        /// <summary>
        /// 向PLC发送工艺数据
        /// </summary>
        /// <param name="collectionName">工艺数据集合名称</param>
        /// <param name="furnaceNumber">炉管编号</param>
        /// <returns>表示异步操作的任务</returns>
        /// <exception cref="Exception">当找不到PLC连接或工艺数据空时抛出异常</exception>
        public async Task SendProcessDataToPLC(string collectionName, int furnaceNumber)
        {
            try
            {
                // 获取对应炉管的PLC连接
                int furnaceIndex = furnaceNumber - 1;
                if (!_modbusClients.TryGetValue(furnaceIndex, out var modbusClient))
                {
                    throw new Exception($"找不到炉管{furnaceNumber}的PLC连接");
                }

                // 从数据库获取工艺数据 - 使用新的方法名
                var processData = await MongoDbService.Instance.GetProcessDataFromCollectionAsync(collectionName);
                if (processData == null || !processData.Any())
                {
                    throw new Exception($"工艺集合{collectionName}中没有数据");
                }

                // 遍历每个工艺步骤并写入PLC
                for (int i = 0; i < processData.Count; i++)
                {
                    var step = processData[i];
                    int baseAddress = i * 50;  // 每个工艺步骤占用50个地址空间

                    // 写入工艺参数
                    await modbusClient.WriteAsync($"{baseAddress + 0}", step.Step); // 步数
                    await modbusClient.WriteAsync($"{baseAddress + 1}", step.Name); // 名称（如果支持字符串写入）
                    await modbusClient.WriteAsync($"{baseAddress + 2}", step.Time); // 时间（s）

                    // 温度参数 T1-T6
                    await modbusClient.WriteAsync($"{baseAddress + 3}", step.T1);
                    await modbusClient.WriteAsync($"{baseAddress + 4}", step.T2);
                    await modbusClient.WriteAsync($"{baseAddress + 5}", step.T3);
                    await modbusClient.WriteAsync($"{baseAddress + 6}", step.T4);
                    await modbusClient.WriteAsync($"{baseAddress + 7}", step.T5);
                    await modbusClient.WriteAsync($"{baseAddress + 8}", step.T6);

                    // 气体参数
                    await modbusClient.WriteAsync($"{baseAddress + 9}", step.N2);
                    await modbusClient.WriteAsync($"{baseAddress + 10}", step.Sih4);
                    await modbusClient.WriteAsync($"{baseAddress + 11}", step.N2o);
                    await modbusClient.WriteAsync($"{baseAddress + 12}", step.Nh3);

                    // 压力值
                    await modbusClient.WriteAsync($"{baseAddress + 13}", step.PressureValue);

                    // 功率参数
                    await modbusClient.WriteAsync($"{baseAddress + 14}", step.Power);

                    // 脉冲参数
                    await modbusClient.WriteAsync($"{baseAddress + 15}", step.PulseOn);
                    await modbusClient.WriteAsync($"{baseAddress + 16}", step.PulseOff);

                    // 运动参数
                    await modbusClient.WriteAsync($"{baseAddress + 17}", step.MoveSpeed);
                    await modbusClient.WriteAsync($"{baseAddress + 18}", step.RetreatSpeed);

                    // 辅热参数
                    await modbusClient.WriteAsync($"{baseAddress + 19}", step.AuxiliaryHeatTemperature);

                    // 垂直速度
                    await modbusClient.WriteAsync($"{baseAddress + 20}", step.VerticalSpeed);
                }

                // 写入工艺步骤总数
                await modbusClient.WriteAsync("2000", processData.Count);

                // 更新当前炉管的工艺集合名
                Furnaces[furnaceIndex].ProcessCollectionName = collectionName;
            }
            catch (Exception ex)
            {
                throw new Exception($"下发工艺数据失败: {ex.Message}");
            }
        }


        /// <summary>
        /// 清理资源，取消数据更新任务并取消事件订阅
        /// </summary>
        public void Cleanup()
        {
            foreach (var cts in _cancellationTokenSources.Values)
            {
                cts?.Cancel();
                cts?.Dispose();
            }
            PlcCommunicationService.Instance.ConnectionStateChanged -= OnPlcConnectionStateChanged;
        }

    }
} 