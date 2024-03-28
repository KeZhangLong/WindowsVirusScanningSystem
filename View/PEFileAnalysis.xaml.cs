using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsVirusScanningSystem.Utilities;
using WindowsVirusScanningSystem.ViewModel;

namespace WindowsVirusScanningSystem.View
{
    public class DosItem
    {
        public DosItem(string _head, string _content)
        {
            Head = _head;
            Content = _content;
        }
        public string Head { get; set; }

        public string Content { get; set; }

        //public string introduce { get; set; }
    }

    /// <summary>
    /// PEFileAnalysis.xaml 的交互逻辑
    /// </summary>
    public partial class PEFileAnalysis : UserControl
    {
        public PEFileAnalysis()
        {
            InitializeComponent();

            DosItems = new ObservableCollection<DosItem>();

            Dictionary<string, byte[]> DosStruct = new Dictionary<string, byte[]>();

        }

        Dictionary<string, byte[]> DosStruct = null;

        ObservableCollection<DosItem> DosItems = null;

        //动画..........................................

        //切换到某文件PE结构页面的按键
        private void ItemInfoPageMove_Click(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            ItemViewModel item = (ItemViewModel)listBox.SelectedItem;

            if (item.IsPE)//是PE文件
            {
                ItemPgaeShow();

                DosFileName.Text = item.FileName;

                PEHelper pEHelper = new PEHelper(item.FilePath);

                    DosItems.Add(new DosItem("e_magic", BitConverter.ToString(pEHelper._DosHeader.e_magic)));
                DosItems.Add(new DosItem("e_cblp", BitConverter.ToString(pEHelper._DosHeader.e_cblp)));
                DosItems.Add(new DosItem("e_cp", BitConverter.ToString(pEHelper._DosHeader.e_cp)));
                DosItems.Add(new DosItem("e_crlc", BitConverter.ToString(pEHelper._DosHeader.e_crlc)));
                DosItems.Add(new DosItem("e_cparhdr", BitConverter.ToString(pEHelper._DosHeader.e_cparhdr)));
                DosItems.Add(new DosItem("e_minalloc", BitConverter.ToString(pEHelper._DosHeader.e_minalloc)));
                DosItems.Add(new DosItem("e_maxalloc", BitConverter.ToString(pEHelper._DosHeader.e_maxalloc)));
                DosItems.Add(new DosItem("e_ss", BitConverter.ToString(pEHelper._DosHeader.e_ss)));
                DosItems.Add(new DosItem("e_sp", BitConverter.ToString(pEHelper._DosHeader.e_sp)));
                DosItems.Add(new DosItem("e_csum", BitConverter.ToString(pEHelper._DosHeader.e_csum)));
                DosItems.Add(new DosItem("e_ip", BitConverter.ToString(pEHelper._DosHeader.e_ip)));
                DosItems.Add(new DosItem("e_cs", BitConverter.ToString(pEHelper._DosHeader.e_cs)));
                DosItems.Add(new DosItem("e_rva", BitConverter.ToString(pEHelper._DosHeader.e_rva)));
                DosItems.Add(new DosItem("e_fg", BitConverter.ToString(pEHelper._DosHeader.e_fg)));
                DosItems.Add(new DosItem("e_bl1", BitConverter.ToString(pEHelper._DosHeader.e_bl1)));
                DosItems.Add(new DosItem("e_fg", BitConverter.ToString(pEHelper._DosHeader.e_fg)));
                DosItems.Add(new DosItem("e_oemid", BitConverter.ToString(pEHelper._DosHeader.e_oemid)));
                DosItems.Add(new DosItem("e_oeminfo", BitConverter.ToString(pEHelper._DosHeader.e_oeminfo)));
                DosItems.Add(new DosItem("e_bl2", BitConverter.ToString(pEHelper._DosHeader.e_bl2)));
                DosItems.Add(new DosItem("e_PESTAR", BitConverter.ToString(pEHelper._DosHeader.e_PESTAR)));



                DosFileConent.ItemsSource = DosItems;
            }
            else//不是PE文件
            {

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

    }
}
