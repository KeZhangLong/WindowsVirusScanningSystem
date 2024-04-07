using System.Windows.Input;
using WindowsVirusScanningSystem.Utilities;

namespace WindowsVirusScanningSystem.ViewModel
{
    public class SearchRecordItem : ViewModelBase
    {
        public SearchRecordItem(string time, string fileName, string filePath, ICommand _recoverRecordItemCommand)
        {
            Time = time;
            FileName = fileName;
            FilePath = filePath;
            RecoverRecordItemCommand = _recoverRecordItemCommand;
        }

        public string Time { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        private ICommand recoverRecordItemCommand;
        public ICommand RecoverRecordItemCommand
        {
            get { return recoverRecordItemCommand; }
            set { recoverRecordItemCommand = value; OnPropertyChanged(); }
        }
    }
}
