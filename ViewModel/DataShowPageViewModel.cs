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
using OxyPlot.Wpf;
using Microsoft.Win32;
using OxyPlot.Annotations;
using System.Windows;
using OfficeOpenXml.Style;

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
        private ObservableCollection<ProcessDataDisplayModel> collectionData = new();
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
            _ = LoadCollectionsAsync();
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

        [RelayCommand]
        private void ExportToExcel()
        {
            try
            {
                if (CollectionData == null || CollectionData.Count == 0)
                {
                    System.Windows.MessageBox.Show("没有可导出的数据！", "导出提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dialog = new SaveFileDialog
                {
                    Filter = "Excel文件|*.xlsx",
                    Title = "保存为Excel文件",
                    FileName = $"{SelectedCollection?.Replace(":", "_").Replace("/", "_")}_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
                };

                if (dialog.ShowDialog() == true)
                {
                    // 检查文件是否被占用
                    if (IsFileInUse(dialog.FileName))
                    {
                        System.Windows.MessageBox.Show("文件正在被其他程序使用，请关闭后重试！", "导出错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 设置许可证上下文

                    using (var package = new ExcelPackage(new FileInfo(dialog.FileName)))
                    {
                        var worksheet = package.Workbook.Worksheets.Add("工艺数据");
                        
                        // 设置表头
                        string[] headers = new[] 
                        { 
                            "T1(℃)", "T2(℃)", "T3(℃)", "T4(℃)", "T5(℃)", 
                            "T6(℃)", "T7(℃)", "T8(℃)", "T9(℃)", 
                            "N2(sccm)", "SiH4(sccm)", "N2O(sccm)", "H2(sccm)", "PH3(sccm)", 
                            "压力(Pa)", "功率1(W)", "功率2(W)", "进/出", 
                            "平移速度(mm/s)", "上下速(mm/s)", 
                            "辅热时间(s)", "辅热温度(℃)", 
                            "脉冲开1(s)", "脉冲关1(s)", "脉冲开2(s)", "脉冲关2(s)", 
                            "射频电流(A)", "电流参考值(A)", "电流卡控值(A)", 
                            "射频电压(V)", "电压参考值(V)", "电压卡控值(V)" 
                        };
                        
                        string[] props = new[] 
                        { 
                            "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9",
                            "N2", "SiH4", "N2O", "H2", "PH3",
                            "Pressure", "Power1", "Power2", "InOut",
                            "MoveSpeed", "UpDownSpeed",
                            "TypicalHeatTime", "AssistTemp",
                            "PulseOn1", "PulseOff1", "PulseOn2", "PulseOff2",
                            "RFCurrent", "CurrentRef", "CurrentLimit",
                            "RFVoltage", "VoltageRef", "VoltageLimit"
                        };

                        // 写入表头并设置样式
                        for (int i = 0; i < headers.Length; i++)
                        {
                            var cell = worksheet.Cells[1, i + 1];
                            cell.Value = headers[i];
                            cell.Style.Font.Bold = true;
                            cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                            cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }

                        // 写入数据
                        for (int row = 0; row < CollectionData.Count; row++)
                        {
                            var dataItem = CollectionData[row];
                            for (int col = 0; col < props.Length; col++)
                            {
                                var prop = typeof(ProcessDataDisplayModel).GetProperty(props[col]);
                                if (prop != null)
                                {
                                    var value = prop.GetValue(dataItem)?.ToString();
                                    worksheet.Cells[row + 2, col + 1].Value = value;
                                }
                            }
                        }

                        // 自动调整列宽
                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        // 保存文件
                        package.Save();
                    }

                    System.Windows.MessageBox.Show($"数据已成功导出到：\n{dialog.FileName}", "导出成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show($"导出操作错误：{ex.Message}", "导出错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                System.Windows.MessageBox.Show($"文件访问错误：{ex.Message}", "导出错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"导出失败：{ex.Message}\n\n详细错误：{ex}", "导出错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsFileInUse(string filePath)
        {
            try
            {
                using (FileStream fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    return false;
                }
            }
            catch (IOException)
            {
                return true;
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
            var model = new PlotModel 
            { 
                Title = "工艺参数曲线",
                IsLegendVisible = true
            };

            // 添加9个温度曲线
            var temperatureSeries = new[]
            {
                new LineSeries { Title = "温度1 (T1)", MarkerType = MarkerType.Circle },
                new LineSeries { Title = "温度2 (T2)", MarkerType = MarkerType.Square },
                new LineSeries { Title = "温度3 (T3)", MarkerType = MarkerType.Triangle },
                new LineSeries { Title = "温度4 (T4)", MarkerType = MarkerType.Diamond },
                new LineSeries { Title = "温度5 (T5)", MarkerType = MarkerType.Cross },
                new LineSeries { Title = "温度6 (T6)", MarkerType = MarkerType.Plus },
                new LineSeries { Title = "温度7 (T7)", MarkerType = MarkerType.Star },
                new LineSeries { Title = "温度8 (T8)", MarkerType = MarkerType.Cross },
                new LineSeries { Title = "温度9 (T9)", MarkerType = MarkerType.Circle }
            };

            // 添加电流、电压和功率曲线
            var rfCurrentSeries = new LineSeries { Title = "射频电流", MarkerType = MarkerType.Diamond };
            var rfVoltageSeries = new LineSeries { Title = "射频电压", MarkerType = MarkerType.Square };
            var power1Series = new LineSeries { Title = "功率1", MarkerType = MarkerType.Triangle };
            var power2Series = new LineSeries { Title = "功率2", MarkerType = MarkerType.Cross };

            for (int i = 0; i < CollectionData.Count; i++)
            {
                var item = CollectionData[i];
                
                // 添加温度数据点
                if (double.TryParse(item.T1, out double t1)) temperatureSeries[0].Points.Add(new DataPoint(i, t1));
                if (double.TryParse(item.T2, out double t2)) temperatureSeries[1].Points.Add(new DataPoint(i, t2));
                if (double.TryParse(item.T3, out double t3)) temperatureSeries[2].Points.Add(new DataPoint(i, t3));
                if (double.TryParse(item.T4, out double t4)) temperatureSeries[3].Points.Add(new DataPoint(i, t4));
                if (double.TryParse(item.T5, out double t5)) temperatureSeries[4].Points.Add(new DataPoint(i, t5));
                if (double.TryParse(item.T6, out double t6)) temperatureSeries[5].Points.Add(new DataPoint(i, t6));
                if (double.TryParse(item.T7, out double t7)) temperatureSeries[6].Points.Add(new DataPoint(i, t7));
                if (double.TryParse(item.T8, out double t8)) temperatureSeries[7].Points.Add(new DataPoint(i, t8));
                if (double.TryParse(item.T9, out double t9)) temperatureSeries[8].Points.Add(new DataPoint(i, t9));

                // 添加电流、电压和功率数据点
                if (double.TryParse(item.RFCurrent, out double current)) rfCurrentSeries.Points.Add(new DataPoint(i, current));
                if (double.TryParse(item.RFVoltage, out double voltage)) rfVoltageSeries.Points.Add(new DataPoint(i, voltage));
                if (double.TryParse(item.Power1, out double power1)) power1Series.Points.Add(new DataPoint(i, power1));
                if (double.TryParse(item.Power2, out double power2)) power2Series.Points.Add(new DataPoint(i, power2));
            }

            // 添加所有曲线到模型
            foreach (var series in temperatureSeries)
            {
                model.Series.Add(series);
            }
            model.Series.Add(rfCurrentSeries);
            model.Series.Add(rfVoltageSeries);
            model.Series.Add(power1Series);
            model.Series.Add(power2Series);

            // 设置坐标轴
            model.Axes.Add(new LinearAxis 
            { 
                Position = AxisPosition.Bottom, 
                Title = "时间点",
                Minimum = 0,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });

            model.Axes.Add(new LinearAxis 
            { 
                Position = AxisPosition.Left, 
                Title = "参数值",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot
            });

            // 添加文本标注
            if (CollectionData.Count > 0)
            {
                // 为每条温度曲线添加标注
                for (int i = 0; i < temperatureSeries.Length; i++)
                {
                    var points = temperatureSeries[i].Points;
                    if (points != null && points.Count > 0)
                    {
                        var lastPoint = points[points.Count - 1];
                        var annotation = new TextAnnotation
                        {
                            Text = $"T{i + 1}",
                            TextPosition = new DataPoint(lastPoint.X + 1, lastPoint.Y),
                            StrokeThickness = 0,
                            TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left
                        };
                        model.Annotations.Add(annotation);
                    }
                }

                // 为电流、电压和功率曲线添加标注
                if (rfCurrentSeries.Points != null && rfCurrentSeries.Points.Count > 0)
                {
                    var lastPoint = rfCurrentSeries.Points[rfCurrentSeries.Points.Count - 1];
                    model.Annotations.Add(new TextAnnotation
                    {
                        Text = "射频电流",
                        TextPosition = new DataPoint(lastPoint.X + 1, lastPoint.Y),
                        StrokeThickness = 0,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left
                    });
                }

                if (rfVoltageSeries.Points != null && rfVoltageSeries.Points.Count > 0)
                {
                    var lastPoint = rfVoltageSeries.Points[rfVoltageSeries.Points.Count - 1];
                    model.Annotations.Add(new TextAnnotation
                    {
                        Text = "射频电压",
                        TextPosition = new DataPoint(lastPoint.X + 1, lastPoint.Y),
                        StrokeThickness = 0,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left
                    });
                }

                if (power1Series.Points != null && power1Series.Points.Count > 0)
                {
                    var lastPoint = power1Series.Points[power1Series.Points.Count - 1];
                    model.Annotations.Add(new TextAnnotation
                    {
                        Text = "功率1",
                        TextPosition = new DataPoint(lastPoint.X + 1, lastPoint.Y),
                        StrokeThickness = 0,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left
                    });
                }

                if (power2Series.Points != null && power2Series.Points.Count > 0)
                {
                    var lastPoint = power2Series.Points[power2Series.Points.Count - 1];
                    model.Annotations.Add(new TextAnnotation
                    {
                        Text = "功率2",
                        TextPosition = new DataPoint(lastPoint.X + 1, lastPoint.Y),
                        StrokeThickness = 0,
                        TextHorizontalAlignment = OxyPlot.HorizontalAlignment.Left
                    });
                }
            }

            PlotModel = model;
        }

        [RelayCommand]
        private async Task LoadData()
        {
            if (string.IsNullOrEmpty(SelectedCollection))
            {
                System.Windows.MessageBox.Show("请先选择一个工艺记录集合！");
                return;
            }

            try
            {
                var selectedRecord = ProcessRecords.FirstOrDefault(r => 
                    $"{r.ProcessName} {r.CreateTime:yyyy-MM-dd HH:mm:ss} ({r.CollectionName})" == SelectedCollection);

                if (selectedRecord == null)
                {
                    System.Windows.MessageBox.Show("未找到对应的工艺记录！");
                    return;
                }

                var processData = await MongoDbService.Instance.GetProcessDataByCollectionName(selectedRecord.CollectionName);
                CollectionData.Clear();
                foreach (var data in processData)
                {
                    CollectionData.Add(data);
                }

                if (IsPlotView)
                {
                    GeneratePlot();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"加载数据失败: {ex.Message}");
            }
        }
    }
} 