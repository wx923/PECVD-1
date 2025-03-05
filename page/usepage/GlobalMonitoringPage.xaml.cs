using System.Windows.Controls;
using WpfApp4.ViewModel;


namespace WpfApp4.page.usepage
{
    /// <summary>
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class GlobalMonitoringPage : Page
    {
        public GlobalMonitoringPage(int tubeNumber)
        {
            this.DataContext = new GlobalMonitoringVM(tubeNumber);
            InitializeComponent();
        }
    }
}
