using DomainLayer.Models.Department;
using DomainLayer.Models.Role;

namespace DomainLayer.Models.User
{
    public interface IUserModel
    {
        DepartmentModel Department { get; set; }
        Guid DepartmentId { get; set; }
        string? Email { get; set; }
        Guid UserId { get; set; }
        string Password { set; }
        byte[] PasswordHash { get; set; }
        RoleModel Role { get; set; }
        Guid RoleId { get; set; }
        byte[] Salt { get; set; }
        string? Url { get; set; }
        string UserName { get; set; }
    }
}