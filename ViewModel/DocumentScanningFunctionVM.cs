﻿using System.Windows;
using Ookii.Dialogs.Wpf;
using System.Windows.Input;
using WindowsVirusScanningSystem.Model;
using WindowsVirusScanningSystem.Utilities;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace WindowsVirusScanningSystem.ViewModel
{
    class DocumentScanningFunctionVM : Utilities.ViewModelBase
    {
        //公用模型
        private readonly PageModel _pageModel;
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

        public DocumentScanningFunctionVM()
        {
            Results = new ObservableCollection<ItemViewModel>();

            _pageModel = new PageModel();
            SearchFolderPathCommand = new RelayCommand(SearchFolderPath);
            SearchFolderContentCommand = new RelayCommand(SearchFolderContent);
        }

        public ICommand SearchFolderPathCommand { get; set; }
        public ICommand SearchFolderContentCommand { get; set; }

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
                    folderPath = fbd.SelectedPath;
                else
                    MessageBox.Show("选择的文件夹不存在");
            }
        }

        /// <summary>
        /// 搜索文件夹路径内的文件跟文件夹
        /// </summary>
        /// <param name="o"></param>
        private void SearchFolderContent(object o)
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
                            if (SearchRecursive)
                            {
                                StartSearchRecursively();
                            }
                            else
                            {
                                StartSearchNonRecursively();
                            }

                            SetSearchingStatus(false);
                        });
                    }
                }
            }
            catch (Exception e) { MessageBox.Show($"{e.Message} -- Cancelling Search"); CancelSearch(); }
        }

        public void StartSearchRecursively()
        {
            if (string.IsNullOrEmpty(folderPath))
            {
                MessageBox.Show("搜索路径为空");
                return;
            }

            void DirectorySearch(string toSearchDir)
            {
                // This will loop through every folder and then do the same thing
                // For subfolders and so on indefinitely until there's no
                // more sub folders
                //这将循环遍历每个文件夹，然后做同样的事情
                //对于子文件夹等等，直到没有
                //更多子文件夹
                foreach (string folder in Directory.GetDirectories(toSearchDir))
                {
                    // Cancel search if needed
                    if (!CanSearch) return;

                    foreach (string file in Directory.GetFiles(folder))
                    {
                        if (!CanSearch) return;

                        SearchFileName(file);
                    }

                    FoldersSearched++;

                    // This is what makes this run recursively, the fact you
                    // can the same function in the same function... sort of
                    //这就是它递归运行的原因
                    //可以在同一个函数中使用同一个函数…的
                    DirectorySearch(folder);
                }
            }

            DirectorySearch(folderPath);

            // Also need to search through every file in the start folders too...
            //还需要搜索开始文件夹中的每个文件…

            foreach (string file in Directory.GetFiles(folderPath))
            {
                if (!CanSearch) return;

                bool hasFoundFile = SearchFileName(file);

                // this stops there from being duplicated items.
                // if it's already found the item above, dont search
                // the contents because that's just pointless.
                //这会阻止重复的项。
                //如果它已经找到了上面的项目，不搜索
                //内容，因为那是没有意义的。
                if (!hasFoundFile)
                {
                    ReadAndSearchFile(file, false);
                }
            }
        }

        public void StartSearchNonRecursively()
        {
            // Non recursive is a big less messy
            // Because this is async, you could end up changing StartFolder and it would
            // Mess up the search a bit.
            //非递归是一个大的不那么混乱
            //因为这是异步的，你可能会改变StartFolder
            //把搜索弄乱一点。

            foreach (string file in Directory.GetDirectories(folderPath))
            {
                if (!CanSearch) return;
                SearchFolderName(file);
            }

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

                        // Inline convert the text to lower if CaseSensitive is false
                        //if ((CaseSensitive ? txt : txt.ToLower()).Contains(searchText))
                        //{
                            ResultFound(file);
                            break;
                        //}
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
                        //Selection = selectionText,
                        FileSizeBytes = fInfo.Length,
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
                        //Selection = selectionText,
                        // This is the flag used before
                        // In the FileSizeFormatterConverter
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
