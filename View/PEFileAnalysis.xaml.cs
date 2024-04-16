using Ookii.Dialogs.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsVirusScanningSystem.Model;
using WindowsVirusScanningSystem.Utilities;
using WindowsVirusScanningSystem.ViewModel;
using Section = WindowsVirusScanningSystem.Utilities.Section;

namespace WindowsVirusScanningSystem.View
{
    public class DosItem : ViewModelBase
    {
        public DosItem()
        {
        }

        private string _fileName = string.Empty;
        public string fileName
        {
            get { return _fileName; }
            set { _fileName = value;OnPropertyChanged(); }
        }

        private Header _header = null;

        public Header header
        {
            get { return _header; }
            set { _header = value; OnPropertyChanged(); }
        }

        //section全部内容
        private List<Section> _sections = null;
        public List<Section> sections
        {
            get { return _sections; }
            set { _sections = value; OnPropertyChanged(); }
        }

        //折叠未开放，显示的内容
        private Section[] fold_sections = new Section[4];
        public Section[] Fold_sections
        {
            get { return fold_sections; }
            set { fold_sections = value; OnPropertyChanged(); }
        }

        private List<Import> _imports = null;

        public List<Import> imports
        {
            get { return _imports; }
            set { _imports = value; OnPropertyChanged();}
        }

    }

    /// <summary>
    /// PEFileAnalysis.xaml 的交互逻辑
    /// </summary>
    public partial class PEFileAnalysis : System.Windows.Controls.UserControl
    {
        //查询文件夹的路径
        public string folderPath = string.Empty;

        //private bool isFold = true;

        DosItem dosItem = null;
        public PEFileAnalysis()
        {
            InitializeComponent();

            dosItem = new DosItem();

            this.DataContext = dosItem;
        }

        //动画..........................................

        //切换到某文件PE结构页面的按键
        private void ItemInfoPageMove_Click(object sender, RoutedEventArgs e)
        {
            //ListBox listBox = (ListBox)sender;

            //ItemViewModel item = (ItemViewModel)listBox.SelectedItem;

            //if (item == null)
            //{
            //    return;
            //}

            //if (item.IsPE)//是PE文件
            //{
            //    ItemPgaeShow();

            //    DosFileName.Text = item.FileName;

            //    PEHelper pEHelper = new PEHelper(item.FilePath);

            //    DosItems.Add(new DosItem("e_magic", BitConverter.ToString(pEHelper._DosHeader.e_magic), "Magic number"));
            //    DosItems.Add(new DosItem("e_cblp", BitConverter.ToString(pEHelper._DosHeader.e_cblp), "Bytes on last page of file"));
            //    DosItems.Add(new DosItem("e_cp", BitConverter.ToString(pEHelper._DosHeader.e_cp), "Pages in file"));
            //    DosItems.Add(new DosItem("e_crlc", BitConverter.ToString(pEHelper._DosHeader.e_crlc), "Relocations"));
            //    DosItems.Add(new DosItem("e_cparhdr", BitConverter.ToString(pEHelper._DosHeader.e_cparhdr), "Size of header in paragraphs"));
            //    DosItems.Add(new DosItem("e_minalloc", BitConverter.ToString(pEHelper._DosHeader.e_minalloc), "Minimum extra paragraphs needed"));
            //    DosItems.Add(new DosItem("e_maxalloc", BitConverter.ToString(pEHelper._DosHeader.e_maxalloc), "Maximum extra paragraphs needed"));
            //    DosItems.Add(new DosItem("e_ss", BitConverter.ToString(pEHelper._DosHeader.e_ss), "Initial (relative) SS value"));
            //    DosItems.Add(new DosItem("e_sp", BitConverter.ToString(pEHelper._DosHeader.e_sp), "Initial SP value"));
            //    DosItems.Add(new DosItem("e_csum", BitConverter.ToString(pEHelper._DosHeader.e_csum), "Checksum"));
            //    DosItems.Add(new DosItem("e_ip", BitConverter.ToString(pEHelper._DosHeader.e_ip), "Initial IP value"));
            //    DosItems.Add(new DosItem("e_cs", BitConverter.ToString(pEHelper._DosHeader.e_cs), "Initial (relative) CS value"));

            //    DosItems.Add(new DosItem("e_rva", BitConverter.ToString(pEHelper._DosHeader.e_rva), ""));
            //    DosItems.Add(new DosItem("e_fg", BitConverter.ToString(pEHelper._DosHeader.e_fg), ""));
            //    DosItems.Add(new DosItem("e_bl1", BitConverter.ToString(pEHelper._DosHeader.e_bl1), ""));
            //    DosItems.Add(new DosItem("e_fg", BitConverter.ToString(pEHelper._DosHeader.e_fg), ""));


            //    DosItems.Add(new DosItem("e_oemid", BitConverter.ToString(pEHelper._DosHeader.e_oemid), "OEM identifier (for e_oeminfo)"));
            //    DosItems.Add(new DosItem("e_oeminfo", BitConverter.ToString(pEHelper._DosHeader.e_oeminfo), "OEM information; e_oemid specific"));

            //    DosItems.Add(new DosItem("e_bl2", BitConverter.ToString(pEHelper._DosHeader.e_bl2), ""));
            //    DosItems.Add(new DosItem("e_PESTAR", BitConverter.ToString(pEHelper._DosHeader.e_PESTAR), ""));

            //    DosFileConent.ItemsSource = DosItems;
            //}
            //else//不是PE文件
            //{

            //}

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
            openFileDialog.CheckFileExists = true; //验证路径的有效性
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //扫描路径
               folderPath = openFileDialog.FileName;

                if (JudgePE(folderPath))
                {
                    ItemPgaeShow();

                    PEHelper pEHelper = new PEHelper(folderPath);

                    dosItem.fileName = folderPath.Split('\\').Last();

                    dosItem.header = pEHelper.GetHeader();

                    dosItem.sections = pEHelper.GetSections();

                    ////section数量大于4，只显示4行
                    //if (dosItem.sections.Count>4 )
                    //{
                    //    dosItem.sections.CopyTo(0, dosItem.Fold_sections, 0, 4);
                    //}
                    //else//section数量小于4，显示全部
                    //{
                    //    dosItem.sections.CopyTo(dosItem.Fold_sections);
                    //}

                    dosItem.imports = pEHelper.GetImports();

                    //DosFileConent.ItemsSource = dosItem.Fold_sections;
                }
                else
                {
                    System.Windows.MessageBox.Show("非PE文件，请重新选择！");
                }
            }
        }

