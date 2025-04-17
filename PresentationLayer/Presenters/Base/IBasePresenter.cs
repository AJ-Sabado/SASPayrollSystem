using PresentationLayer.Views;
using PresentationLayer.Views.Forgot_Password_Forms;

namespace PresentationLayer.Presenters.Base
{
    public interface IBasePresenter
    {
        IDashboard_Employee DashboardEmployeeView { get; }
        ILogin_Form LoginView { get; }

    }
}