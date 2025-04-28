using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp4.Models
{
    // 工艺流程记录信息集合
    public class ProcessRecordInfo : ObservableObject
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _tubeNumber;
        public int TubeNumber
        {
            get => _tubeNumber;
            set => SetProperty(ref _tubeNumber, value);
        }

        private string _processName;
        public string ProcessName
        {
            get => _processName;
            set => SetProperty(ref _processName, value);
        }

        private DateTime _createTime;
        public DateTime CreateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }

        private string _collectionName;
        public string CollectionName
        {
            get => _collectionName;
            set => SetProperty(ref _collectionName, value);
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get => _isCompleted;
            set => SetProperty(ref _isCompleted, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
    }
} 