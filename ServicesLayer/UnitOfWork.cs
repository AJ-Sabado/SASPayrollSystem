using DomainLayer.Defaults;
using DomainLayer.Enums;
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

        public async Task LoginUser(string username, string password)
        {
            throw new NotImplementedException();
        }


        public async Task InitialSeeding()
        {
            await SeedRoles();
            await SeedDepartments();
            await SeedAdmin();
            await SeedEmployee();
            await SeedHolidays();
        }

        private async Task SeedEmployee()
        {
            var employeeRole = await RoleRepository.GetAsync(r => r.NormalizedName == "employee".ToUpperInvariant(), includeProperties: "Users");
            if (employeeRole.Users.Count() == 0)
            {
                var employeeUser = new UserModel()
                {
                    Username = "user1",
                    Password = "password",
                    RoleId = employeeRole.RoleId,
                    Role = employeeRole,
                    Status = FormStatus.Approved
                };

                var employee = new EmployeeModel()
                {
                    UserId = employeeUser.UserId,
                    User = employeeUser,
                    BasicMonthlyRate = 20000,
                    BasicDailyRate = 919.54m
                };
                employee.EmployeeAccountInfo = new EmployeeAccountInfoModel()
                {
                    EmployeeId = employee.EmployeeId,
                    Employee = employee

                    //TO DO - Add sample details
                };

                employeeRole.Users.Add(employeeUser);
                await RoleRepository.UpdateAsync(employeeRole);
                await EmployeeRepository.AddAsync(employee);
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

        private async Task SeedAdmin()
        {
            var adminRole = await RoleRepository.GetAsync(r => r.NormalizedName == "admin".ToUpperInvariant(), includeProperties: "Users");

            if (adminRole.Users.Count() == 0)
            {
                var adminUser = new UserModel()
                {
                    Username = "admin",
                    Password = "password",
                    RoleId = adminRole.RoleId,
                    Role = adminRole,
                    Status = FormStatus.Approved
                };

                var adminModel = new AdminModel()
                {
                    UserId = adminUser.UserId,
                    User = adminUser
                };

                adminRole.Users.Add(adminUser);
                await RoleRepository.UpdateAsync(adminRole);
                await AdminRepository.AddAsync(adminModel);
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
