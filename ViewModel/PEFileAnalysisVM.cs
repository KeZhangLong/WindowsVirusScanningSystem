using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using WindowsVirusScanningSystem.Model;
using WindowsVirusScanningSystem.Utilities;
using WpfHexaEditor.Core.MethodExtention;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsVirusScanningSystem.ViewModel
{
    public class PEFileAnalysisVM : ViewModelBase
    {
        private readonly PageModel _pageModel;
        public ObservableCollection<ItemViewModel> Results { get; set; }

        public ObservableCollection<SearchRecordItem> SearchRecordResults { get; set; }

        //查询文件夹的路径
        public string? folderPath
        {
            get { return _pageModel.FolderPath; }
            set { _pageModel.FolderPath = value; OnPropertyChanged(); }
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

        /// <summary>
        /// 是否取消了搜索任务
        /// </summary>
        public bool CanSearch
        {
            get { return _pageModel.CanSearch; }
            set { _pageModel.CanSearch = value; OnPropertyChanged(); }
        }
        public string? CurrentlySearching
        {
            get { return _pageModel.CurrentlySearching; }
            set { _pageModel.CurrentlySearching = value; OnPropertyChanged(); }
        }
        public bool CaseSensitive
        {
            get { return _pageModel.CaseSensitive; }
            set { _pageModel.CaseSensitive = value; OnPropertyChanged(); }
        }

        public PEFileAnalysisVM()
        {
            _pageModel = new PageModel();

            Results = new ObservableCollection<ItemViewModel>();

            SearchRecordResults = new ObservableCollection<SearchRecordItem>();

            SearchFileOfFolderPathCommand = new RelayCommand(SearchFileOfFolderPath);

            ItemDoubleClickCommand = new RelayCommand(ItemDoubleClick);

            RefreshDbData();
        }

        private void RefreshDbData()
        {
            DataTable dt = SQLiteHelper.Instance.GetDataTable("2");

            int RowCount = dt.Rows.Count;

            SearchRecordResults.Clear();

            for (int i = 0; i < RowCount; i++)
            {
                SearchRecordResults.Add(new SearchRecordItem(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(),null));
            }
        }

       

        public ICommand SearchFileOfFolderPathCommand { get; set; }
        public ICommand SearchFolderContentCommand { get; set; }

        public ICommand ItemDoubleClickCommand { get; set; }

        private void ItemDoubleClick(object o)
        {
            int selectIndex = (int)o;
        }

        /// <summary>
        /// 查找要搜索的文件夹路径
        /// </summary>
        /// <param name="o"></param>
        private void SearchFileOfFolderPath(object o)
        {
            VistaFolderBrowserDialog fbd = new VistaFolderBrowserDialog();
            fbd.UseDescriptionForTitle = true;
            fbd.Description = "请选择一个文件夹";
            if (fbd.ShowDialog() == true)
            {
                if (IsDirectory(fbd.SelectedPath))
                {
                    folderPath = fbd.SelectedPath;
                    SearchFolderContent();//开始扫描内容

                    //TranslateTransform tt = new TranslateTransform();//创建平移对象
                    //Window1.RenderTransform = tt;
                    //DoubleAnimation by = new DoubleAnimation(bord1.Width, this.Width - bord1.Width, new Duration(TimeSpan.FromMinutes(0.5)));//创建动画处理对象
                    //by.AutoReverse = true;//反向运动
                    //by.RepeatBehavior = RepeatBehavior.Forever;//无限循环
                    //tt.BeginAnimation(TranslateTransform.XProperty, by);
                }

                else
                    MessageBox.Show("选择的文件夹不存在");
            }
        }

        /// <summary>
        /// 搜索文件夹路径内的文件跟文件夹
        /// </summary>
        /// <param name="o"></param>
        private void SearchFolderContent()
        {
            try
            {
                //这个方法将是异步的，所以取消任何当前活动的搜索
                //虽然根据视图逻辑，这应该是不可能的
                //执行多个搜索，因为搜索按钮被禁用。

                //先停止之前的搜索
                CancelSearch();

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

                            SetSearchingStatus(false);

                        });


                        SQLiteHelper.Instance.InsertData(DateTime.Now.ToString("yyyy-MMM-dd-HH-mm-ss"), folderPath, folderPath, "2");

                        RefreshDbData();
                    }
                }
            }
            catch (Exception e) { MessageBox.Show($"{e.Message} -- Cancelling Search"); CancelSearch(); }
        }

        public void StartSearchNonRecursively()
        {
            foreach (string folder in Directory.GetDirectories(folderPath))
            {
                if (!CanSearch) return;
                SearchFolderName(folder);
            }

            foreach (string file in Directory.GetFiles(folderPath))
            {
                if (!CanSearch) return;
                bool hasFoundFile = SearchFileName(file);

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
                    FilesSearched++;
            }
            catch (Exception e) { MessageBox.Show($"{e.Message} -- Cancelling Search"); CancelSearch(); }
        }

        public bool SearchFileName(string name)
        {
            CurrentlySearching = name;
            string fPath = CaseSensitive ? name : name.ToLower();
            ResultFound(fPath);
            FilesSearched++;
            return true;
        }

        /// <summary>
        /// 查询文件夹名称
        /// </summary>
        /// <param name="name"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public bool SearchFolderName(string name)
        {
            string dPath = CaseSensitive ? name : name.ToLower();

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

        public void AddResultAsync(ItemViewModel result)
        {
            Application.Current?.Dispatcher?.Invoke(() =>
            {
                Results.Add(result);
            });
        }

        public ItemViewModel CreateResultFromPath(string path)
        {
            if (IsFile(path))//文件
            {
                try
                {
                    FileInfo fInfo = new FileInfo(path);

                    bool isPE = JudgePE(path);

                    ItemViewModel result = new ItemViewModel()
                    {
                        Image = IconHelper.GetIconOfFile(path, false, false),
                        FileName = fInfo.Name,
                        FilePath = fInfo.FullName,
                        FileSizeBytes = fInfo.Length,
                        IsPE = isPE,
                        IsScanCompleteColor = isPE ? Brushes.Green : Brushes.Red,
                        Type = FileType.File
                    };

                    return result;
                }
                catch (Exception e) { MessageBox.Show($"{e.Message}"); return null; }
            }

            else if (IsDirectory(path))//文件夹
            {
                try
                {
                    DirectoryInfo dInfo = new DirectoryInfo(path);
                    ItemViewModel result = new ItemViewModel()
                    {
                        Image = IconHelper.GetIconOfFile(path, false, true),
                        FileName = dInfo.Name,
                        FilePath = dInfo.FullName,
                        IsPE = false,//文件夹默认非PE
                        IsScanCompleteColor = Brushes.Red,
                        FileSizeBytes = long.MaxValue,
                        Type = FileType.Folder
                    };

                    return result;
                }
                catch (Exception e) { MessageBox.Show($"{e.Message}"); return null; }
            }

            return null;
        }

        private bool JudgePE(string path)
        {
            try
            {
                System.IO.FileStream File = new FileStream(path, System.IO.FileMode.Open);

                byte[] PEFileByte = new byte[2];

                File.Read(PEFileByte, 0, 2);

                File.Close();

                if (PEFileByte[0] == 0x4D && PEFileByte[1] == 0x5A)//判断是否为PE文件->MZ
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                return false;
            }
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
