using DomainLayer.Models.Department;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;

namespace ServicesLayer
{
    public interface IAdminOperationsService
    {
        UserModel? AdminUser { get; }

        bool ConfirmAction(string password);
        Task<IList<DepartmentModel>> GetDepartmentsList();
        Task<IList<HolidayModel>> GetHolidayList();
        Task<IList<RoleModel>> GetRolesList();
        Task<int> GetEmployeeCount();
        Task<UserModel?> InitializeService(Guid adminUserGuid);
    }
}