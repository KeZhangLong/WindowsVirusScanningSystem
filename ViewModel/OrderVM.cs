using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsVirusScanningSystem.Model;

namespace WindowsVirusScanningSystem.ViewModel
{
    class OrderVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public OrderVM()
        {
            _pageModel = new PageModel();
        }
    }
}
