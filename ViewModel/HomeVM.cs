using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;
using WindowsVirusScanningSystem.Utilities;

namespace WindowsVirusScanningSystem.ViewModel
{
    public class HomeVM : Utilities.ViewModelBase
    {
        public ObservableCollection<SearchRecordItem> SearchRecordResults { get; set; }

        public HomeVM()
        {
            SearchRecordResults = new ObservableCollection<SearchRecordItem>();

            RefreshDbData();
        }

        //刷新扫描记录数据
        private void RefreshDbData()
        {
            DataTable dt = SQLiteHelper.Instance.GetScanVirusData();

            int RowCount = dt.Rows.Count;

            SearchRecordResults.Clear();

            for (int i = 0; i < RowCount; i++)
            {
                SearchRecordResults.Add(new SearchRecordItem(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString()));
            }
        }

        private ICommand recoverRecordItemCommand;
        public ICommand RecoverRecordItemCommand
        {
            get { return recoverRecordItemCommand; }
            set { recoverRecordItemCommand = value; OnPropertyChanged(); }
        }
    }
}
