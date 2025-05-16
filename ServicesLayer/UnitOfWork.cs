using DomainLayer.Defaults;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAccountInfo;
using DomainLayer.Models.EmployeeAttendance;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using DomainLayer.Services;
using InfrastructureLayer.DataAccess;
using InfrastructureLayer.DataAccess.Repositories.Common;
using ServicesLayer.Common;
using ServicesLayer.Enums;



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

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(AppDbContext));

            _adminRepository ??= new BaseRepository<AdminModel>(_context);
            _contractorRepository ??= new BaseRepository<ContractorModel>(_context);
            _departmentRepository ??= new BaseRepository<DepartmentModel>(_context);
            _employeeRepository ??= new BaseRepository<EmployeeModel>(_context);
            _holidayRepository ??= new BaseRepository<HolidayModel>(_context);
            _roleRepository ??= new BaseRepository<RoleModel>(_context);
            _userRepository ??= new BaseRepository<UserModel>(_context);

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
            //await Save();
            await SeedAdminUser();
            await SeedEmployeeUser();
            await Save();
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
                    //Status = FormStatus.Approved
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
                    PagIbigIdNumber = "1434-5678-9012",
                    BankName = "Landbank",
                    BankAccountName = "JANE JOHN S. DOE",
                    BankAccountId = "4748-4478-9012-3456",

                    CompanyId = "#598764",
                    Role = "Product Designer",
                    EmploymentType = EmploymentType.Regular,
                    DateHired = new DateOnly(1997, 1, 27)
                };
                employeeRole.Users.Add(user);
                department.Users.Add(user);

                //await RoleRepository.UpdateAsync(employeeRole);
                //await DepartmentRepository.UpdateAsync(department);
            }
        }
        private async Task SeedHolidays()
        {
            var holidays = await HolidayRepository.GetManyAsync();

            if (holidays.Count() == 0)
            {
                var defaults = new DefaultHolidays();
                await HolidayRepository.AddRangeAsync(defaults.DefaultHolidaysList);
                await Save();
            }
        }

        private async Task SeedDepartments()
        {
            var departments = await DepartmentRepository.GetManyAsync();
            if (departments.Count() == 0)
            {
                string[] defaultDepartments =
                {
                    "Management",
                    "Finance & Operations",
                    "Administration",
                    "Partner",
                    "Individual Contractor",
                    "Unassigned"
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
                await Save();
            }
        }

        private async Task SeedRoles()
        {
            string[] defaultRoles =
            {
                "Admin",
                "Employee",
                "Contractor",
                "No Access"
            };
            var roles = await RoleRepository.GetManyAsync();
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
                await Save();
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
                    //Status = FormStatus.Approved
                };

                user.Admin = new AdminModel()
                {
                    UserId = user.UserId,
                    User = user
                };

                adminRole.Users.Add(user);
                department.Users.Add(user);

                //await RoleRepository.UpdateAsync(adminRole);
                //await DepartmentRepository.UpdateAsync(department);
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<RegisterUserResult> RegisterUser(string username, string email, string password, string confirmPassword, string roleName = null, string departmentName = null)
        {
            var user = await UserRepository.GetAsync(u => u.Username == username || u.Email == email);
            if (user != null)
            {
                return RegisterUserResult.UserAlreadyExists;
            }
            if (!_modelDataAnnotationsCheck.IsValidEmail(email))
            {
                return RegisterUserResult.InvalidEmail;
            }
            if (password != confirmPassword)
            {
                return RegisterUserResult.PasswordMismatch;
            }
            if (password.Length < 8)
            {
                return RegisterUserResult.WeakPassword;
            }
            if (string.IsNullOrEmpty(roleName))
            {
                roleName = "No Access";
            }
            if (string.IsNullOrEmpty(departmentName))
            {
                departmentName = "Unassigned";
            }

            var role = await RoleRepository.GetAsync(r => r.NormalizedName == roleName.ToUpperInvariant(), includeProperties: "Users");
            var department = await DepartmentRepository.GetAsync(d => d.NormalizedName == departmentName.ToUpperInvariant(), includeProperties: "Users");

            if (role == null || department == null)
            {
                return RegisterUserResult.UnknownError;
            }

            var newUser = new UserModel()
            {
                Username = username,
                Password = password,
                Email = email,
                RoleId = role.RoleId,
                Role = role,
                DepartmentId = department.DepartmentId,
                Department = department,
            };

            role.Users.Add(newUser);
            department.Users.Add(newUser);

            //await RoleRepository.UpdateAsync(role);
            //await DepartmentRepository.UpdateAsync(department);

            await Save();

            return RegisterUserResult.Success;
        }

        public async Task UpdateEmployeeAttendanceRecords(Guid EmployeeId)
        {
            //TO DO - Add logic to update employee attendance records
            var employee = await EmployeeRepository.GetAsync(e => e.EmployeeId == EmployeeId, includeProperties: "User,EmployeeAttendances,EmployeeLeaves,EmployeePayslips");
            if (employee != null && employee.EmployeeAttendances != null && employee.EmployeePayslips != null)
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly startDate;
                DateOnly endDate;
                if (today.Day < 16)
                {
                    startDate = new DateOnly(today.Year, today.Month, 1);
                    endDate = new DateOnly(today.Year, today.Month, 15);
                }
                else
                {
                    startDate = new DateOnly(today.Year, today.Month, 16);
                    endDate = new DateOnly(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));
                }
                //Get all employee attendance records between start and end date
                var attendances = employee.EmployeeAttendances.Where(a => IsDateBetween(a.Date, startDate, endDate)).ToList();
                var holidays = await HolidayRepository.GetManyAsync(h => IsDateBetween(h.Date, startDate, today));

                var payslip = employee.EmployeePayslips.FirstOrDefault(p => p.PeriodStart == startDate && p.PeriodEnd == endDate);
                
                //Iterate through work days
                for (DateOnly date = startDate; date < today; date = date.AddDays(1))
                {

                    var attendance = attendances.FirstOrDefault(a => a.Date == date);
                    var holiday = holidays.FirstOrDefault(h => h.Date == date);
                    if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        //Rest Days Work Calculation Here
                        continue;
                    }
                    if (holiday != null)
                    {
                        //Holiday Work Calculation Here
                        continue;
                    }
                }
            }

            await Save();
        }

        private bool IsDateBetween(DateOnly date, DateOnly startDate, DateOnly endDate)
        {
            return date >= startDate && date <= endDate;
        }
    }
}
