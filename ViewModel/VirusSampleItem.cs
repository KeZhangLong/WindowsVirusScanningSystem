using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WindowsVirusScanningSystem.Utilities;

namespace WindowsVirusScanningSystem.ViewModel
{
    class VirusSampleItem
    {
        public VirusSampleItem(string sampleId, string sampleName, string sampleHash, string createdTime)
        {
            SampleId = sampleId;
            SampleName = sampleName;
            SampleHash = sampleHash;
            CreatedTime = createdTime;

            ItemDeleteCommand = new RelayCommand(ItemDelete);
        }

        public string SampleId { get; set; }
        public string SampleName { get; set; }
        public string SampleHash { get; set; }
        public string CreatedTime { get; set; }

        public ICommand ItemDeleteCommand { get; set; }

        private void ItemDelete(object e)
        {
            if (SQLiteHelper.Instance.DeleteVirusSample(SampleId))
            {
                MessageBox.Show("删除成功，请重新刷新!");
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }
    }
}
