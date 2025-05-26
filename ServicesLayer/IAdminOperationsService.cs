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

        int EmployeeCount { get; }
        int ContractorCount { get; }

        //Tables
        IList<ContractorModel> Contractors { get; }
        IList<DepartmentModel> Departments { get; }
        IList<EmployeeEvaluatedAttendanceModel> EvaluatedAttendances { get; }
        IList<EmployeeAttendanceLogModel> EmployeeAttendanceLogs { get; }
        IList<EmployeeAttendanceRequestModel> EmployeeAttendanceRequests { get; }
        IList<EmployeeLeaveModel> EmployeesOnLeave { get; }
        IList<EmployeeLeaveModel> EmployeeLeaveRequests { get; }
        IList<EmployeeModel> Employees { get; }
        IList<HolidayModel> Holidays { get; }
        IList<RoleModel> Roles { get; }
        IList<ContractorAttendanceLogModel> ContractorAttendanceLogs { get; }
        IList<UserModel> CurrentEmployees { get; }
        IList<UserModel> EmployeeRequests { get; }
        IList<EmployeePayslipModel> EmployeePayslips { get; }
        IList<ContractorPayslipModel> ContractorPayslips { get; }
        IDictionary<string, PayDateTotalPair> SummarizedPayrolls { get; }

        Task RefreshHolidaysTable();
        Task RefreshDepartmentsTable();
        Task RefreshRolesTable();
        Task RefreshContractorsTable();
        Task RefreshEmployeesTable();
        Task RefreshEvaluatedAttendances(DepartmentModel? department = null, DateTime? date = null, string employeeName = null);
        Task RefreshEmployeeAttendanceLogs(DateTime? date = null);
        Task RefreshEmployeeAttendanceRequests();
        Task RefreshEmployeeLeaves(string employeeName = null);
        Task RefreshContractorAttendanceLogs(DateTime? date = null);
        Task RefreshEmployees(string name = null, RoleModel? role = null, DepartmentModel? department = null);
        Task RefreshEmployeePayslips();
        Task RefreshContractorPayslips();
        Task RecountPopulation();
        Task SummarizePayrolls();

        Task AddDepartment(DepartmentModel department);
        Task DeleteDepartment(DepartmentModel department);

        Task AddHoliday(HolidayModel holiday);
        Task DeleteHoliday(HolidayModel holiday);

        Task<UserModel?> InitializeService(Guid adminUserGuid);
        Task RevertInitialState();
    }
}