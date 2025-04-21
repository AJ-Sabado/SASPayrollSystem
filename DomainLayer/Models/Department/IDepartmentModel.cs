using DomainLayer.Models.User;

namespace DomainLayer.Models.Department
{
    public interface IDepartmentModel
    {
        ICollection<UserModel> Users { get; }
        Guid DepartmentId { get; set; }
        string Name { get; set; }
        string NormalizedName { get; }
    }
}