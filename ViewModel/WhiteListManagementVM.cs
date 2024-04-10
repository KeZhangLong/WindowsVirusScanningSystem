using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsVirusScanningSystem.Utilities;
using WindowsVirusScanningSystem.View;

namespace WindowsVirusScanningSystem.ViewModel
{
    class WhiteListManagementVM
    {
        public ObservableCollection<VirusSampleItem> WhiteList { get; set; }

        public WhiteListManagementVM()
        {
            AddFileIntoWhiteListCommand = new RelayCommand(AddFileIntoWhiteList);

            WhiteList = new ObservableCollection<VirusSampleItem>();

            RefreshDbData();
        }

        public ICommand AddFileIntoWhiteListCommand { get; set; }

        private void AddFileIntoWhiteList(object e)
        {
            AddFileIntoWhiteListDisplay addFileIntoWhiteListDisplay = new AddFileIntoWhiteListDisplay();

            addFileIntoWhiteListDisplay.ShowDialog();

            RefreshDbData();
        }

        //刷新扫描记录数据
        private void RefreshDbData()
        {
            DataTable dt = SQLiteHelper.Instance.GetFileWhiteListData();

            int RowCount = dt.Rows.Count;

            WhiteList.Clear();

            for (int i = 0; i < RowCount; i++)
            {
                WhiteList.Add(new VirusSampleItem(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString()));
            }
        }
    }
}
