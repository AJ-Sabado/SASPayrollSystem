using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeeContactInfo
{
    public interface IEmployeeContactInfo
    {
        string Email { get; }
        EmployeeModel Employee { get; set; }
        Guid EmployeeId { get; set; }
        Guid Id { get; set; }
        string MailingAddress { get; set; }
        string PhoneNumber { get; set; }
        string? PhoneNumberAlt { get; set; }
        string? Telephone { get; set; }
    }
}