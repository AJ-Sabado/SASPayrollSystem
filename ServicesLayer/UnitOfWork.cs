using DomainLayer.Defaults;
using DomainLayer.Enums;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Helpers;
using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAccountInfo;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using InfrastructureLayer.DataAccess;
using InfrastructureLayer.DataAccess.Repositories.Common;
using ServicesLayer.Common;



namespace ServicesLayer
{
    public class UnitOfWork : IUnitOfWork
    {

        //Repositories
        private IBaseRepository<AdminModel> _adminRepository;
        private IBaseRepository<ContractorModel> _contractorRepository;
        private IBaseRepository<DepartmentModel> _departmentRepository;
        private IBaseRepository<EmployeeModel> _employeeRepository;
        private IBaseRepository<HolidayModel> _holidayRepository;
        private IBaseRepository<RoleModel> _roleRepository;
        private IBaseRepository<UserModel> _userRepository;

        //Common Services
        private IModelDataAnnotationsCheck _modelDataAnnotationsCheck;

        //Services List
        public IBaseServices<AdminModel> AdminRepository { get; private set; }
        public IBaseServices<ContractorModel> ContractorRepository { get; private set; }
        public IBaseServices<DepartmentModel> DepartmentRepository { get; private set; }
        public IBaseServices<EmployeeModel> EmployeeRepository { get; private set; }
        public IBaseServices<HolidayModel> HolidayRepository { get; private set; }
        public IBaseServices<RoleModel> RoleRepository { get; private set; }
        public IBaseServices<UserModel> UserRepository { get; private set; }

        public UnitOfWork()
        {
            _adminRepository ??= new BaseRepository<AdminModel>();
            _contractorRepository ??= new BaseRepository<ContractorModel>();
            _departmentRepository ??= new BaseRepository<DepartmentModel>();
            _employeeRepository ??= new BaseRepository<EmployeeModel>();
            _holidayRepository ??= new BaseRepository<HolidayModel>();
            _roleRepository ??= new BaseRepository<RoleModel>();
            _userRepository ??= new BaseRepository<UserModel>();

            _modelDataAnnotationsCheck ??= new ModelDataAnnotationsCheck();

            AdminRepository ??= new BaseServices<AdminModel>(_adminRepository, _modelDataAnnotationsCheck);
            ContractorRepository ??= new BaseServices<ContractorModel>(_contractorRepository, _modelDataAnnotationsCheck);
            DepartmentRepository ??= new BaseServices<DepartmentModel>(_departmentRepository, _modelDataAnnotationsCheck);
            EmployeeRepository ??= new BaseServices<EmployeeModel>(_employeeRepository, _modelDataAnnotationsCheck);
            HolidayRepository ??= new BaseServices<HolidayModel>(_holidayRepository, _modelDataAnnotationsCheck);
            RoleRepository ??= new BaseServices<RoleModel>(_roleRepository, _modelDataAnnotationsCheck);
            UserRepository ??= new BaseServices<UserModel>(_userRepository, _modelDataAnnotationsCheck);
        }

        public async Task<UserModel?> Login(string usernameOrEmail, string password)
        {
            UserModel? user = null;
            if (_modelDataAnnotationsCheck.IsValidEmail(usernameOrEmail))
            { 
                user = await UserRepository.GetAsync(u => u.Email == usernameOrEmail, includeProperties: "Role,Department");
            }
            else
            {
                user = await UserRepository.GetAsync(u => u.Username == usernameOrEmail, includeProperties: "Role,Department");
            }
            if (user != null)
            {
                var encryption = new Encryption();
                var passwordHash = encryption.GenerateHash(password, user.Salt);
                if (user.PasswordHash.SequenceEqual(passwordHash))
                {
                    return user;
                }
            }
            return null;
        }


        public async Task InitialSeeding()
        {
            await SeedRoles();
            await SeedDepartments();
            await SeedHolidays();
            await SeedAdminUser();
            await SeedEmployeeUser();
        }

