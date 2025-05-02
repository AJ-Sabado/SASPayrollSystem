using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using ServicesLayer.Common;

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

        Task ApproveNewUserRequest(string requestEmail, string roleName = null);
        Task ForgotPasswordRequest(string username, string email, string password, string confirmPassword);
        Task InitialSeeding();
        Task<UserModel?> Login(string usernameOrEmail, string password);
        Task NewUserRequest(string username, string password, string email);
        void Save();
    }
}