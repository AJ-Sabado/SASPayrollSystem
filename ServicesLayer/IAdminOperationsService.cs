using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;

namespace ServicesLayer
{
    public interface IAdminOperationsService
    {
        UserModel? AdminUser { get; }

        bool ConfirmAction(string password);

        //Tables
        IList<ContractorModel> Contractors { get; }
        IList<DepartmentModel> Departments { get; }
        IList<EmployeeModel> Employees { get; }
        IList<HolidayModel> Holidays { get; }
        IList<RoleModel> Roles { get; }

        Task RefreshHolidaysTable();
        Task RefreshDepartmentsTable();
        Task RefreshRolesTable();
        Task RefreshContractorsTable();
        Task RefreshEmployeesTable();

        Task<UserModel?> InitializeService(Guid adminUserGuid);
    }
}