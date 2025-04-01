using DomainLayer.Models.User;
using DomainLayer.ViewModels.AttendanceLog;
using DomainLayer.ViewModels.DashboardDetails;
using DomainLayer.ViewModels.JobDeskDetails;

namespace PresentationLayer.Presenters.DashboardEmployee
{
    public interface IDashboardEmployeePresenter
    {
        IEnumerable<AttendanceLogViewModel> GetAttendanceLogs(IUserModel user);
        DashboardDetailsViewModel GetDashboardDetails(IUserModel user);
        JobDeskDetailsViewModel GetJobDeskDetails(IUserModel user);
    }
}