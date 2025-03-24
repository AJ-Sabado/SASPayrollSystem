using DomainLayer.Models.Attendance;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.ForgotPasswordRequest;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Leave;
using DomainLayer.Models.NewUserRequest;
using DomainLayer.Models.Payroll;
using DomainLayer.Models.Role;
using DomainLayer.Models.Salary;
using DomainLayer.Models.User;
using DomainLayer.ViewModels.AttendanceLog;
using DomainLayer.ViewModels.DashboardDetails;
using DomainLayer.ViewModels.JobDeskDetails;
using ServicesLayer.Common;

namespace ServicesLayer
{
    public interface IUnitOfWork
    {
        IBaseServices<AttendanceModel> AttendanceRepository { get; }
        IUserModel? CurrentUser { get; }
        IBaseServices<DepartmentModel> DepartmentRepository { get; }
        IBaseServices<EmployeeModel> EmployeeRepository { get; }
        IBaseServices<ForgotPasswordRequestModel> ForgotPasswordRequestRepository { get; }
        IBaseServices<HolidayModel> HolidayRepository { get; }
        IBaseServices<LeaveModel> LeaveRepository { get; }
        IBaseServices<NewUserRequestModel> NewUserRequestRepository { get; }
        IBaseServices<PayrollModel> PayrollRepository { get; }
        IBaseServices<RoleModel> RoleRepository { get; }
        IBaseServices<SalaryModel> SalaryRepository { get; }
        IBaseServices<UserModel> UserRepository { get; }

        Task ApproveNewUserRequest(string requestEmail, string roleName = null);
        Task ForgotPasswordRequest(string username, string email, string password, string confirmPassword);
        Task InitialSeeding();
        Task LoginUser(string username, string password);
        Task NewUserRequest(string username, string password, string email);
        DashboardDetailsViewModel GetDashboardDetails(IUserModel user);
        IEnumerable<AttendanceLogViewModel> GetAttendanceLog(IUserModel user);
        JobDeskDetailsViewModel GetJobDeskDetails(IUserModel user);
        void Save();
    }
}