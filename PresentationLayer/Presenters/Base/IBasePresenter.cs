using PresentationLayer.Presenters.DashboardEmployee;

namespace PresentationLayer.Presenters.Base
{
    public interface IBasePresenter
    {
        IDashboardEmployeePresenter DashboardEmployeePresenter { get; }
        IDashboard_Employee DashboardEmployeeView { get; }
    }
}