using DomainLayer.Models.Attendance;
using DomainLayer.Models.ForgotPasswordRequest;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.Leave;
using DomainLayer.Models.NewUserRequest;
using DomainLayer.Models.Payroll;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
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
        IBaseServices<LeaveModel> LeaveRepository { get; }
        IBaseServices<NewUserRequestModel> NewUserRequestRepository { get; }
        IBaseServices<PayrollModel> PayrollRepository { get; }
        IBaseServices<RoleModel> RoleRepository { get; }
        IBaseServices<UserModel> UserRepository { get; }

        //-------------------------------------------- LOGIN TAsKS --------------------------------------------//

        //Approves a registration request *must only be accessible by admin
        Task ApproveNewUserRequest(string requestEmail, string roleName = null);

        //Sends password change request to Db
        Task ForgotPasswordRequest(string username, string email, string password, string confirmPassword);

        //Seeds initial Db info. Make sure to run on runtime
        Task InitialSeeding();

        //Authenticate's user login
        Task LoginUser(string username, string password);

        //Sends a registratopm request to Db
        Task NewUserRequest(string username, string password, string email);



        //Saves changes in context
        void Save();
    }
}