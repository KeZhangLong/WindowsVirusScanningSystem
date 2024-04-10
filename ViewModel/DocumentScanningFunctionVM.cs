using System.Windows;
using Ookii.Dialogs.Wpf;
using System.Windows.Input;
using WindowsVirusScanningSystem.Model;
using WindowsVirusScanningSystem.Utilities;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;
using System.Data;
using System.Windows.Documents;
using System.Collections.Generic;

namespace WindowsVirusScanningSystem.ViewModel
{
    enum ScanType
    {
        扫描成功,
        疑似病毒,
        发生错误,
        终止扫描
    }
    class DocumentScanningFunctionVM : Utilities.ViewModelBase
    {
        private string btnState = "病毒检测";

        public string BtnState
        {
            get { return btnState; }
            set { btnState = value; OnPropertyChanged(); }
        }

        //进度条值
        private int progress = 0;
        public int Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyChanged(); }
        }

        //进度条总值
        private int count = int.MaxValue;
        public int Count
        {
            get { return count; }
            set { count = value; OnPropertyChanged(); }
        }

        //公用模型
        private readonly PageModel _pageModel;

        //Listbox.Item
        public ObservableCollection<ItemViewModel> Results { get; set; }

      

        //查询文件夹的路径
        public string? folderPath
        {
            get { return _pageModel.FolderPath; }
            set { _pageModel.FolderPath = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 搜索到的文件夹数目
        /// </summary>
        public int FoldersSearched
        {
            get { return _pageModel.FoldersSearched; }
            set { _pageModel.FoldersSearched = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 搜索到的文件数目
        /// </summary>
        public int FilesSearched
        {
            get { return _pageModel.FilesSearched; }
            set { _pageModel.FilesSearched = value; OnPropertyChanged(); }
        }


        public string? CurrentlySearching
        {
            get { return _pageModel.CurrentlySearching; }
            set { _pageModel.CurrentlySearching = value; OnPropertyChanged(); }
        }

        public string? CurrentlyScanning
        {
            get { return _pageModel.CurrentlyScanning; }
            set { _pageModel.CurrentlyScanning = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 是否正在扫描（病毒）
        /// </summary>
        public bool IsScanning
        {
            get { return _pageModel.IsScanning; }
            set { _pageModel.IsScanning = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 能否进行扫描（病毒）
        /// </summary>
        public bool CanScanning
        {
            get { return _pageModel.CanScanning; }
            set { _pageModel.CanScanning = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 是否正在搜索
        /// </summary>
        public bool IsSearching
        {
            get { return _pageModel.IsSearching; }
            set { _pageModel.IsSearching = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 是否取消了搜索任务
        /// </summary>
        public bool CanSearch
        {
            get { return _pageModel.CanSearch; }
            set { _pageModel.CanSearch = value; OnPropertyChanged(); }
        }

        public bool CaseSensitive
        {
            get { return _pageModel.CaseSensitive; }
            set { _pageModel.CaseSensitive = value; OnPropertyChanged(); }
        }

        public bool SearchRecursive
        {
            get { return _pageModel.SearchRecursive; }
            set { _pageModel.SearchRecursive = value; OnPropertyChanged(); }
        }

        ObservableCollection<string> WhiteList = null;
        
        public DocumentScanningFunctionVM()
        {
            Results = new ObservableCollection<ItemViewModel>();

            _pageModel = new PageModel();

            WhiteTableReady();

            //通过文件路径扫描文件夹内容
            //SearchFolderPathCommand = new RelayCommand(SearchFolderPath);

            SearchFolderContentCommand = new RelayCommand(SearchFolderPath);

            VirusDetectionCommand = new RelayCommand(VirusDetection);
        }

        //public ICommand SearchFolderPathCommand { get; set; }
        public ICommand SearchFolderContentCommand { get; set; }
        public ICommand VirusDetectionCommand { get; set; }

        /// <summary>
        /// 准备白名单字典
        /// </summary>
        private void WhiteTableReady()
        {
            DataTable WhiteTable = SQLiteHelper.Instance.GetFileWhiteListData();

            WhiteList = new ObservableCollection<string>();

            Task.Run(() =>
            {
                for(int i=0;i<WhiteTable.Rows.Count;i++)
                {
                    WhiteList.Add(WhiteTable.Rows[i][1].ToString());
                }
            });
        }

        /// <summary>
        /// 查找要搜索的文件夹路径
        /// </summary>
        /// <param name="o"></param>
        private void SearchFolderPath(object o)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.UseDescriptionForTitle = true;
            fbd.Description = "请选择一个文件夹";
            if (fbd.ShowDialog() == true)
            {
                if (IsDirectory(fbd.SelectedPath))
                {
                    //扫描路径
                    folderPath = fbd.SelectedPath;
                    //初始化进度条的总值、初始值
                    InitProgress();
                    //扫描文件夹内容
                    SearchFolderContent();
                }
                else
                {
                    MessageBox.Show("选择的文件夹不存在");
                }
            }
        }

        private void InitProgress()
        {
            Count = int.MaxValue;
            Progress = 0;
        }

        /// <summary>
        /// 病毒扫描
        /// </summary>
        /// <param name="o"></param>
        private void VirusDetection(object o)
        {
            if (BtnState == "病毒检测")
            {
                //正在查询文件，不能病毒检查
                if (IsSearching)
                {
                    return;
                }

                //已经扫描一次
                if(Progress == Count)
                {
                    return;
                }

                //先停止之前的搜索
                StopScanning();

                //检测文件或文件夹数量，小于等于0直接返回
                if (Results.Count <= 0)
                {
                    return;
                }

                //设置为启动模式
                SetScanningStatus(true);

                BtnState = "取消病毒检测";

                var t1 = Task.Run(() =>
                {
                    ScanModule();

                    //完成后，状态设置为false
                    SetScanningStatus(false);
                });
            }
            else
            {
                //先停止之前的搜索
                StopScanning();

                BtnState = "病毒检测";

                InitProgress();

                MessageBox.Show("已停止扫描");
            }
        }

        //扫描病毒模块
        private void ScanModule()
        {
            try
            {
                int virusCount = 0;

                //文件+文件夹数量
                Count = Results.Count;

                //遍历Results
                for (int i = 0; i < Results.Count; i++)
                {
                    if (!CanScanning) return;

                    if (Results[i].IsSkipScan)//跳过扫描
                    {
                        SkipScan(Results[i]);
                    }
                    else
                    {
                        //检测是不是文件夹，如果是则使用递归
                        if (Results[i].Type == FileType.Folder)//文件夹需要进去进行递归
                        {
                            int vCount = 0;

                            ScanType DirectorySearch(string filePath)
                            {
                                //开始之前先设置变量
                                ScanType result = ScanType.扫描成功;

                                //这将循环遍历每个文件夹，然后做同样的事情;
                                foreach (string folder in Directory.GetDirectories(filePath))//检测文件夹内是否还有文件夹
                                {
                                    if (!CanScanning) return ScanType.终止扫描;

                                    foreach (string file in Directory.GetFiles(folder))
                                    {
                                        // Cancel search if needed
                                        if (!CanScanning) return ScanType.终止扫描;

                                        result = FileMd5Culator(Results[i],file);

                                        if (result == ScanType.疑似病毒)
                                        {
                                            virusCount++;
                                            vCount++;
                                        }
                                        if(result == ScanType.发生错误)
                                        {
                                            return result;
                                        }
                                    }

                                    //继续查看还有没有文件夹
                                     return DirectorySearch(folder);//递归
                                }

                                return ScanType.扫描成功;
                            }

                            ScanType result = DirectorySearch(Results[i].FilePath);

                            //标识扫描成功的话进入
                            if (result != ScanType.扫描成功)
                            {
                                Results[i].IsScanComplete = result.ToString();
                                Results[i].IsScanCompleteColor = Brushes.Red;
                            }

                            //文件夹内文件数为0
                            if (Directory.GetFiles(Results[i].FilePath).Length == 0)
                            {
                                Results[i].IsScanComplete = "扫描成功";
                                Results[i].IsScanCompleteColor = Brushes.Green;
                            }
                            else
                            {
                                foreach (string file in Directory.GetFiles(Results[i].FilePath))//文件夹内的文件
                                {
                                    if (!CanScanning)
                                    {
                                        Results[i].IsScanComplete = ScanType.终止扫描.ToString();
                                        return;
                                    }

                                    ScanType r = FileMd5Culator(Results[i], file);

                                    if (r != ScanType.扫描成功)
                                    {
                                        Results[i].IsScanComplete = r.ToString();
                                        Results[i].IsScanCompleteColor = Brushes.Red;
                                    }
                                    if(r == ScanType.疑似病毒)
                                    {
                                        virusCount++;
                                        vCount++;
                                    }
                                }

                                if (vCount == 0)
                                {
                                    Results[i].IsScanComplete = "扫描完成";
                                    Results[i].IsScanCompleteColor = Brushes.Green;
                                }
                            }
                        }
                        else//文件
                        {
                            FileMd5Culator(Results[i],ref virusCount);
                        }

                        //再去数据库进行匹配
                    }

                    Progress++;
                }

                if(Count == Progress)
                {
                    MessageBox.Show("扫描完成");

                    BtnState = "病毒检测";

                    //扫描完成后，将数据保存到数据库
                    SQLiteHelper.Instance.InsertScanVirusData(DateTime.Now.ToString("yyyyMMMddHHmmssfff"), folderPath, FilesSearched.ToString(),
                        FoldersSearched.ToString(), virusCount.ToString(), DateTime.Now.ToString("s"));
                }

                StopScanning();//停止搜索
            }
            catch (Exception ex)
            {
                StopScanning();//停止搜索
                throw new Exception($"病毒扫描错误{ex.Message}");
            }
        }

        private void SkipScan(ItemViewModel item)
        {
            item.IsScanComplete = "跳过扫描";
            //item.IsScanCompleteColor = Brushes.Green;
        }

        private void FileMd5Culator(ItemViewModel item , ref int virusCount)
        {
            DataResult<string> r = HashHelper.ComputeMD5(item.FilePath);

            if (r.IsSuccess)
            {
                item.MD5Str = r.Value;//文件的MD5值

                bool result = VirusDetect(r.Value);//病毒检测结果

                if (result)//不是病毒
                {
                    item.IsScanComplete = "扫描完成";
                    item.IsScanCompleteColor = Brushes.Green;
                }
                else
                {
                    virusCount++;
                    item.IsScanComplete = "疑似病毒";
                    item.IsScanCompleteColor = Brushes.Red;
                }
            }
            else
            {
                item.MD5Str = string.Empty;//文件的MD5值
                item.IsScanComplete = "发生错误";
                item.IsScanCompleteColor = Brushes.Red;
            }
        }

        private bool VirusDetect(string FileMD5)
        {
            int result = SQLiteHelper.Instance.GetVirusSampleDataByValue(FileMD5);

            if(result != 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 文件MD5计算
        /// </summary>
        /// <param name="item"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private ScanType FileMd5Culator(ItemViewModel item,string filePath)
        {
            DataResult<string> r = HashHelper.ComputeMD5(filePath);

            //计算成功
            if (r.IsSuccess)
            {
                bool result = VirusDetect(r.Value);//病毒检测结果

                if (result)//不是病毒
                {
                    return ScanType.扫描成功;
                }
                else
                {
                    return ScanType.疑似病毒;
                }
            }
            else//大概率文件被占用，无法计算
            {
                MessageBox.Show(r.Message);
                return ScanType.发生错误;
            }
        }

        /// <summary>
        /// 搜索文件夹路径内的文件跟文件夹
        /// </summary>
        private void SearchFolderContent()
        {
            try
            {
                // This method will be async, so cancel any search currenly active
                // Although according to the view logic that shouldn't be possible
                // To do multiple searches because the search button becomes disabled.
                //这个方法将是异步的，所以取消任何当前活动的搜索
                //虽然根据视图逻辑，这应该是不可能的
                //执行多个搜索，因为搜索按钮被禁用。

                //先停止之前的搜索
                CancelSearch();

                //先停止之前的扫描（病毒）
                StopScanning();

                //搜索到的文件、文件夹数目清零
                ClearSearchCounters();

                if (!string.IsNullOrEmpty(folderPath))//判断文件夹是否为空
                {
                    if (IsDirectory(folderPath))
                    {
                        Clear();

                        SetSearchingStatus(true);

                        Task.Run(() =>
                        {
                            StartSearchNonRecursively();

                            //完成后，扫描状态设置未false
                            SetSearchingStatus(false);
                        });
                    }
                }
            }
            catch (Exception e) { MessageBox.Show($"{e.Message} -- Cancelling Search"); CancelSearch(); }
        }

        public void StartSearchNonRecursively()
        {
            //文件夹
            foreach (string folder in Directory.GetDirectories(folderPath))
            {
                if (!CanSearch) return;

                SearchFolderName(folder);
            }

            //文件
            foreach (string file in Directory.GetFiles(folderPath))
            {
                if (!CanSearch) return;
                bool hasFoundFile = SearchFileName(file);

                // Again this stops items from being searched when they've
                // Already been found in the above code from the name.
                if (!hasFoundFile)
                {
                    ReadAndSearchFile(file, false);
                }
            }

        }

        public void ReadAndSearchFile(string file, bool increaseSearchedFiles)
        {
            try
            {
                CurrentlySearching = file;

                // FileStreams are better because they wont load
                // The entire file into memory which is very good
                // If the file to be searched is maybe 1 gigabyte.
                using (FileStream fs = File.OpenRead(file))
                {
                    // Read the file in chunks of 1kb.
                    byte[] b = new byte[1024];
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        // Cancels the search if CanSearch is false
                        if (!CanSearch) return;

                        // Get text from buffer
                        string txt = Encoding.ASCII.GetString(b);

                        ResultFound(file);
                        break;
                    }
                }

                if (increaseSearchedFiles)
                {
                    FilesSearched++;
                }
            }
            catch (Exception e) { MessageBox.Show($"{e.Message} -- Cancelling Search"); CancelSearch(); }
        }

        public bool SearchFileName(string name)
        {
            CurrentlySearching = name;
            string fPath = name;
            ResultFound(fPath);
            FilesSearched++;
            return true;
        }

        /// <summary>
        /// 清除上一次搜索到的数据
        /// </summary>
        public void Clear()
        {
            Results.Clear();
        }

        /// <summary>
        /// 搜索到的文件、文件夹数目清零
        /// </summary>
        public void ClearSearchCounters()
        {
            FilesSearched = 0;
            FoldersSearched = 0;
        }

        /// <summary>
        /// 停止扫描（病毒）
        /// </summary>
        public void StopScanning()
        {
            SetScanningStatus(false);
        }

        /// <summary>
        /// 设置扫描（病毒）状态
        /// </summary>
        /// <param name="isScanning"></param>
        public void SetScanningStatus(bool isScanning)
        {
            IsScanning = isScanning;
            CanScanning = isScanning;
            CurrentlyScanning = "";
        }

        /// <summary>
        /// 停止搜索
        /// </summary>
        public void CancelSearch()
        {
            SetSearchingStatus(false);
        }

        /// <summary>
        /// 设置搜索的状态：true（正在进行）、false（停止）
        /// </summary>
        /// <param name="isSearching"></param>
        public void SetSearchingStatus(bool isSearching)
        {
            IsSearching = isSearching;
            CanSearch = isSearching;
            CurrentlySearching = "";
        }

        /// <summary>
        /// 查询文件夹名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public bool SearchFolderName(string name)
        {
            string dPath = name;

            ResultFound(dPath);
            FoldersSearched++;
            return true;
        }

        public void ResultFound(string path)
        {
            ItemViewModel result = CreateResultFromPath(path);
            if (result != null)
                AddResultAsync(result);
        }

        //异步增加ListBox.Item
        public void AddResultAsync(ItemViewModel result)
        {
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                Results.Add(result);
            });
        }

        //创建ListBox.Item
        public ItemViewModel CreateResultFromPath(string path)
        {
            if (IsFile(path))
            {
                try
                {
                    FileInfo fInfo = new FileInfo(path);
                    ItemViewModel result = new ItemViewModel()
                    {
                        Image = IconHelper.GetIconOfFile(path, false, false),
                        FileName = fInfo.Name,
                        FilePath = fInfo.FullName,
                        FileSizeBytes = fInfo.Length,
                        IsScanCompleteColor = Brushes.White,
                        IsScanComplete = "未扫描",
                        IsSkipScan = WhiteList.Contains(path) ? true : false,//是否加入白名单
                        IsPE = false,
                        Type = FileType.File
                    };

                    return result;
                }
                catch (Exception e) { MessageBox.Show($"{e.Message}"); return null; }
            }

            else if (IsDirectory(path))
            {
                try
                {
                    DirectoryInfo dInfo = new DirectoryInfo(path);
                    ItemViewModel result = new ItemViewModel()
                    {
                        Image = IconHelper.GetIconOfFile(path, false, true),
                        FileName = dInfo.Name,
                        FilePath = dInfo.FullName,
                        IsScanCompleteColor = Brushes.White,
                        IsScanComplete = "未扫描",
                        IsSkipScan = false,
                        IsPE = false,
                        FileSizeBytes = long.MaxValue,
                        Type = FileType.Folder
                    };

                    return result;
                }
                catch (Exception e) { MessageBox.Show($"{e.Message}"); return null; }
            }

            return null;
        }

        #region//文件的检查、查询等功能
        /// <summary>
        /// Checks if the path is a file
        /// 检查路径是否为文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFile(string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path);
        }

        /// <summary>
        /// 判断路径是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectory(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        /// <summary>
        /// Checks if a path is a drive
        /// 检查路径是否为驱动器
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDrive(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }

        /// <summary>
        /// Gets the name of a file within a path
        /// 获取路径中文件的名称
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static string GetFileName(string fullpath)
        {
            return System.IO.Path.GetFileName(fullpath);
        }

        /// <summary>
        /// 返回目录/文件夹的名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetDirectoryName(string path)
        {
            return IsDirectory(path) ? new DirectoryInfo(path).Name : "";
        }
        #endregion
    }
}
