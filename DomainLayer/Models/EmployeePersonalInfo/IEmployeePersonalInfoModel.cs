using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeePersonalInfo
{
    public interface IEmployeePersonalInfoModel
    {
        DateOnly BirthDate { get; set; }
        CivilStatusEnum CivilStatus { get; set; }
        EmployeeModel Employee { get; set; }
        Guid EmployeeId { get; set; }
        string FullName { get; set; }
        GenderEnum Gender { get; set; }
        string HomeAddress { get; set; }
        Guid Id { get; set; }
    }
}