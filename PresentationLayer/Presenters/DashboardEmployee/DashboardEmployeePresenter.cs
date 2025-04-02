using DomainLayer.Models.User;
using DomainLayer.ViewModels.AttendanceLog;
using DomainLayer.ViewModels.DashboardDetails;
using DomainLayer.ViewModels.JobDeskDetails;
using PresentationLayer.Views;
using ServicesLayer;

namespace PresentationLayer.Presenters.DashboardEmployee
{
    public class DashboardEmployeePresenter : IDashboardEmployeePresenter
    {
        private readonly ILogin_Form _loginForm;
        private readonly IDashboard_Employee _dashboardForm;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardEmployeePresenter(IUnitOfWork unitOfWork, IDashboard_Employee dashboardForm, ILogin_Form loginForm)
        {
            _unitOfWork = unitOfWork;
            _dashboardForm = dashboardForm;
            _loginForm = loginForm;

            _dashboardForm.Exit += Exit;
        }
        public DashboardDetailsViewModel GetDashboardDetails(IUserModel user)
        {
            return _unitOfWork.GetDashboardDetails(user);
        }
        public IEnumerable<AttendanceLogViewModel> GetAttendanceLogs(IUserModel user)
        {
            return _unitOfWork.GetAttendanceLog(user);
        }
        public JobDeskDetailsViewModel GetJobDeskDetails(IUserModel user)
        {
            return _unitOfWork.GetJobDeskDetails(user);
        }

        private void Exit(object? Sender, EventArgs e)
        {
            _loginForm.UsernameField = "";
            _loginForm.PasswordField = "";
            _loginForm.Show();
        }
    }
}
