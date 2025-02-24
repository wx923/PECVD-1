using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HslCommunication.ModBus;
using WpfApp.Services;
using WpfApp4.Models;
using WpfApp4.Services;

namespace WpfApp4.ViewModel
{
    partial class  SpaceDataVM:ObservableObject
    {

        private readonly ModbusTcpNet _robotPlc = PlcCommunicationService.Instance.ModbusTcpClients[PlcCommunicationService.PlcType.Motion];

        [ObservableProperty]
        public string _spaceValue1;

        [ObservableProperty]
        public string _spaceValue2;

        [ObservableProperty]
        public string _spaceValue3;

        [ObservableProperty]
        public string _spaceValue4;

        [ObservableProperty]
        public string _spaceValue5;

        [ObservableProperty]
        public string _spaceValue6;

        [ObservableProperty]
        public string _spaceValue7;

        [ObservableProperty]
        public string _spaceValue8;

        [ObservableProperty]
        public string _spaceValue9;

        [ObservableProperty]
        public string _spaceValue10;

        [ObservableProperty]
        public string _spaceValue11;

        [ObservableProperty]
        public string _spaceValue12;

        [ObservableProperty]
        public string _spaceValue13;

        [ObservableProperty]
        public string _spaceValue14;

        [ObservableProperty]
        public string _spaceValue15;

        [ObservableProperty]
        public string _spaceValue16;

        [ObservableProperty]
        public string _spaceValue17;

        [ObservableProperty]
        public string _spaceValue18;

        [ObservableProperty]
        public string _spaceValue19;

        [ObservableProperty]
        public string _spaceValue20;

        [ObservableProperty]
        public string _spaceValue21;
        [ObservableProperty]
        public string _spaceValue22;

        [ObservableProperty]
        public string _spaceValue23;

        [ObservableProperty]
        public string _spaceValue24;

        [ObservableProperty]
        public string _spaceValue25;

        [ObservableProperty]
        public string _spaceValue26;
        [RelayCommand]
        public async Task UpdataPositionData(string LocationNum)
        {
            var num = int.Parse(LocationNum);
            switch (num)
            {
                case 1:
                    await UpdateOrAddXPositionAsync(1,SpaceValue1);
                    await _robotPlc.WriteAsync("100", SpaceValue1);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 2:
                    await UpdateOrAddXPositionAsync(2, SpaceValue2);
                    await _robotPlc.WriteAsync("100", SpaceValue2);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 3:
                    await UpdateOrAddXPositionAsync(3, SpaceValue3);
                    await _robotPlc.WriteAsync("100", SpaceValue3);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 4:
                    await UpdateOrAddXPositionAsync(4, SpaceValue4);
                    await _robotPlc.WriteAsync("100", SpaceValue4);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 5:
                    await UpdateOrAddXPositionAsync(5, SpaceValue5);
                    await _robotPlc.WriteAsync("100", SpaceValue5);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 6:
                    await UpdateOrAddXPositionAsync(6, SpaceValue6);
                    await _robotPlc.WriteAsync("100", SpaceValue6);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 7:
                    await UpdateOrAddXPositionAsync(7, SpaceValue7);
                    await _robotPlc.WriteAsync("100", SpaceValue7);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 8:
                    await UpdateOrAddXPositionAsync(8, SpaceValue8);
                    await _robotPlc.WriteAsync("100", SpaceValue8);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 9:
                    await UpdateOrAddXPositionAsync(9, SpaceValue9);
                    await _robotPlc.WriteAsync("100", SpaceValue9);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 10:
                    await UpdateOrAddXPositionAsync(10, SpaceValue10);
                    await _robotPlc.WriteAsync("100", SpaceValue10);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 11:
                    await UpdateOrAddXPositionAsync(11, SpaceValue11);
                    await _robotPlc.WriteAsync("100", SpaceValue11);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 12:
                    await UpdateOrAddXPositionAsync(12, SpaceValue12);
                    await _robotPlc.WriteAsync("100", SpaceValue12);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 13:
                    await UpdateOrAddXPositionAsync(13, SpaceValue13);
                    await _robotPlc.WriteAsync("100", SpaceValue13);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 14:
                    await UpdateOrAddYPositionAsync(1, SpaceValue14);
                    await _robotPlc.WriteAsync("100", SpaceValue14);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 15:
                    await UpdateOrAddYPositionAsync(2, SpaceValue15);
                    await _robotPlc.WriteAsync("100", SpaceValue15);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 16:
                    await UpdateOrAddYPositionAsync(3, SpaceValue16);
                    await _robotPlc.WriteAsync("100", SpaceValue16);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 17:
                    await UpdateOrAddYPositionAsync(4, SpaceValue17);
                    await _robotPlc.WriteAsync("100", SpaceValue17);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 18:
                    await UpdateOrAddYPositionAsync(5, SpaceValue18);
                    await _robotPlc.WriteAsync("100", SpaceValue18);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 19:
                    await UpdateOrAddYPositionAsync(6, SpaceValue19);
                    await _robotPlc.WriteAsync("100", SpaceValue19);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 20:
                    await UpdateOrAddYPositionAsync(7, SpaceValue20);
                    await _robotPlc.WriteAsync("100", SpaceValue20);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 21:
                    await UpdateOrAddYPositionAsync(8, SpaceValue21);
                    await _robotPlc.WriteAsync("100", SpaceValue21);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 22:
                    await UpdateOrAddYPositionAsync(9, SpaceValue22);
                    await _robotPlc.WriteAsync("100", SpaceValue22);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 23:
                    await UpdateOrAddYPositionAsync(10, SpaceValue23);
                    await _robotPlc.WriteAsync("100", SpaceValue23);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 24:
                    await UpdateOrAddYPositionAsync(11, SpaceValue24);
                    await _robotPlc.WriteAsync("100", SpaceValue24);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 25:
                    await UpdateOrAddYPositionAsync(12, SpaceValue25);
                    await _robotPlc.WriteAsync("100", SpaceValue25);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case 26:
                    await UpdateOrAddYPositionAsync(13, SpaceValue26);
                    await _robotPlc.WriteAsync("100", SpaceValue26);
                    MessageBox.Show("操作成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }
        }

        //x值的调用
        public async Task UpdateOrAddXPositionAsync(int loc, string spaceValue) {
            var filteredPositions = MongoDbService.Instance.GlobalPositions.FirstOrDefault(p => p.Location == loc);
            if (filteredPositions != null)
            {
                filteredPositions.X = float.Parse(SpaceValue1);
            }
            else
            {
                filteredPositions = new Position
                {
                    Location = loc,
                    X = float.Parse(spaceValue),
                    Y = 0
                };
                await MongoDbService.Instance.UpdatePositionAsync(filteredPositions);
                MongoDbService.Instance.GlobalPositions.Add(filteredPositions); // 添加到集合中
            }
        }

        //Y值的调用
        public async Task UpdateOrAddYPositionAsync(int loc, string spaceValue)
        {
            var filteredPositions = MongoDbService.Instance.GlobalPositions.FirstOrDefault(p => p.Location == loc);
            if (filteredPositions != null)
            {
                filteredPositions.Y = float.Parse(SpaceValue1);
            }
            else
            {
                filteredPositions = new Position
                {
                    Location = loc,
                    X = 0,
                    Y = float.Parse(spaceValue)
                };
                await MongoDbService.Instance.UpdatePositionAsync(filteredPositions);
                MongoDbService.Instance.GlobalPositions.Add(filteredPositions); // 添加到集合中
            }
        }

    }
}
