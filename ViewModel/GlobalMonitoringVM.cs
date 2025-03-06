using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
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


        public GlobalMonitoringVM(int turbNumtubeNumber) {

            _progressMonitor = MongoDbService.Instance.GlobalProcessFlowSteps.FirstOrDefault(x => x.Fnum == (turbNumtubeNumber - 1));
            _dataCollection = GlobalMonitoringService.Instance.GlobalMonitoringAllData[turbNumtubeNumber - 1];
            Console.WriteLine($"{_progressMonitor.ProcessFileName}");
        }
    }
}
