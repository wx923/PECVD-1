using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using WpfApp4.Services;
using WpfApp4.Models;
using System.IO;
using OfficeOpenXml; // 需要EPPlus库
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using Microsoft.Win32;

namespace WpfApp4.ViewModel
{
    public partial class DataShowPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private int tubeNumber;
        [ObservableProperty]
        private ObservableCollection<string> collections = new();
        [ObservableProperty]
        private string selectedCollection;
        [ObservableProperty]
        private ObservableCollection<ProcessRecordInfo> processRecords = new();
        [ObservableProperty]
        private ObservableCollection<ProcessExcelModel> collectionData = new();
        private int viewIndex = 0; // 0:表格 1:曲线
        public int ViewIndex
        {
            get => viewIndex;
            set
            {
                if (viewIndex != value)
                {
                    viewIndex = value;
                    OnPropertyChanged(nameof(ViewIndex));
                    OnPropertyChanged(nameof(IsTableView));
                    OnPropertyChanged(nameof(IsPlotView));
                }
            }
        }
        public bool IsTableView => ViewIndex == 0;
        public bool IsPlotView => ViewIndex == 1;
        [ObservableProperty]
        private PlotModel plotModel;

        public DataShowPageViewModel(int tubeNumber)
        {
            TubeNumber = tubeNumber;
            PropertyChanged += DataShowPageViewModel_PropertyChanged;
            _ = LoadCollectionsAsync();
        }

        private async void DataShowPageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SelectedCollection))
            {
                await LoadCollectionDataAsync();
            }
        }

        [RelayCommand]
        private async Task LoadCollectionsAsync()
        {
            var allRecords = MongoDbService.Instance.GlobalProcessRecords;
            var completedFiles = allRecords
                .Where(f => f.TubeNumber == TubeNumber && f.IsCompleted)
                .OrderByDescending(f => f.CreateTime)
                .ToList();

            ProcessRecords.Clear();
            Collections.Clear();
            foreach (var file in completedFiles)
            {
                ProcessRecords.Add(file);
                Collections.Add($"{file.ProcessName} {file.CreateTime:yyyy-MM-dd HH:mm:ss} ({file.CollectionName})");
            }
            SelectedCollection = Collections.FirstOrDefault();
        }

        private async Task LoadCollectionDataAsync()
        {
            if (string.IsNullOrEmpty(SelectedCollection))
            {
                CollectionData.Clear();
                return;
            }
            // 从SelectedCollection中提取CollectionName
            var record = ProcessRecords.FirstOrDefault(r => SelectedCollection.Contains(r.CollectionName));
            if (record == null)
            {
                CollectionData.Clear();
                return;
            }
            var data = await MongoDbService.Instance.GetProcessDataFromCollectionAsync(record.CollectionName);
            CollectionData.Clear();
            foreach (var item in data)
                CollectionData.Add(item);
        }

        [RelayCommand]
        private void ExportToExcel()
        {
            if (CollectionData == null || CollectionData.Count == 0)
            {
                System.Windows.MessageBox.Show("没有可导出的数据！");
                return;
            }
            var dialog = new SaveFileDialog
            {
                Filter = "Excel文件|*.xlsx",
                Title = "保存为Excel文件",
                FileName = $"工艺数据_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
            };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                        // 中文表头
                        string[] headers = new[] { "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "N2", "SiH4", "N2O", "H2", "PH3", "压力", "功率1", "功率2", "进/出", "平移速度", "上下速", "辅热时间", "辅热温度", "脉冲开1", "脉冲关1", "脉冲开2", "脉冲关2", "射频电流", "电流参考值（A）", "电流卡控值（A）", "射频电压", "电压参考值（V）", "电压卡控值（V）" };
                        string[] props = new[] { "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "N2", "SiH4", "N2O", "H2", "PH3", "Pressure", "Power1", "Power2", "InOut", "MoveSpeed", "UpDownSpeed", "TypicalHeatTime", "AssistTemp", "PulseOn1", "PulseOff1", "PulseOn2", "PulseOff2", "RFCurrent", "CurrentRef", "CurrentLimit", "RFVoltage", "VoltageRef", "VoltageLimit" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            worksheet.Cells[1, i + 1].Value = headers[i];
                        }
                        for (int row = 0; row < CollectionData.Count; row++)
                        {
                            for (int col = 0; col < props.Length; col++)
                            {
                                var prop = typeof(ProcessExcelModel).GetProperty(props[col]);
                                worksheet.Cells[row + 2, col + 1].Value = prop?.GetValue(CollectionData[row]);
                            }
                        }
                        package.SaveAs(new FileInfo(dialog.FileName));
                    }
                    System.Windows.MessageBox.Show("导出成功！");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"导出失败: {ex.Message}");
                }
            }
        }

        [RelayCommand]
        private void ToggleView()
        {
            ViewIndex = (ViewIndex + 1) % 2; // 目前只支持表格和曲线，后续可扩展
            if (IsPlotView)
            {
                GeneratePlot();
            }
        }

        private void GeneratePlot()
        {
            var model = new PlotModel { Title = "工艺参数曲线" };
            // 以T1为例，x轴为秒（索引），y轴为T1的值
            var series = new LineSeries { Title = "T1", MarkerType = MarkerType.Circle };
            for (int i = 0; i < CollectionData.Count; i++)
            {
                var item = CollectionData[i];
                var t1Prop = typeof(ProcessExcelModel).GetProperty("T1");
                if (t1Prop != null)
                {
                    var y = t1Prop.GetValue(item);
                    if (y != null && double.TryParse(y.ToString(), out double yVal))
                    {
                        series.Points.Add(new DataPoint(i, yVal));
                    }
                }
            }
            model.Series.Add(series);
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "时间 (s)" });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "T1" });
            PlotModel = model;
        }
    }
} 