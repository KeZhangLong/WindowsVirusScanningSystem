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
            PEFileAnalysisCommand = new RelayCommand(PEFileAnalysis);
            WpfHexEditorCommand = new RelayCommand(WpfHexEditor);
            DocumentScanningFunctionCommand = new RelayCommand(DocumentScanningFunction);
            SampleImportCommand= new RelayCommand(SampleImport);
            WhiteListManagementCommand = new RelayCommand(WhiteListManagement);

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
        public ICommand PEFileAnalysisCommand { get; set; }
        public ICommand WpfHexEditorCommand { get; set; }
        public ICommand DocumentScanningFunctionCommand { get; set; }
        public ICommand SampleImportCommand { get; set; }

        public ICommand WhiteListManagementCommand{ get; set; }

    private void Home(object obj) => CurrentView = new HomeVM();
        private void DocumentScanningFunction(object obj) => CurrentView = new DocumentScanningFunction();
        private void PEFileAnalysis(object obj) => CurrentView = new PEFileAnalysisVM();
        private void WpfHexEditor(object obj) => CurrentView = new WpfHexEditorVM();
        private void SampleImport(object obj) => CurrentView = new SampleImportVM();
        private void WhiteListManagement(object obj) => CurrentView = new WhiteListManagementVM();
    }
}
