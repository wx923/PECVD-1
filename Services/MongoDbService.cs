using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using WpfApp4.Models;
using System.Windows;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;

namespace WpfApp4.Services
{
    public class MongoDbService
    {
        // 添加单例实例
        private static readonly Lazy<MongoDbService> _instance = 
            new Lazy<MongoDbService>(() => new MongoDbService());
        public static MongoDbService Instance => _instance.Value;

        //代表数据库
        public readonly IMongoDatabase _database;

        //代表数据库里面的集合
        private readonly IMongoCollection<Boat> _boats;
        private readonly IMongoCollection<BoatMonitor> _monitors;
        private readonly IMongoCollection<ProcessExcelModel> _processExcelCollection;
        private readonly IMongoCollection<ProcessFileInfo> _processFileCollection;
        private readonly IMongoCollection<FurnaceData> _furnaceCollection;
        private readonly IMongoCollection<Position> _positions;
        private readonly IMongoCollection<MotionBoatModel> _motionBoatModel;
        private readonly IMongoCollection<GlobalMonitoringStatusModel> _processFlowStepsCollection;


        // 全局数据集合
        public ObservableCollection<Boat> GlobalBoats { get; private set; }
        public ObservableCollection<BoatMonitor> GlobalMonitors { get; private set; }
        public ObservableCollection<ProcessFileInfo> GlobalProcessFiles { get; private set; }

        public ObservableCollection<Position> GlobalPositions { get; private set; }

        public ObservableCollection<MotionBoatModel> GlobalMotionBoats { get; private set; }

        public ObservableCollection<GlobalMonitoringStatusModel> GlobalProcessFlowSteps { get; private set; }

        private MongoDbService()
        {
            // 初始化全局集合
            GlobalBoats = new ObservableCollection<Boat>();
            GlobalMonitors = new ObservableCollection<BoatMonitor>();
            GlobalProcessFiles = new ObservableCollection<ProcessFileInfo>();
            GlobalPositions = new ObservableCollection<Position>();
            GlobalMotionBoats = new ObservableCollection<MotionBoatModel>();
            GlobalProcessFlowSteps = new ObservableCollection<GlobalMonitoringStatusModel>();
            try
            {
                var settings = MongoClientSettings.FromConnectionString("mongodb://localhost:27017");
                settings.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
                settings.ConnectTimeout = TimeSpan.FromSeconds(5);

                var client = new MongoClient(settings);

                // 测试连接
                client.ListDatabaseNames().FirstOrDefault();

                _database = client.GetDatabase("PECVD");

                // 确保集合存在，如果过集合不存在，则创建集合
                var collections = _database.ListCollectionNames().ToList();
                if (!collections.Contains("Boats"))
                    _database.CreateCollection("Boats");
                if (!collections.Contains("BoatMonitors"))
                    _database.CreateCollection("BoatMonitors");
                if (!collections.Contains("ProcessExcel"))
                    _database.CreateCollection("ProcessExcel");
                if (!collections.Contains("ProcessFiles"))
                    _database.CreateCollection("ProcessFiles");
                if (!collections.Contains("FurnaceData"))
                    _database.CreateCollection("FurnaceData");
                if (!collections.Contains("Positions"))
                {
                    _database.CreateCollection("Positions");
                }

                if (!collections.Contains("MotionBoats"))
                {
                    _database.CreateCollection("MotionBoats");
                }
                if (!collections.Contains("ProcessFlowStep"))
                {
                    _database.CreateCollection("ProcessFlowStep");
                }

                //获取集合
                _boats = _database.GetCollection<Boat>("Boats");
                _monitors = _database.GetCollection<BoatMonitor>("BoatMonitors");
                _processExcelCollection = _database.GetCollection<ProcessExcelModel>("ProcessExcel");
                _processFileCollection = _database.GetCollection<ProcessFileInfo>("ProcessFiles");
                _furnaceCollection = _database.GetCollection<FurnaceData>("FurnaceData");
                _positions = _database.GetCollection<Position>("Positions");
                _motionBoatModel = _database.GetCollection<MotionBoatModel>("MotionBoats");
                _processFlowStepsCollection = _database.GetCollection<GlobalMonitoringStatusModel>("ProcessFlowStep");
                // 初始化完成后加载数据
                _ = LoadAllDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MongoDB初始化失败: {ex.GetType().Name}\n详细信息: {ex.Message}");
                throw;
            }
        }

