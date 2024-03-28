using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsVirusScanningSystem.Model
{
    public class PageModel
    {
        public string? FolderPath { get; set; }

        /// <summary>
        /// 搜索到的文件夹数目
        /// </summary>
        public int FoldersSearched { get; set; }

        /// <summary>
        /// 搜索到的文件数目
        /// </summary>
        public int FilesSearched { get; set; }

        /// <summary>
        /// 当前的搜索的文件
        /// </summary>
        public string? CurrentlySearching { get; set; }

        /// <summary>
        /// 当前的扫描的文件
        /// </summary>
        public string? CurrentlyScanning { get; set; }

        /// <summary>
        /// 是否正在搜索
        /// </summary>
        public bool IsSearching { get; set; }

        /// <summary>
        /// 能否进行搜索
        /// </summary>
        public bool CanSearch { get; set; }

        /// <summary>
        /// 是否正在扫描（病毒）
        /// </summary>
        public bool IsScanning { get; set; }

        /// <summary>
        /// 能否进行扫描（病毒）
        /// </summary>
        public bool CanScanning { get; set; }

        public bool CaseSensitive { get; set; }
        public bool SearchRecursive { get; set; }
    }
}
