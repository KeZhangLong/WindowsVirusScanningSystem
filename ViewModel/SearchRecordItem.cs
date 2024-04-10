using System.Windows.Input;
using WindowsVirusScanningSystem.Utilities;
using WindowsVirusScanningSystem.View;

namespace WindowsVirusScanningSystem.ViewModel
{
    public class SearchRecordItem : ViewModelBase
    {
        public SearchRecordItem Instance = null;
        public SearchRecordItem(string recordId, string scanPath, string fileCount, string folderCount,string virusCount, string scanTime)
        {
            RecordId = recordId;
            ScanPath = scanPath;
            FileCount = fileCount;
            FolderCount = folderCount;
            VirusCount = virusCount;
            ScanTime = scanTime;
            VirusCount = virusCount;

            Instance = this;

            RecoverRecordItemCommand = new RelayCommand(ShowDataView);
        }

        public string RecordId { get; set; }
        public string ScanPath { get; set; }
        public string FileCount { get; set; }
        public string FolderCount { get; set; }
        public string VirusCount { get; set; }
        public string ScanTime { get; set; }

        //点击查看扫描记录详细数据
        public ICommand RecoverRecordItemCommand { get; set; }

        private void ShowDataView(object e)
        {
            ScanDataDisplay scanDataDisplay = new ScanDataDisplay(Instance);

            scanDataDisplay.ShowDialog();
        }
    }
}