        private async Task SeedEmployeeUser()
        {
            var employeeRole = await RoleRepository.GetAsync(r => r.NormalizedName == "employee".ToUpperInvariant(), includeProperties: "Users");
            if (employeeRole.Users.Count() == 0)
            {
                var department = await DepartmentRepository.GetAsync(d => d.NormalizedName == "Finance & Operations".ToUpperInvariant(), includeProperties: "Users");
                var user = new UserModel()
                {
                    Username = "user1",
                    Password = "password",
                    Email = "test1@test.com",
                    RoleId = employeeRole.RoleId,
                    Role = employeeRole,
                    DepartmentId = department.DepartmentId,
                    Department = department,
                    Status = FormStatus.Approved
                };
                decimal monthlyRate = 15000;
                user.Employee = new EmployeeModel()
                {
                    UserId = user.UserId,
                    User = user,
                    BasicMonthlyRate = monthlyRate,
                    BasicDailyRate = SalaryConverter.ConvertMonthlyToDaily(monthlyRate)
                };
                user.Employee.EmployeeAccountInfo = new EmployeeAccountInfoModel()
                {
                    EmployeeId = user.Employee.EmployeeId,
                    Employee = user.Employee,

                    //TO DO - Add employee information after EmployeeAccountInfoModel is adjusted.
                    FirstName = "Jane John",
                    LastName = "Doe",
                    MiddleInitial = "S.",
                    Gender = Gender.Male,
                    DateOfBirth = new DateOnly(2025, 1, 26),
                    Nationality = Nationality.Filipino,

                    PrimaryPhoneNumber = "+639000000001",
                    SecondaryPhoneNumber = "+639000000002",
                    Telephone = "(8)123-4567",
                    SecondaryEmail = "secondary@test.com",
                    MailingAddress = "Blk 4, Lot 47, Villa Amparo Subdivision, Brgy. Sylvacion, Panabo City",
                    FacebookUrl = "https://www.facebook.com/",
                    LinkedInUrl = "https://www.linkedin.com/",
                    WebsiteUrl = "https://github.com/",

                    TaxIdNumber = "123-456-789-012",
                    SSSIdNumber = "123-4567890-0",
                    PhilHealthIdNumber = "12-34567890-1",
                    PagIbigIdNumber  = "1434-5678-9012",
                    BankName = "Landbank",
                    BankAccountName = "JANE JOHN S. DOE",
                    BankAccountId = "4748-4478-9012-3456",

                    CompanyId = "598764",
                    Role = "Product Designer",
                    EmploymentType = EmploymentType.Regular,
                    DateHired = new DateOnly(1997, 1, 27)
                };
                employeeRole.Users.Add(user);
                department.Users.Add(user);

                await RoleRepository.UpdateAsync(employeeRole);
                await DepartmentRepository.UpdateAsync(department);
            }
        }
        private async Task SeedHolidays()
        {
            var holidays = await HolidayRepository.GetAllAsync();

            if (holidays.Count() == 0)
            {
                var defaults = new DefaultHolidays();
                await HolidayRepository.AddRangeAsync(defaults.DefaultHolidaysList);
            }
        }

        private async Task SeedDepartments()
        {
            var departments = await DepartmentRepository.GetAllAsync();
            if (departments.Count() == 0)
            {
                string[] defaultDepartments =
                {
                    "Management",
                    "Finance & Operations",
                    "Administration",
                    "Partner",
                    "Individual Contractor"
                };
                var departmentModels = new List<DepartmentModel>();
                foreach (var department in defaultDepartments)
                {
                    var model = new DepartmentModel()
                    {
                        Name = department
                    };
                    departmentModels.Add(model);
                }
                await DepartmentRepository.AddRangeAsync(departmentModels);
            }
        }

        private async Task SeedRoles()
        {
            string[] defaultRoles =
            {
                "admin",
                "employee",
                "contractor"
            };
            var roles = await RoleRepository.GetAllAsync();
            var rolemodels = new List<RoleModel>();
            if (roles.Count() == 0)
            {
                foreach (var defaultRole in defaultRoles)
                {
                    var role = new RoleModel()
                    {
                        Name = defaultRole,
                    };
                    rolemodels.Add(role);
                }
                await RoleRepository.AddRangeAsync(rolemodels);
            }

        }

        private async Task SeedAdminUser()
        {
            var adminRole = await RoleRepository.GetAsync(r => r.NormalizedName == "ADMIN", includeProperties: "Users");

            if (adminRole.Users.Count() == 0)
            {
                var department = await DepartmentRepository.GetAsync(r => r.NormalizedName == "Administration".ToUpperInvariant(), includeProperties: "Users");

                var user = new UserModel()
                {
                    Username = "admin",
                    Password = "password",
                    Email = "test@test.com",
                    RoleId = adminRole.RoleId,
                    Role = adminRole,
                    DepartmentId = department.DepartmentId,
                    Department = department,
                    Status = FormStatus.Approved
                };

                user.Admin = new AdminModel()
                {
                    UserId = user.UserId,
                    User = user
                };

                adminRole.Users.Add(user);
                department.Users.Add(user);

                await RoleRepository.UpdateAsync(adminRole);
                await DepartmentRepository.UpdateAsync(department);
            }
        }

        public async Task NewUserRequest(string username, string password, string email)
        {
            throw new NotImplementedException();
        }

        public async Task ApproveNewUserRequest(string requestEmail, string roleName = null)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            using (var context = new AppDbContext())
            {
                context.SaveChanges();
            }
        }

        public async Task ForgotPasswordRequest(string username, string email, string password, string confirmPassword)
        {
            throw new NotImplementedException();
        }
    }
}
