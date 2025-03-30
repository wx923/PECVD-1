using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WpfApp4.ViewModel;

namespace WpfApp4.page.usepage
{
    /// <summary>
    /// Page8.xaml 的交互逻辑
    /// </summary>

    public partial class AlarmPage : Page
    {
        public AlarmPage(int tubeNumber)
        {

            InitializeComponent();
            this.DataContext = new AlermVm(tubeNumber-1);
        }
    }
}
