using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp4.Models
{
    public partial class Position:ObservableObject
    {
        [ObservableProperty]
        public int x;

        [ObservableProperty]
        public int y;

        [ObservableProperty]
        public int location;
    }
}
