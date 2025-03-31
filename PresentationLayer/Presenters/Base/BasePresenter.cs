using PresentationLayer.Presenters.DashboardEmployee;
using PresentationLayer.Presenters.Login;
using PresentationLayer.Views;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters.Base
{
    public class BasePresenter : IBasePresenter
    {
        private IUnitOfWork _unitOfWork = new UnitOfWork();

        private LoginPresenter LoginPresenter;
        public ILogin_Form LoginView { get; private set; }

        private IDashboardEmployeePresenter DashboardEmployeePresenter;
        public IDashboard_Employee DashboardEmployeeView { get; private set; }

        public BasePresenter()
        {
            _unitOfWork.InitialSeeding();

            DashboardEmployeeView = new Dashboard_Employee();
            LoginView = new Login_Form();

            DashboardEmployeePresenter = new DashboardEmployeePresenter(_unitOfWork, DashboardEmployeeView, LoginView);
            LoginPresenter = new LoginPresenter(_unitOfWork, LoginView, DashboardEmployeeView);
        }


    }
}
