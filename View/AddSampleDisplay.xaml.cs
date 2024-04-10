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
using System.Windows.Shapes;
using WindowsVirusScanningSystem.ViewModel;

namespace WindowsVirusScanningSystem.View
{
    /// <summary>
    /// AddSampleDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class AddSampleDisplay : Window
    {
        AddSampleDisplayVM addSampleDisplayVM = null;
        public AddSampleDisplay()
        {
            InitializeComponent();

            addSampleDisplayVM = new AddSampleDisplayVM();

            this.DataContext = addSampleDisplayVM;
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//关闭此窗体
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
    }
}
