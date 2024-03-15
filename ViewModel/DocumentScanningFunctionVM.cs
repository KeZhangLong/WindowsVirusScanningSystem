using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsVirusScanningSystem.Model;

namespace WindowsVirusScanningSystem.ViewModel
{
    class DocumentScanningFunctionVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public DocumentScanningFunctionVM()
        {
            _pageModel = new PageModel();
        }
    }
}
