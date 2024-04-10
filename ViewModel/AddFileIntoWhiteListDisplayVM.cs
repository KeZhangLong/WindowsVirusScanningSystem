using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsVirusScanningSystem.Utilities;

namespace WindowsVirusScanningSystem.ViewModel
{
    public class AddFileIntoWhiteListDisplayVM : ViewModelBase
    {
        private string fileName = string.Empty;
        public string _FileName
        {
            get { return fileName; }
            set { fileName = value; OnPropertyChanged(); }
        }

        private string hashValue = string.Empty;
        public string HashValue
        {
            get { return hashValue; }
            set { hashValue = value; OnPropertyChanged(); }
        }
        public AddFileIntoWhiteListDisplayVM()
        {
            AddSampleCommand = new RelayCommand(AddSample);

            SelectFilePathCommand = new RelayCommand(SelectFilePath);
        }
        public ICommand SelectFilePathCommand { get; set; }
        public ICommand AddSampleCommand { get; set; }

        private void SelectFilePath(object e)
        {
            OpenFileDialog openFileDialog  = new OpenFileDialog();
            openFileDialog.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
            openFileDialog.CheckFileExists = true; //验证路径的有效性
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //扫描路径
                _FileName = openFileDialog.FileName;

                DataResult<string> r = HashHelper.ComputeMD5(_FileName);

                //计算成功
                if (r.IsSuccess)
                {
                    HashValue = r.Value;
                }
                else//大概率文件被占用，无法计算
                {
                    System.Windows.MessageBox.Show(r.Message);

                    _FileName = string.Empty;

                    HashValue = string.Empty;
                }
            }
        }

        private void AddSample(object e)
        {
            SQLiteHelper.Instance.InsertFileWhiteListData(DateTime.Now.ToString("yyyyMMddHHmmssfffff"), _FileName, HashValue, DateTime.Now.ToString("s"));

            System.Windows.MessageBox.Show("导入成功");

            _FileName = string.Empty;

            HashValue = string.Empty;
        }

        /// <summary>
        /// 判断路径是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsDirectory(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path);
        }
    }
}
