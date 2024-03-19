using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using WindowsVirusScanningSystem.Utilities;
using WindowsVirusScanningSystem.View;

namespace WindowsVirusScanningSystem.ViewModel
{
    public class NavigationVM : ViewModelBase
    {
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            ProductsCommand = new RelayCommand(Product);
            OrdersCommand = new RelayCommand(Order);
            WpfHexEditorCommand = new RelayCommand(WpfHexEditor);
            DocumentScanningFunctionCommand = new RelayCommand(DocumentScanningFunction);

            // Startup Page
            CurrentView = new HomeVM();
        }

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand WpfHexEditorCommand { get; set; }
        public ICommand DocumentScanningFunctionCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void DocumentScanningFunction(object obj) => CurrentView = new DocumentScanningFunction();
        private void Product(object obj) => CurrentView = new ProductVM();
        private void Order(object obj) => CurrentView = new OrderVM();
        private void WpfHexEditor(object obj) => CurrentView = new WpfHexEditorVM();
    }
}
