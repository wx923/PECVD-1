using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HslCommunication.ModBus;
using NPOI.SS.Formula.Functions;
using WpfApp.Services;
using WpfApp4.Models;
using WpfApp4.ViewModel;
using static WpfApp.Services.PlcCommunicationService;

namespace WpfApp4.Services
{
    internal class HomePageService
    {
        //创建全局单例对象
        private static Lazy<HomePageService> _instance = new Lazy<HomePageService>(() => new HomePageService());
        public static HomePageService Instance => _instance.Value;

        //创建六个炉管PLC对象
        public Dictionary<int, ModbusTcpNet> _connectionStatus = new Dictionary<int, ModbusTcpNet>();

        //创建六个炉管的任务管理对象
        public Dictionary<int, CancellationTokenSource> _taskStatus = new Dictionary<int, CancellationTokenSource>();

        //数据绑定对象
        public Dictionary<int, HomePageModel> _viewModel = new Dictionary<int, HomePageModel>();
        HomePageService()
        {
            //第一步初始化PLC连接
            InitializePLCConnection();

            //绑定事件通知机制
            PlcCommunicationService.Instance.ConnectionStateChanged += OnPlcConnectionStateChanged;

            //开始读取PLC数据
            StartReadingAllFurnaces();

        }

        private void StartReadingAllFurnaces()
        {
            for (var i = 0; i < 6; i++)
            {
                StartReadingFurnace(i);
            }
        }


        private void StartReadingFurnace(int furnaceIndex)
        {
            if (_taskStatus.ContainsKey(furnaceIndex)) {
                _taskStatus[furnaceIndex].Cancel();
                _taskStatus[furnaceIndex].Dispose();
                _taskStatus.Remove(furnaceIndex);
            }
            _taskStatus[furnaceIndex] = new CancellationTokenSource();
            Task.Run(async () => {
                await ReadPlcDataAsync(furnaceIndex, _taskStatus[furnaceIndex].Token);
            }, _taskStatus[furnaceIndex].Token);
        }

