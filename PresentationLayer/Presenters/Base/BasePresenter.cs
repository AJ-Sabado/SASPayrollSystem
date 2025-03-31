using PresentationLayer.Presenters.DashboardEmployee;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters.Base
{
    public class BasePresenter
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork();

        public IDashboardEmployeePresenter DashboardEmployeePresenter { get; private set; }



        public BasePresenter()
        {
            DashboardEmployeePresenter = new DashboardEmployeePresenter(_unitOfWork);
        }
    }
}
