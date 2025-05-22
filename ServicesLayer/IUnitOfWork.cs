using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
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
        IBaseServices<ContractorModel> ContractorRepository { get; }
        IBaseServices<DepartmentModel> DepartmentRepository { get; }
        IBaseServices<EmployeeModel> EmployeeRepository { get; }
        IBaseServices<HolidayModel> HolidayRepository { get; }
        IBaseServices<RoleModel> RoleRepository { get; }
        IBaseServices<UserModel> UserRepository { get; }

        Task InitialSeeding();
        Task<UserModel?> Login(string usernameOrEmail, string password);
        Task<RegisterUserResult> RegisterUser(string username, string email, string password, string confirmPassword, string roleName = null, string departmentName = null);
        Task EvaluateAllEmployeeAttendanceLog(DateOnly periodStart, DateOnly periodEnd);
        Task GenerateAllEmployeePayslips(DateOnly periodStart, DateOnly periodEnd, DateOnly payDate);
        Task Save();
    }
}