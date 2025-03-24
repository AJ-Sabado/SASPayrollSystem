using DomainLayer.Models.Employee;
using DomainLayer.Models.ForgotPasswordRequest;
using DomainLayer.Models.Role;

namespace DomainLayer.Models.User
{
    public interface IUserModel
    {
        string? Email { get; set; }
        EmployeeModel? Employee { get; set; }
        Guid? EmployeeId { get; set; }
        Guid Id { get; set; }
        string Password { set; }
        byte[] PasswordHash { get; }
        string? PhoneNumber { get; set; }
        RoleModel Role { get; set; }
        Guid RoleId { get; set; }
        byte[] Salt { get; }
        string? Url { get; set; }
        string UserName { get; set; }

        void ConfirmPasswordChange(IForgotPasswordRequestModel request);
    }
}