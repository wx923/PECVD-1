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
        public float _x;

        [ObservableProperty]
        public float _y;

        [ObservableProperty]
        public int _location;
    }
}
