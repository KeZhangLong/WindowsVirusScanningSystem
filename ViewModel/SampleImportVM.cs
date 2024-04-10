using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.TextFormatting;
using WindowsVirusScanningSystem.Model;
using WindowsVirusScanningSystem.Utilities;

namespace WindowsVirusScanningSystem.ViewModel
{
    class SampleImportVM : Utilities.ViewModelBase
    {
        bool isCancel = false;

        int rowCount = int.MaxValue;

        int progress = 0;

        string btnState = "导入病毒样本";

        public string BtnState
        {
            get { return btnState; }
            set { btnState = value; OnPropertyChanged(); }
        }

        public int RowCount
        {
            get { return rowCount; }
            set { rowCount = value; OnPropertyChanged(); }
        }

        public int Progress
        {
            get { return progress; }
            set { progress = value; OnPropertyChanged(); }
        }

        public bool IsCancel
        {
            get { return isCancel; }
            set { isCancel = value; OnPropertyChanged(); }
        }
        public SampleImportVM()
        {
            ImportedVirusSampleCommand = new RelayCommand(ImportedVirusSample);
        }

        public RelayCommand ImportedVirusSampleCommand { get; set; }

        private void ImportedVirusSample(object e)
        {
            if (BtnState == "导入病毒样本")
            {
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();

                openFileDialog.Filter = "文本文件(*.txt)|*.txt";
                openFileDialog.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
                openFileDialog.CheckFileExists = true; //验证路径的有效性

                if (openFileDialog.ShowDialog() == DialogResult.OK) //用户点击确认按钮，发送确认消息
                {
                    BtnState = "取消导入";

                    TaskState(false);

                    string filePath = openFileDialog.FileName;//获取在文件对话框中选定的路径或者字符串

                    ReadTxt(filePath);
                }
            }
            else
            {
                TaskState(true);
            }
        }

        private void TaskState(bool state)
        {
            isCancel = state;
        }

        private void ReadTxt(string filePath)
        {
            try
            {
                Task.Run(() =>
                {
                    StreamReader sR = File.OpenText(filePath);

                    // 全部读完 
                    string restOfStream = sR.ReadToEnd();

                    string[] data = restOfStream.Split("\r\n");

                    RowCount = data.Length;

                    for (int i = 0; i < RowCount; i++)
                    {
                        if (isCancel)//检测是否要停止导入
                        {
                            break;
                        }

                        if (data[i].Contains("#"))
                        {
                            Progress++;
                            continue;
                        }

                        SQLiteHelper.Instance.InsertVirusSampleData(data[i],"Unknown", data[i],DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff"));

                        Progress++;
                    }

                    if (RowCount == Progress)//导入完成
                    {
                        MessageBox.Show("导入成功！");
                    }
                    else//停止导入
                    {
                        //什么也不用干
                    }

                    BtnState = "导入病毒样本";
                    RowCount = int.MaxValue;
                    Progress = 0;

                    sR.Close();
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"病毒数据导入数据库出错，{ex.Message}");
            }
        }
    }
}
