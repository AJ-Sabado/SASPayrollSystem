using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.ContractorPayslip;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
using DomainLayer.Models.EmployeeLeave;
using DomainLayer.Models.EmployeePayslip;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using ServicesLayer.Common;
using ServicesLayer.Enums;

namespace ServicesLayer
{
    public interface IUnitOfWork
    {
        IBaseServices<AdminModel> AdminRepository { get; }
        IBaseServices<ContractorAttendanceLogModel> ContractorAttendanceLogRepository { get; }
        IBaseServices<ContractorPayslipModel> ContractorPayslipRepository { get; }
        IBaseServices<ContractorModel> ContractorRepository { get; }
        IBaseServices<DepartmentModel> DepartmentRepository { get; }
        IBaseServices<EmployeeAttendanceLogModel> EmployeeAttendanceLogRepository { get; }
        IBaseServices<EmployeeAttendanceRequestModel> EmployeeAttendanceRequestRepository { get; }
        IBaseServices<EmployeeEvaluatedAttendanceModel> EmployeeEvaluatedAttendanceRepository { get; }
        IBaseServices<EmployeeLeaveModel> EmployeeLeaveRepository { get; }
        IBaseServices<EmployeePayslipModel> EmployeePayslipRepository { get; }
        IBaseServices<EmployeeModel> EmployeeRepository { get; }
        IBaseServices<HolidayModel> HolidayRepository { get; }
        IBaseServices<RoleModel> RoleRepository { get; }
        IBaseServices<UserModel> UserRepository { get; }

        Task EvaluateAllEmployeeAttendanceLog(DateOnly periodStart, DateOnly periodEnd);
        Task GenerateAllContractorPayslips(DateOnly periodStart, DateOnly periodEnd, DateOnly payDate);
        Task GenerateAllEmployeePayslips(DateOnly periodStart, DateOnly periodEnd, DateOnly payDate);
        Task InitialSeeding();
        Task<UserModel?> Login(string usernameOrEmail, string password);
        Task<RegisterUserResult> RegisterUser(string username, string email, string password, string confirmPassword, string roleName = null, string departmentName = null);
        Task Save();
    }
}