        /// <summary>
        /// 判断是否是PE文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool JudgePE(string path)
        {
            try
            {
                System.IO.FileStream File = new FileStream(path, System.IO.FileMode.Open);

                byte[] PEFileByte = new byte[2];

                File.Read(PEFileByte, 0, 2);

                File.Close();

                if (PEFileByte[0] == 0x4D && PEFileByte[1] == 0x5A)//判断是否为PE文件->MZ
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }



        //某文件PE结构页面的返回
        private void ItemPageReturn_Click(object sender, RoutedEventArgs e)
        {
            ItemPageConceal();
        }

        //某文件PE结构页面的显示动画
        private void ItemPgaeShow()
        {
            //初始化System.Windows.Media.Animation.Storyboard类的新实例。Storyboard(脚本)
            Storyboard sb = new Storyboard();

            DoubleAnimation da1 = new DoubleAnimation(window.ActualWidth, new Duration(TimeSpan.FromSeconds(0.5)));//界面一X轴移动动画

            Window1.RenderTransform = new TranslateTransform();

            da1.AutoReverse = false;//不重复执行

            Storyboard.SetTarget(da1, Window1);//绑定动画为这个按钮执行的浮点动画

            Storyboard.SetTargetProperty(da1, new PropertyPath("(Border.RenderTransform).(TranslateTransform.X)"));//依赖的属性

            DoubleAnimation da1Opacity = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.5)));//不透明度设置为不可见

            Storyboard.SetTarget(da1Opacity, Window1);//绑定动画为这个按钮执行的浮点动画

            Storyboard.SetTargetProperty(da1Opacity, new PropertyPath(Border.OpacityProperty));//依赖的属性




            DoubleAnimation da2 = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.5)));//界面二X轴移动动画

            window2.RenderTransform = new TranslateTransform();

            da2.AutoReverse = false;//不重复执行

            Storyboard.SetTarget(da2, window2);//绑定动画为这个按钮执行的浮点动画

            Storyboard.SetTargetProperty(da2, new PropertyPath("(Border.RenderTransform).(TranslateTransform.X)"));//依赖的属性

            DoubleAnimation da2Opacity = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.5)));//不透明度设置为可见

            Storyboard.SetTarget(da2Opacity, window2);//绑定动画为这个按钮执行的浮点动画

            Storyboard.SetTargetProperty(da2Opacity, new PropertyPath(Border.OpacityProperty));//依赖的属性


            //向故事板中加入此浮点动画
            sb.Children.Add(da1);

            sb.Children.Add(da1Opacity);

            sb.Children.Add(da2);

            sb.Children.Add(da2Opacity);

            sb.Begin();//播放此动画
        }

        //某文件PE结构页面的隐藏动画
        private void ItemPageConceal()
        {
            Storyboard sb = new Storyboard();


            DoubleAnimation da1 = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.5)));//界面一X轴移动动画

            Window1.RenderTransform = new TranslateTransform();

            da1.AutoReverse = false;

            Storyboard.SetTarget(da1, Window1);//绑定动画为这个按钮执行的浮点动画

            Storyboard.SetTargetProperty(da1, new PropertyPath("(Border.RenderTransform).(TranslateTransform.X)"));//依赖的属性

            DoubleAnimation da1Opacity = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.5)));//不透明度设置为可见

            Storyboard.SetTarget(da1Opacity, Window1);//绑定动画为这个按钮执行的浮点动画

            Storyboard.SetTargetProperty(da1Opacity, new PropertyPath(Border.OpacityProperty));//依赖的属性




            DoubleAnimation da2 = new DoubleAnimation(window.ActualWidth, new Duration(TimeSpan.FromSeconds(0.5)));//界面二X轴移动动画

            window2.RenderTransform = new TranslateTransform();

            da2.AutoReverse = false;

            Storyboard.SetTarget(da2, window2);//绑定动画为这个按钮执行的浮点动画

            Storyboard.SetTargetProperty(da2, new PropertyPath("(Border.RenderTransform).(TranslateTransform.X)"));//依赖的属性

            DoubleAnimation da2Opacity = new DoubleAnimation(0, new Duration(TimeSpan.FromSeconds(0.5)));//不透明度设置为不可见

            Storyboard.SetTarget(da2Opacity, window2);//绑定动画为这个按钮执行的浮点动画

            Storyboard.SetTargetProperty(da2Opacity, new PropertyPath(Border.OpacityProperty));//依赖的属性


            sb.Children.Add(da1);//向故事板中加入此浮点动画

            sb.Children.Add(da1Opacity);

            sb.Children.Add(da2);//向故事板中加入此浮点动画

            sb.Children.Add(da2Opacity);

            sb.Begin();//播放此动画
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

        //private void ItemFoldButton_Clik(object sender, RoutedEventArgs e)
        //{
        //    //setions内容是否折叠起来
        //    if (isFold)//是
        //    {
                
        //    }
        //    else//否
        //    {

        //    }
        //}
    }
}
