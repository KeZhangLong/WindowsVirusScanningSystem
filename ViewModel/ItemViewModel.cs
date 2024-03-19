using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsVirusScanningSystem.Model;

namespace WindowsVirusScanningSystem.ViewModel
{
    public enum FileType
    {
        Folder, File
    }
    public class ItemViewModel : Utilities.ViewModelBase
    {
        // Using an icon because it's really simple
        // Can use a converter to convert icons to
        // BitmapImage/ImageSource which the Image
        // Control uses.

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

        // Doesn't need to be binded
        public FileType Type { get; set; }
    }
}
