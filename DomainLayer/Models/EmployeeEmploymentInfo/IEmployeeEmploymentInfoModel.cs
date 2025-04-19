using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeeEmploymentInfo
{
    public interface IEmployeeEmploymentInfoModel
    {
        string CompanyId { get; set; }
        DateOnly DateHired { get; set; }
        string Department { get; }
        EmployeeModel Employee { get; set; }
        Guid EmployeeId { get; set; }
        Guid Id { get; set; }
        string JobTitle { get; set; }
        uint YearsEmployed { get; }
    }
}