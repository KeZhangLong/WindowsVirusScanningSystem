﻿using System;
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

namespace WindowsVirusScanningSystem.View
{
    /// <summary>
    /// DocumentScanningFunction.xaml 的交互逻辑
    /// </summary>
    public partial class DocumentScanningFunction : UserControl
    {
        public DocumentScanningFunction()
        {
            InitializeComponent();
        }
    }

    public class TodoItem
    {
        public string Title { get; set; }
        public int Completion { get; set; }
    }
}
