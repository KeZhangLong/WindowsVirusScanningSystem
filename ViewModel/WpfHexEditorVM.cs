using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsVirusScanningSystem.Model;

namespace WindowsVirusScanningSystem.ViewModel
{
    public class WpfHexEditorVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public WpfHexEditorVM()
        {
            _pageModel = new PageModel();
        }
    }
}
