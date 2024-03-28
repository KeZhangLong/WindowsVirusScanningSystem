using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsVirusScanningSystem.Model;

namespace WindowsVirusScanningSystem.ViewModel
{
    public enum FolderOrFileSate
    {
        未扫描,
        已扫描,
        有病毒
    }
    public enum FileType
    {
        Folder, File
    }
    public class ItemViewModel : Utilities.ViewModelBase
    {
        public ItemViewModel()
        {
            MD5Strs = new List<string>();
        }

        // Using an icon because it's really simple
        // Can use a converter to convert icons to
        // BitmapImage/ImageSource which the Image
        // Control uses.
        // ICON（图标）转换成照片
        private Icon _image;
        public Icon Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(); }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged(); }
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; OnPropertyChanged(); }
        }

        private long _fileSizeBytes;
        public long FileSizeBytes
        {
            get { return _fileSizeBytes; }
            set { _fileSizeBytes = value; OnPropertyChanged(); }
        }

        private string _selection;
        public string Selection
        {
            get { return _selection; }
            set { _selection = value; OnPropertyChanged(); }
        }

        //检测结果（未扫描、已扫描、有病毒）
        private string _isScanComplete;
        public string IsScanComplete
        {
            get { return _isScanComplete; }
            set { _isScanComplete = value; OnPropertyChanged(); }
        }

        //检测结果字体颜色
        private System.Windows.Media.Brush _isScanCompleteColor;
        public System.Windows.Media.Brush IsScanCompleteColor
        {
            get { return _isScanCompleteColor; }
            set { _isScanCompleteColor = value; OnPropertyChanged(); }
        }

        //是否跳过扫描
        private bool _isSkipScan;
        public bool IsSkipScan
        {
            get { return _isSkipScan; }
            set { _isSkipScan = value; OnPropertyChanged(); }
        }

        //是否是PE文件
        private bool _isPE;
        public bool IsPE
        {
            get { return _isPE; }
            set { _isPE = value; OnPropertyChanged(); }
        }

        private string _md5Str;

        public string MD5Str
        {
            get { return _md5Str; }
            set { _md5Str = value;OnPropertyChanged(); }
        }

        private List<string> _md5Strs;

        public List<string> MD5Strs
        {
            get { return _md5Strs; }
            set { _md5Strs = value; OnPropertyChanged(); }
        }

        // Doesn't need to be binded
        public FileType Type { get; set; }
    }
}
