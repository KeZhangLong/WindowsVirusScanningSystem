using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsVirusScanningSystem.Model;

namespace WindowsVirusScanningSystem.ViewModel
{
    class ProductVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
      
        public ProductVM()
        {
            _pageModel = new PageModel();
        }
    }
}
