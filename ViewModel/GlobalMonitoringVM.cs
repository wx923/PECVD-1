using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Driver;
using WpfApp4.Models;
using WpfApp4.Services;

namespace WpfApp4.ViewModel
{
    partial class GlobalMonitoringVM :ObservableObject
    {
        [ObservableProperty]
        public GlobalMonitoringDataModel _dataCollection;

        //步骤监控对象
        [ObservableProperty]
        public GlobalMonitoringStatusModel _progressMonitor;

        //配方文件结构
        public Dictionary<int, ProcessExcelModel> _processExcels;

        public GlobalMonitoringVM(int turbNumtubeNumber) {
            _progressMonitor = MongoDbService.Instance.GlobalProcessFlowSteps.FirstOrDefault(x => x.Fnum == (turbNumtubeNumber - 1));
            _dataCollection = GlobalMonitoringService.Instance.GlobalMonitoringAllData[turbNumtubeNumber - 1];
        }




        #region 开始工艺
        [RelayCommand]
        public async Task StartProcess() {
            //从工艺配方表中读取对应的结构
            // 使用await等待异步操作完成
            var list = await MongoDbService.Instance._database.GetCollection<ProcessExcelModel>(_progressMonitor.ProcessFileName)
                .Find(ProcessExcelModel => true)
                .ToListAsync();

            // 将列表转换为字典（假设ProcessExcelModel有一个Id属性）
            int index = 0; // 初始化索引变量
            foreach (var item in list)
            {
                _processExcels[index] = item; // 使用索引作为字典的键
                index++; // 索引递增
            }

            //判断是否能够开始工艺
            for (var i= 0; i < _processExcels.Count; i++)
            {
                switch (_processExcels[i].Id)
                {
                    case "装片":
                         LoadSampleAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "升温":
                         HeatUpAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "慢抽真空":
                         SlowPumpDownAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "抽真空":
                         PumpDownAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "检漏":
                         DetectAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "调压":
                         AdjustPressureAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "淀积":
                         DepositAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "清洗":
                         CleanAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "充氮":
                         FillWithNitrogenAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    case "卸片":
                         UnloadSampleAsync();
                        await Task.Delay(_processExcels[i].Time);
                        break;
                    default:
                        
                        break;

                }
            }
            //开始执行工艺
            
        
        }

        private async Task LoadSampleAsync()
        {
            // 实现装片逻辑
        }

        private async Task HeatUpAsync()
        {
            // 实现升温逻辑
        }

        private async Task SlowPumpDownAsync()
        {
            // 实现慢抽真空逻辑
        }

        private async Task PumpDownAsync()
        {
            // 实现抽真空逻辑
        }

        private async Task DetectAsync()
        {
            // 实现检测逻辑
        }
        private async Task AdjustPressureAsync()
        {
            //实现调压

            throw new NotImplementedException();
        }
        private async Task FillWithNitrogenAsync()
        {
            //实现充氮
            throw new NotImplementedException();
        }

        private async Task DepositAsync()
        {
            // 实现沉积逻辑
        }

        private async Task CleanAsync()
        {
            // 实现清洗逻辑
        }

        private async Task UnloadSampleAsync()
        {
            // 实现卸片逻辑
        }
        #endregion
    }
}