        public async Task LoadAllDataAsync()
        {
            try
            {
                // 清空现有数据
                GlobalBoats.Clear();
                GlobalMonitors.Clear();
                GlobalProcessFiles.Clear();
                GlobalPositions.Clear();
                GlobalMotionBoats.Clear();
                GlobalProcessFlowSteps.Clear();

                // 从数据库加载数据
                var boats = await _boats.Find(_ => true).ToListAsync();
                var monitors = await _monitors.Find(_ => true).ToListAsync();
                var processFiles = await _processFileCollection.Find(_ => true).ToListAsync();
                var positions= await _positions.Find(_ => true).ToListAsync();
                var motionBoats=await _motionBoatModel.Find(_ => true).ToListAsync();
                var processFlowSteps = await _processFlowStepsCollection.Find(_ => true).ToListAsync();
                // 更新集合并添加属性变更事件
                foreach (var boat in boats)
                {
                    boat.PropertyChanged += OnBoatPropertyChanged;
                    GlobalBoats.Add(boat);
                }

                foreach (var monitor in monitors)
                {
                    monitor.PropertyChanged += OnMonitorPropertyChanged;
                    GlobalMonitors.Add(monitor);
                }

                foreach (var processFile in processFiles)
                {
                    processFile.PropertyChanged += OnProcessFilePropertyChanged;
                    GlobalProcessFiles.Add(processFile);
                }

                foreach (var position in positions)
                {
                    position.PropertyChanged += OnPositionPropertyChanged;
                    GlobalPositions.Add(position);
                }
                foreach(var motionBoat in motionBoats)
                {
                    motionBoat.PropertyChanged += OnMotionBoatPropertyChanged;
                    GlobalMotionBoats.Add(motionBoat);
                }
                //如果对象为空的话，创建六个炉管的对象
                if (!processFlowSteps.Any()) 
                {
                        for (int i = 0; i < 6; i++){
                        var flowStep = new GlobalMonitoringStatusModel
                        {
                            Fnum = i,
                        };

                        await UpdataProcessFlowStepAsync(flowStep);
                        var filter = Builders<GlobalMonitoringStatusModel>.Filter.Eq(x => x.Fnum,i);
                        var a = _processFlowStepsCollection.Find(filter).FirstOrDefault();
                        GlobalProcessFlowSteps.Add(flowStep);
                    }
                }
                else {
                    foreach (var flowStep in processFlowSteps)
                    {
                        flowStep.PropertyChanged += OnProcessFlowStepChanged;
                        GlobalProcessFlowSteps.Add(flowStep);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载数据失败: {ex.Message}");
            }
        }



        #region 属性变更事件处理
        private async void OnBoatPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Boat boat)
            {
                try
                {
                    await UpdateBoatAsync(boat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"更新舟数据失败: {ex.Message}");
                }
            }
        }

