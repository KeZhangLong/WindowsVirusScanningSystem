using System;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsVirusScanningSystem.Utilities;

namespace WindowsVirusScanningSystem.ViewModel
{
    public class AddSampleDisplayVM : ViewModelBase
    {
        private string virusName = string.Empty;
        public string VirusName
        {
            get { return virusName; }
            set { virusName = value; OnPropertyChanged(); }
        }

        private string hashValue = string.Empty;
        public string HashValue
        {
            get { return hashValue; }
            set { hashValue = value; OnPropertyChanged(); }
        }
        public AddSampleDisplayVM()
        {
            AddSampleCommand = new RelayCommand(AddSample);
        }

        public ICommand AddSampleCommand { get; set; }

        private void AddSample(object e)
        {
            SQLiteHelper.Instance.InsertVirusSampleData(DateTime.Now.ToString("yyyyMMddHHmmssfffff"), VirusName, HashValue, DateTime.Now.ToString("s"));

            MessageBox.Show("导入成功");

            VirusName = string.Empty;

            HashValue = string.Empty;
        }
    }
}
