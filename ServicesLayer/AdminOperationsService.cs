using DomainLayer.Models.Department;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using DomainLayer.Services;

namespace ServicesLayer
{
    public class AdminOperationsService : IAdminOperationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserModel? AdminUser { get; private set; }


        public AdminOperationsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel?> InitializeService(Guid adminUserGuid)
        {
            var user = await _unitOfWork.UserRepository
                .GetAsync(u => u.UserId == adminUserGuid
                    , includeProperties: "Role,Admin");

            if (user == null)
                throw new ArgumentException("User not found.");

            if (!user.Role.NormalizedName.Equals("admin", StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("This user has no administrator privillages!");

            AdminUser = user;
            return AdminUser;
        }

        public bool ConfirmAction(string password)
        {
            if (AdminUser == null)
                throw new ArgumentException("Administration service is uninitialized!");

            var encryptor = new Encryption();
            var pass = encryptor.GenerateHash(password, AdminUser.Salt);
            if (pass != null && pass.SequenceEqual(AdminUser.PasswordHash))
                return true;
            return false;
        }

        //Admin Page Operations
        public async Task<IList<DepartmentModel>> GetDepartmentsList()
        {
            if (AdminUser == null)
                ThrowAccessDeniedException();

            var departments = await _unitOfWork.DepartmentRepository.GetManyAsync(d => d.NormalizedName != "UNASSIGNED".ToUpperInvariant());
            return [.. departments];
        }
        public async Task<IList<RoleModel>> GetRolesList()
        {
            if (AdminUser == null)
                ThrowAccessDeniedException();
            var roles = await _unitOfWork.RoleRepository.GetManyAsync(r => r.NormalizedName != "no access".ToUpperInvariant());
            return [.. roles];
        }
        public async Task<IList<HolidayModel>> GetHolidayList()
        {
            if (AdminUser == null)
                ThrowAccessDeniedException();

            var holidays = await _unitOfWork.HolidayRepository.GetAllAsync();
            return [.. holidays];
        }
        public async Task<int> GetEmployeeCount()
        {
            if (AdminUser == null)
                ThrowAccessDeniedException();
            var employees1 = await _unitOfWork.EmployeeRepository.GetAllAsync();
            var employees2 = await _unitOfWork.ContractorRepository.GetAllAsync();

            return employees1.Count() + employees2.Count();
        }

        private static void ThrowAccessDeniedException()
        {
            throw new ArgumentException("Access denied!");
        }

    }
}
