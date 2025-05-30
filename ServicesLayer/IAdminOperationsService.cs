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

namespace ServicesLayer
{
    public interface IAdminOperationsService
    {
        UserModel? AdminUser { get; }

        bool ConfirmAction(string password);

        //Population Information
        int UserTotalCount { get; }
        int EmployeeCount { get; }
        int ContractorCount { get; }

        //Cached Tables
        IList<ContractorAttendanceLogModel> ContractorAttendanceLogs { get; }
        IList<ContractorPayslipModel> ContractorPayslips { get; }
        IList<ContractorModel> Contractors { get; }
        IList<DepartmentModel> Departments { get; }
        IList<EmployeeAttendanceLogModel> EmployeeAttendanceLogs { get; }
        IList<EmployeeAttendanceRequestModel> EmployeeAttendanceRequests { get; }
        IList<EmployeeEvaluatedAttendanceModel> EmployeeEvaluatedAttendances { get; }
        IList<EmployeeLeaveModel> EmployeeLeaves { get; }
        IList<EmployeePayslipModel> EmployeePayslips { get; }
        IList<EmployeeModel> Employees { get; }
        IList<HolidayModel> Holidays { get; }
        IList<RoleModel> Roles { get; }
        IList<UserModel> Users { get; }
        IDictionary<string, PayDateTotalPair> SummarizedPayrolls { get; }

        //Refresh Methods
        Task RefreshContractorAttendanceLogs();
        Task RefreshContractorPayslips();
        Task RefreshContractors();
        Task RefreshDepartments();
        Task RefreshHolidays();
        Task RefreshRoles();
        Task RefreshEmployeeAttendanceLogs();
        Task RefreshEmployeeAttendanceRequests();
        Task RefreshEmployeeEvaluatedAttendances();
        Task RefreshEmployees();
        Task RefreshEmployeeLeaves();
        Task RefreshEmployeePayslips();
        Task RefreshUsers();
        Task SummarizePayrolls();
        Task RecountPopulation();

        //Operations
        Task UpdateUser(UserModel user);
        Task DeleteUser(UserModel user);

        Task AddDepartment(DepartmentModel department);
        Task DeleteDepartment(DepartmentModel department);

        Task AddEmployee(EmployeeModel employee);
        Task AddContractor(ContractorModel contractor);

        Task AddHoliday(HolidayModel holiday);
        Task DeleteHoliday(HolidayModel holiday);

        Task<UserModel?> InitializeService(Guid adminUserGuid);
        Task RevertInitialState();
    }
}