using DomainLayer.Enums;

namespace DomainLayer.Models.ChangePasswordRequest
{
    public interface IForgotPasswordRequestModel
    {
        string ConfirmPassword { set; }
        DateOnly DateOfRequest { get; }
        string Email { get; set; }
        Guid Id { get; set; }
        string Password { set; }
        byte[] PasswordHash { get; }
        byte[] Salt { get; }
        FormStatus Status { get; set; }
        TimeOnly TimeOfRequest { get; }
        string UserName { get; set; }
    }
}