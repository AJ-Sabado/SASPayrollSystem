    using ServicesLayer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using PresentationLayer.Views;

namespace PresentationLayer.Presenters
{
    internal class Dashboard_EmployeePresenter
    {
        private IDashboard_Employee _view;
        private IUnitOfWork _unitOfWork;

        public Dashboard_EmployeePresenter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void LoadDashboard()
        {
            // Business logic here, call _view.UpdateUI() instead of direct UI calls
            _view.ShowDashboard();
        }

        public void SetView(IDashboard_Employee view)
        {
            _view = view;
        }

        public void ShowDashboard()
        {
            _view.ShowDashboard();
        }

        public void ShowJobDesk()
        {
            _view.ShowJobDesk();
        }

        public void ShowAccounts()
        {
            _view.ShowAccounts();
        }
 
    }
}
