using PresentationLayer.Views;

namespace PresentationLayer.Presenters.Base
{
    public interface IBasePresenter
    {
        IDashboard_Employee DashboardEmployeeView { get; }
        ILogin_Form LoginView { get; }
    }
}