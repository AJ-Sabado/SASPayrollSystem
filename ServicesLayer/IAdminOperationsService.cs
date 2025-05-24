using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
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
        IList<EmployeeEvaluatedAttendanceModel> EvaluatedAttendances { get; }
        IList<EmployeeAttendanceLogModel> EmployeeAttendanceLogs { get; }
        IList<EmployeeAttendanceRequestModel> EmployeeAttendanceRequests { get; }
        IList<EmployeeModel> Employees { get; }
        IList<HolidayModel> Holidays { get; }
        IList<RoleModel> Roles { get; }

        Task RefreshHolidaysTable();
        Task RefreshDepartmentsTable();
        Task RefreshRolesTable();
        Task RefreshContractorsTable();
        Task RefreshEmployeesTable();
        Task RefreshEvaluatedAttendances(DepartmentModel? department = null, DateTime? date = null);
        Task RefreshEmployeeAttendanceLogs(DateTime? date = null);
        Task RefreshEmployeeAttendanceRequests();

        Task<UserModel?> InitializeService(Guid adminUserGuid);
    }
}