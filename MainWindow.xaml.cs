using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfApp4.page.usepage;
using WpfApp4.Services;
using WpfApp4.Services.WpfApp4.Services;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //定时器对象
        private DispatcherTimer _timer;
        //用于按钮状态切换
        private Button LastButtonUI;
        private int CurrentTubeNumber = 1; // 当前选中的炉管号
        private Dictionary<(Type PageType,int TubeNumber),Page> _pageCache = new Dictionary<(Type, int), Page>();

        public MainWindow()
        {
            InitializeComponent();

            _=MongoDbService.Instance;
            _ = GlobalMonitoringService.Instance;
            _ = AlarmService.Instance;

            // 默认导航到 HomePage（不依赖炉管，使用 TubeNumber = 0）
            MainFrame.Navigate(GetOrCreatePage(typeof(HomePage), 0));
            LastButtonUI = BtnHome;
            LastButtonUI.Style = (Style)FindResource("TopNavigationSelectedButtonStyle");
            
            // 初始化时间显示
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            
            // 默认选中炉管1
            BtnFurnaceTube1.IsChecked = true;
            
            // 添加炉管选择事件处理
            BtnFurnaceTube1.Checked += FurnaceTube_Checked;
            BtnFurnaceTube2.Checked += FurnaceTube_Checked;
            BtnFurnaceTube3.Checked += FurnaceTube_Checked;
            BtnFurnaceTube4.Checked += FurnaceTube_Checked;
            BtnFurnaceTube5.Checked += FurnaceTube_Checked;
            BtnFurnaceTube6.Checked += FurnaceTube_Checked;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeTextBlock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void FurnaceTube_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                string content = radioButton.Content.ToString();
                CurrentTubeNumber = int.Parse(content.Replace("炉管", ""));
                
                // 获取当前页面并刷新
                RefreshCurrentPage();
            }
        }


        /// <summary>
        /// 刷新当前页面的mainFrame的区域显示
        /// </summary>
        private void RefreshCurrentPage()
        {
            if(MainFrame.Content  is Page currentPage)
            {
                Type pageType=currentPage.GetType();
                MainFrame.Navigate(GetOrCreatePage(pageType, CurrentTubeNumber));
            }
        }

        private void NavigateToPage(Type pageType, int tubeNumber, Button button)
        {
            //对于部分页面需要进行炉管选择才能进行跳转
            if(pageType != typeof(HomePage) && pageType != typeof(MotionControlPage) &&
            pageType != typeof(BoatManagementPage) && pageType != typeof(ProcessManagementPage))
            {
                if (!CheckTubeSelected())return;
                
            }

            MainFrame.Navigate(GetOrCreatePage(pageType, tubeNumber));
            LastButtonUI.Style = (Style)FindResource("TopNavigationButtonStyle");
            LastButtonUI = button;
            button.Style = (Style)FindResource("TopNavigationSelectedButtonStyle");
        }

        // 检查是否选择了炉管
        private bool CheckTubeSelected()
        {
            if (!BtnFurnaceTube1.IsChecked == true && 
                !BtnFurnaceTube2.IsChecked == true && 
                !BtnFurnaceTube3.IsChecked == true && 
                !BtnFurnaceTube4.IsChecked == true && 
                !BtnFurnaceTube5.IsChecked == true && 
                !BtnFurnaceTube6.IsChecked == true)
            {
                MessageBox.Show("请先选择炉管！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                BtnFurnaceTube1.IsChecked = true; // 默认选中炉管1
                return false;
            }
            return true;
        }

        //使用页面路由跳转
        // 导航方法调用统一入口
        private void NavigateToParameterSettingPage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(ParameterSettingPage), CurrentTubeNumber, BtnParameterSetting);
        }

        private void NavigateToMotionControlPage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(MotionControlPage), 0, BtnMotionControl);
        }

        private void NavigateToControlInterfacePage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(ControlInterfacePage), CurrentTubeNumber, BtnControlInterface);
        }

        private void NavigateToGlobalMonitoringPage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(GlobalMonitoringPage), CurrentTubeNumber, BtnGlobalMonitoring);
        }

        private void NavigateToProcessMonitoringPage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(ProcessMonitoringPage), CurrentTubeNumber, BtnProcessMonitoring);
        }

        private void NavigateToMonitoringAlarmPage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(AlarmPage), CurrentTubeNumber, BtnMonitoringAlarm);
        }

        private void NavigateToHomePage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(HomePage), 0, BtnHome);
        }

        private void NavigateToBoatManagementPage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(BoatManagementPage), 0, BtnBoatManagement);
        }

        private void NavigateToProcessManagementPage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(ProcessManagementPage), 0, BtnProcessManagement);
        }

        private void NavigateToDataShowPage(object sender, RoutedEventArgs e)
        {
            NavigateToPage(typeof(DataShowPage), CurrentTubeNumber, BtnDataShow);
        }

        //从字典中获取页面
        private Page GetOrCreatePage(Type pageType,int tubeNumber)
        {
            var key = (pageType, tubeNumber);
            if(!_pageCache.TryGetValue(key,out Page page))
            {
                if (pageType == typeof(ParameterSettingPage))
                    page = new ParameterSettingPage(tubeNumber);
                else if (pageType == typeof(ControlInterfacePage))
                    page = new ControlInterfacePage(tubeNumber);
                else if (pageType == typeof(GlobalMonitoringPage))
                    page = new GlobalMonitoringPage(tubeNumber);
                else if (pageType == typeof(ProcessMonitoringPage))
                    page = new ProcessMonitoringPage(tubeNumber);
                else if (pageType == typeof(AlarmPage))
                    page = new AlarmPage(tubeNumber);
                else if (pageType == typeof(DataShowPage))
                    page = new DataShowPage(tubeNumber);
                else if (pageType == typeof(MotionControlPage))
                    page = new MotionControlPage();
                else if (pageType == typeof(HomePage))
                    page = new HomePage();
                else if (pageType == typeof(BoatManagementPage))
                    page = new BoatManagementPage();
                else if (pageType == typeof(ProcessManagementPage))
                    page = new ProcessManagementPage();

                _pageCache[key] = page;
            }
            return page;
        }
    }
}