        private async void OnMonitorPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is BoatMonitor monitor)
            {
                try
                {
                    await UpdateBoatMonitorAsync(monitor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"更新监控数据失败: {ex.Message}");
                }
            }
        }

        private async void OnProcessFilePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is ProcessFileInfo processFile)
            {
                try
                {
                    var filter = Builders<ProcessFileInfo>.Filter.Eq(x => x.Id, processFile.Id);
                    await _processFileCollection.ReplaceOneAsync(filter, processFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"更新工艺文件信息失败: {ex.Message}");
                }
            }
        }

        private async void OnPositionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is Position postion)
            {
                try
                {
                    await UpdatePositionAsync(postion);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"更新位置数据失败: {ex.Message}");
                }
            }
        }

        private async void OnMotionBoatPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is MotionBoatModel motionBoat)
            {
                try
                {
                    await UpdataMotionBoatAsync(motionBoat);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"更新舟对象数据失败:{ex.Message}");
                }
            }
        }



        private async void OnProcessFlowStepChanged(object? sender, PropertyChangedEventArgs e)
        {
            try 
            { if (sender is GlobalMonitoringStatusModel flowStep)
                    await UpdataProcessFlowStepAsync(flowStep);
            }
            catch (Exception ex) {
                MessageBox.Show($"更新工艺过程数据失败:{ex.Message}");
            }
        }

        #endregion

        #region Boat Operations
        // 获取所有舟对象
        public async Task<List<Boat>> GetAllBoatsAsync()
        {
            try
            {
                return await _boats.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取舟对象失败: {ex.Message}");
                return new List<Boat>();
            }
        }

        // 添加舟对象
        public async Task AddBoatAsync(Boat boat)
        {
            try
            {
                await _boats.InsertOneAsync(boat);
                boat.PropertyChanged += OnBoatPropertyChanged;  // 添加属性变更事件订阅
                GlobalBoats.Add(boat);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加舟对象失败: {ex.Message}");
            }
        }

        // 更新舟对象
        public async Task<bool> UpdateBoatAsync(Boat boat)
        {
            try
            {
                // 查找现有记录
                var filter = Builders<Boat>.Filter.Eq("monitorBoatNumber", boat.MonitorBoatNumber);
                var existingBoat = await _boats.Find(filter).FirstOrDefaultAsync();

                if (existingBoat != null)
                {
                    // 使用现有记录的 id
                    boat._id = existingBoat._id;
                    // 更新记录
                    var updateFilter = Builders<Boat>.Filter.Eq("_id", boat._id);
                    var result = await _boats.ReplaceOneAsync(updateFilter, boat);
                    return result.ModifiedCount > 0;
                }
                else
                {
                    // 如果是新记录，生成新的 id
                    boat._id = ObjectId.GenerateNewId().ToString();
                    await _boats.InsertOneAsync(boat);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新舟对象失败: {ex.Message}");
                return false;
            }
        }

        // 删除舟对象
        public async Task DeleteBoatAsync(string id)
        {
            try
            {
                var filter = Builders<Boat>.Filter.Eq("_id", id);
                await _boats.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除舟对象失败: {ex.Message}");
            }
        }
        #endregion

        #region 舟监控对象
        // 获取所有舟监控对象
        public async Task<List<BoatMonitor>> GetAllBoatMonitorsAsync()
        {
            try
            {
                return await _monitors.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取所有舟监控对象失败: {ex.Message}");
                return new List<BoatMonitor>();
            }
        }

        // 添加监控对象
        public async Task<bool> AddBoatMonitorAsync(BoatMonitor monitor)
        {
            try
            {
                await _monitors.InsertOneAsync(monitor);
                monitor.PropertyChanged += OnMonitorPropertyChanged;  // 添加属性变更事件订阅
                GlobalMonitors.Add(monitor);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加监控对象失败: {ex.Message}");
                return false;
            }
        }

        // 更新监控对象
        public async Task<bool> UpdateBoatMonitorAsync(BoatMonitor monitor)
        {
            try
            {
                var filter = Builders<BoatMonitor>.Filter.Eq("_id", monitor._id);
                var result = await _monitors.ReplaceOneAsync(filter, monitor);
                return result.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新监控对象失败: {ex.Message}");
                return false;
            }
        }

        // 删除监控对象
        public async Task<bool> DeleteBoatMonitorAsync(string id)
        {
            try
            {
                var filter = Builders<BoatMonitor>.Filter.Eq("_id", id);
                var result = await _monitors.DeleteOneAsync(filter);
                return result.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除监控对象失败: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region 工艺文件和工艺文件名操作

        public async Task UpdateProcessExcelAsync(string fileId, List<ProcessExcelModel> processes)
        {
            try
            {
                // 先获取文件信息
                var fileInfo = await _processFileCollection.Find(x => x.Id == fileId).FirstOrDefaultAsync();
                if (fileInfo == null) throw new Exception("未找到工艺文件信息");

                // 获取对应的集合
                var collection = _database.GetCollection<ProcessExcelModel>(fileInfo.CollectionName);
                
                // 删除集合中的所有现有数据
                await collection.DeleteManyAsync(_ => true);

                // 插入新数据
                if (processes.Any())
                {
                    await collection.InsertManyAsync(processes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"更新工艺Excel数据失败: {ex.Message}");
            }
        }
        // 保存新的工艺文件
        public async Task<string> SaveProcessFileAsync(string fileName, string description, List<ProcessExcelModel> data)
        {
            // 处理文件名以确保是有效的集合名（移除非法字符）
            string collectionName = fileName.Replace(" ", "_")  // 替换空格为下划线
                                          .Replace("-", "_")    // 替换横线为下划线
                                          .Replace(".", "_")    // 替换点为下划线
                                          .Replace("/", "_")    // 替换斜杠为下划线
                                          .Replace("\\", "_");  // 替换反斜杠为下划线

            var fileInfo = new ProcessFileInfo
            {
                FileName = fileName,
                CreateTime = DateTime.Now,
                Description = description,
                CollectionName = collectionName  // 使用处理后的文件名作为集合名
            };
            
            await _processFileCollection.InsertOneAsync(fileInfo);
            
            // 获取新的集合并插入数据
            var collection = _database.GetCollection<ProcessExcelModel>(fileInfo.CollectionName);
            await collection.InsertManyAsync(data);
            
            return fileInfo.Id;
        }

        // 获取所有工艺文件信息
        public async Task<List<ProcessFileInfo>> GetAllProcessFilesAsync()
        {
            return await _processFileCollection.Find(_ => true).ToListAsync();
        }

        // 获取指定工艺文件的数据
        public async Task<List<ProcessExcelModel>> GetProcessDataByFileIdAsync(string fileId)
        {
            // 先获取文件信息
            var fileInfo = await _processFileCollection.Find(x => x.Id == fileId).FirstOrDefaultAsync();
            if (fileInfo == null) return new List<ProcessExcelModel>();

            // 从对应的集合中获取数据
            var collection = _database.GetCollection<ProcessExcelModel>(fileInfo.CollectionName);
            return await collection.Find(_ => true).ToListAsync();
        }

        // 删除工艺文件及其数据
        public async Task DeleteProcessFileAsync(string fileId)
        {
            var fileInfo = await _processFileCollection.Find(x => x.Id == fileId).FirstOrDefaultAsync();
            if (fileInfo != null)
            {
                // 删除集合
                await _database.DropCollectionAsync(fileInfo.CollectionName);
                // 删除文件信息
                await _processFileCollection.DeleteOneAsync(x => x.Id == fileId);
            }
        }
        //判断是否有重复的集合名
        public async Task<bool> CollectionExistsAsync(string collectionName)
        {
            try
            {
                // 处理文件名以确保是有效的集合名（移除非法字符）
                string processedName = collectionName.Replace(" ", "_")  // 替换空格为下划线
                                                    .Replace("-", "_")    // 替换横线为下划线
                                                    .Replace(".", "_")    // 替换点为下划线
                                                    .Replace("/", "_")    // 替换斜杠为下划线
                                                    .Replace("\\", "_"); // 替换反斜杠为下划线

                var filter = new BsonDocument("name", processedName);
                var collections = await _database.ListCollectionsAsync(new ListCollectionsOptions { Filter = filter });
                return await collections.AnyAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"检查集合是否存在时出错: {ex.Message}");
            }
        }
        #endregion

        /// <summary>
        /// 重命名集合
        /// </summary>
        /// <param name="oldCollectionName">原集合名</param>
        /// <param name="newCollectionName">新集合名</param>
        public async Task RenameCollectionAsync(string oldCollectionName, string newCollectionName)
        {
            try
            {
                // 获取原集合
                var oldCollection = _database.GetCollection<ProcessExcelModel>(oldCollectionName);
                var data = await oldCollection.Find(_ => true).ToListAsync();

                // 创建新集合并复制数据
                var newCollection = _database.GetCollection<ProcessExcelModel>(newCollectionName);
                if (data.Any())
                {
                    await newCollection.InsertManyAsync(data);
                }

                // 删除旧集合
                await _database.DropCollectionAsync(oldCollectionName);
            }
            catch (Exception ex)
            {
                throw new Exception($"重命名集合失败: {ex.Message}");
            }
        }

        // 获取MongoDB集合
        public IMongoCollection<ProcessExcelModel> GetCollection(string collectionName)
        {
            return _database.GetCollection<ProcessExcelModel>(collectionName);
        }

        /// <summary>
        /// 从指定集合中获取所有工艺数据
        /// </summary>
        /// <param name="collectionName">集合名称</param>
        /// <returns>工艺数据列表</returns>
        public async Task<List<ProcessExcelModel>> GetProcessDataFromCollectionAsync(string collectionName)
        {
            try
            {
                // 获取指定名称的集合
                var collection = _database.GetCollection<ProcessExcelModel>(collectionName);
                
                // 获取集合中的所有数据
                var data = await collection.Find(_ => true).ToListAsync();
                
                return data ?? new List<ProcessExcelModel>();
            }
            catch (Exception ex)
            {
                throw new Exception($"从集合 {collectionName} 获取工艺数据失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 检查数据库连接状态
        /// </summary>
        /// <returns>如果连接正常返回true，否则返回false</returns>
        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                // 使用现有的数据库实例执行一个简单的命令来测试连接
                await _database.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        #region 坐标相关操作
        public async Task<bool> UpdatePositionAsync(Position position)
        {
            try
            {
                // 查找现有记录
                var filter = Builders<Position>.Filter.Eq("Location", position.Location);
                var existing = await _positions.Find(filter).FirstOrDefaultAsync();

                if (existing != null)
                {
                    // 更新记录
                    var updateFilter = Builders<Position>.Filter.Eq("Location", position.Location);
                    var result = await _positions.ReplaceOneAsync(updateFilter, position);
                    return result.ModifiedCount > 0;
                }
                else
                {
                    // 如果是新记录，生成新的 id
                    await _positions.InsertOneAsync(position);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"更新坐标数据失败: {ex.Message}");
                return false;
            }
        }
        #endregion



        #region 运动舟对象
        //更新运动舟对象
        public async Task<bool> UpdataMotionBoatAsync(MotionBoatModel motionBoat)
        {
            try
            {
                var filter = Builders<MotionBoatModel>.Filter.Eq("Location", motionBoat.Location);
                var existing = await _motionBoatModel.Find(filter).FirstOrDefaultAsync();

                if (existing != null)
                {
                    var result = await _motionBoatModel.ReplaceOneAsync(filter, existing);
                    return result.ModifiedCount > 0;
                }
                else {
                    await _motionBoatModel.InsertOneAsync(motionBoat);
                    return true;
                }
            }catch(Exception ex)
            {
                MessageBox.Show($"更新舟对象失败:{ex.Message}");
                return false;
            }
        }

        //删除数据库舟对象
        public async Task<bool> DeleteMotionBoatAsync(MotionBoatModel motionBoat)
        {
            try {
                var filter = Builders<MotionBoatModel>.Filter.Eq("Location",1);
                 var result= await _motionBoatModel.DeleteOneAsync(filter);
                if (result.DeletedCount > 0)
                {
                    MessageBox.Show($"删除舟对象成功");
                    return true;
                }
                else
                {
                    MessageBox.Show($"删除舟对象失败");
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"删除舟对象失败:{ex.Message}");
                return false;
            }
        }

        #endregion

        #region 工艺过程数据
        //创建六个对象

        //数据更新操作
        public async Task<bool> UpdataProcessFlowStepAsync(GlobalMonitoringStatusModel flowStep)
        {
            try
            {
                var filter = Builders<GlobalMonitoringStatusModel>.Filter.Eq("Fnum", flowStep.Fnum);
                var existing = await _processFlowStepsCollection.Find(filter).FirstOrDefaultAsync();

                if (existing !=null)
                {
                   var result = await _processFlowStepsCollection.ReplaceOneAsync(filter, flowStep);
                   return result.ModifiedCount > 0;
                }
                else
                {
                    await _processFlowStepsCollection.InsertOneAsync(flowStep);
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"更新舟对象失败:{ex.Message}");
                return false;
            }
        }
        #endregion
    }

}