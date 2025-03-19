using DomainLayer.Models.Attendance;
using DomainLayer.Models.Contribution;
using DomainLayer.Models.Department;
using DomainLayer.Models.Leave;
using DomainLayer.Models.Salary;

namespace DomainLayer.Models.Employee
{
    public interface IEmployeeModel
    {
        ICollection<AttendanceModel> Attendances { get; }
        decimal BasicSemiMonthlyRate { get; set; }
        DateOnly BirthDay { get; set; }
        ContributionModel Contribution { get; set; }
        DepartmentModel Department { get; set; }
        Guid DepartmentId { get; set; }
        DateOnly EmploymentDate { get; set; }
        string FullName { get; set; }
        Guid Id { get; set; }
        string JobTitle { get; set; }
        uint LeaveCredits { get; set; }
        ICollection<LeaveModel> Leaves { get; }
        ICollection<SalaryModel> Salaries { get; }
        TimeOnly WorkShiftEnd { get; set; }
        TimeOnly WorkShiftStart { get; set; }
    }
}