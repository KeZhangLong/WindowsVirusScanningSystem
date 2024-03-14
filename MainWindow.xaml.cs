using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsVirusScanningSystem
{
    public partial class MainWindow : Window
    {
        bool IsMaximized = false;

        double windowHeight;

        double windowWidth;

        public MainWindow()
        {
            InitializeComponent();

            windowHeight = this.Height;

            windowWidth = this.Width;
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();//应用关闭
        }

        /// <summary>
        /// 窗体移动代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// 窗体缩放代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseLeftButtonDownEvent(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //是否最小化判断
                if (IsMaximized)//是
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = windowWidth;
                    this.Height = windowHeight;
                    IsMaximized = false;
                }
                else//否
                {
                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }
        }
    }
}