        private async Task ReadPlcDataAsync(int furnaceId, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_connectionStatus.ContainsKey(furnaceId))
                {
                    int address = 40001;

                    var innerTemp1Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var innerTemp2Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var innerTemp3Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var innerTemp4Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var innerTemp5Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var innerTemp6Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var innerTemp7Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var innerTemp8Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var innerTemp9Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;

                    var outerTemp1Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var outerTemp2Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var outerTemp3Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var outerTemp4Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var outerTemp5Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var outerTemp6Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var outerTemp7Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var outerTemp8Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var outerTemp9Result = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;

                    var n2FlowRateResult = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var nh3FlowRateResult = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var sih4FlowRateResult = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var n2oFlowRateResult = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var rfPowerResult = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var pressureResult = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var auxiliaryHeatTempResult = await _connectionStatus[furnaceId].ReadFloatAsync(address.ToString()); address += 2;
                    var processStatusResult = await _connectionStatus[furnaceId].ReadCoilAsync(address.ToString());

                    if (innerTemp1Result.IsSuccess || innerTemp2Result.IsSuccess || innerTemp3Result.IsSuccess ||
                        innerTemp4Result.IsSuccess || innerTemp5Result.IsSuccess || innerTemp6Result.IsSuccess ||
                        innerTemp7Result.IsSuccess || innerTemp8Result.IsSuccess || innerTemp9Result.IsSuccess ||
                        outerTemp1Result.IsSuccess || outerTemp2Result.IsSuccess || outerTemp3Result.IsSuccess ||
                        outerTemp4Result.IsSuccess || outerTemp5Result.IsSuccess || outerTemp6Result.IsSuccess ||
                        outerTemp7Result.IsSuccess || outerTemp8Result.IsSuccess || outerTemp9Result.IsSuccess ||
                        n2FlowRateResult.IsSuccess || nh3FlowRateResult.IsSuccess || sih4FlowRateResult.IsSuccess ||
                        n2oFlowRateResult.IsSuccess || rfPowerResult.IsSuccess || pressureResult.IsSuccess ||
                        auxiliaryHeatTempResult.IsSuccess || processStatusResult.IsSuccess)
                    {
                        await Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            _viewModel[furnaceId].InnerTemperatures1 = innerTemp1Result.Content;
                            _viewModel[furnaceId].InnerTemperatures2 = innerTemp2Result.Content;
                            _viewModel[furnaceId].InnerTemperatures3 = innerTemp3Result.Content;
                            _viewModel[furnaceId].InnerTemperatures4 = innerTemp4Result.Content;
                            _viewModel[furnaceId].InnerTemperatures5 = innerTemp5Result.Content;
                            _viewModel[furnaceId].InnerTemperatures6 = innerTemp6Result.Content;
                            _viewModel[furnaceId].InnerTemperatures7 = innerTemp7Result.Content;
                            _viewModel[furnaceId].InnerTemperatures8 = innerTemp8Result.Content;
                            _viewModel[furnaceId].InnerTemperatures9 = innerTemp9Result.Content;

                            _viewModel[furnaceId].OutTemperatures1 = outerTemp1Result.Content;
                            _viewModel[furnaceId].OutTemperatures2 = outerTemp2Result.Content;
                            _viewModel[furnaceId].OutTemperatures3 = outerTemp3Result.Content;
                            _viewModel[furnaceId].OutTemperatures4 = outerTemp4Result.Content;
                            _viewModel[furnaceId].OutTemperatures5 = outerTemp5Result.Content;
                            _viewModel[furnaceId].OutTemperatures6 = outerTemp6Result.Content;
                            _viewModel[furnaceId].OutTemperatures7 = outerTemp7Result.Content;
                            _viewModel[furnaceId].OutTemperatures8 = outerTemp8Result.Content;
                            _viewModel[furnaceId].OutTemperatures9 = outerTemp9Result.Content;

                            _viewModel[furnaceId].N2FlowRate = n2FlowRateResult.Content;
                            _viewModel[furnaceId].Nh3FlowRate = nh3FlowRateResult.Content;
                            _viewModel[furnaceId].SiH4FlowRate = sih4FlowRateResult.Content;
                            _viewModel[furnaceId].N2OFlowRate = n2oFlowRateResult.Content;
                            _viewModel[furnaceId].RfPower = rfPowerResult.Content;
                            _viewModel[furnaceId].Pressure = pressureResult.Content;
                            _viewModel[furnaceId].AuxiliaryHeatTemperature = auxiliaryHeatTempResult.Content;
                            _viewModel[furnaceId].ProcessStatus = processStatusResult.Content;
                        });
                    }
                }

                await Task.Delay(1000, token);
            }
        }
  
        public void InitializePLCConnection()
        {
            for(var i = 0; i < 6; i++)
            {
                if(PlcCommunicationService.Instance.ConnectionStates.TryGetValue((PlcType)i, out bool isConnected) && isConnected)
                {
                    _connectionStatus[i] = PlcCommunicationService.Instance.ModbusTcpClients[(PlcType)i];
                }
                _viewModel[i]=new HomePageModel();
            }
        }

        //
        private void OnPlcConnectionStateChanged(object sender, (PlcType PlcType, bool IsConnected) e)
        {
            int plcIndex = (int)e.PlcType;
            if (plcIndex < 6) // 只处理炉管 PLC (0-5)
            {
                if (e.IsConnected)
                {
                    _connectionStatus[plcIndex] = PlcCommunicationService.Instance.ModbusTcpClients[e.PlcType];
                }
                else
                {
                    //移除对应的PLC连接对象
                    _connectionStatus.Remove(plcIndex);

                    //取消对应的任务
                    if (_taskStatus.ContainsKey(plcIndex))
                    {
                        _taskStatus[plcIndex].Cancel();
                        _taskStatus[plcIndex].Dispose();
                        _taskStatus.Remove(plcIndex);
                    }
                }
            }
        }
    }
}
