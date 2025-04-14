using DomainLayer.Models.Attendance;
using DomainLayer.Models.Contribution;
using DomainLayer.Models.Department;
using DomainLayer.Models.EmployeeContactInfo;
using DomainLayer.Models.EmployeeEmploymentInfo;
using DomainLayer.Models.EmployeeFinancialInfo;
using DomainLayer.Models.EmployeePersonalInfo;
using DomainLayer.Models.Leave;
using DomainLayer.Models.EmployeeSalary;
using DomainLayer.Models.User;

namespace DomainLayer.Models.Employee
{
    public interface IEmployeeModel
    {
        ICollection<AttendanceModel> Attendances { get; }
        decimal BasicSemiMonthlyRate { get; set; }
        ContributionModel Contribution { get; set; }
        DepartmentModel Department { get; set; }
        Guid DepartmentId { get; set; }
        EmployeeContactInfoModel? EmployeeContactInfo { get; set; }
        EmployeeEmploymentInfoModel? EmployeeEmploymentInfo { get; set; }
        EmployeeFinancialInfoModel? EmployeeFinancialInfo { get; set; }
        EmployeePersonalInfoModel? EmployeePersonalInfo { get; set; }
        Guid Id { get; set; }
        uint LeaveCredits { get; set; }
        ICollection<LeaveModel> Leaves { get; }
        ICollection<EmployeeSalaryModel> Salaries { get; }
        UserModel User { get; set; }
        Guid UserId { get; set; }
        TimeOnly WorkShiftEnd { get; set; }
        TimeOnly WorkShiftStart { get; set; }
    }
}