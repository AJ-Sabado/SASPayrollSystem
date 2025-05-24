using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
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

        //Cached Tables and data
        public IList<ContractorModel> Contractors { get; private set; } = [];
        public IList<DepartmentModel> Departments { get; private set; } = [];
        public IList<EmployeeModel> Employees { get; private set; } = [];
        public IList<HolidayModel> Holidays { get; private set; } = [];
        public IList<RoleModel> Roles { get; private set; } = [];

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

            //Load Tables
            await RefreshHolidaysTable();
            await RefreshDepartmentsTable();
            await RefreshRolesTable();
            await RefreshContractorsTable();
            await RefreshEmployeesTable();

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

        public async Task RefreshHolidaysTable()
        {
            var holidays = await _unitOfWork.HolidayRepository.GetAllAsync();
            Holidays = holidays.ToList();
        }

        public async Task RefreshDepartmentsTable()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetManyAsync(d => d.NormalizedName != "unassigned".ToUpperInvariant());
            Departments = departments.ToList();
        }

        public async Task RefreshRolesTable()
        {
            var roles = await _unitOfWork.RoleRepository.GetManyAsync(r => r.NormalizedName != "no access".ToUpperInvariant());
            Roles = roles.ToList();
        }

        public async Task RefreshContractorsTable()
        {
            var contractors = await _unitOfWork.ContractorRepository.GetAllAsync();
            Contractors = contractors.ToList();
        }

        public async Task RefreshEmployeesTable()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            Employees = employees.ToList();
        }
    }
}
