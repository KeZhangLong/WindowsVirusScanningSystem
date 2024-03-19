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


        public string? CurrentlySearching { get; set; }
        
        /// <summary>
        /// 是否正在搜索
        /// </summary>
        public bool IsSearching { get; set; }

        public bool CanSearch { get; set; }

        public bool CaseSensitive { get; set; }
        public bool SearchRecursive { get; set; }
    }
}
