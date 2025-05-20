using DomainLayer.Defaults;
using DomainLayer.Enums;
using DomainLayer.Enums.EmployeeAttendanceLog;
using DomainLayer.Enums.EmployeeEvaluatedAttendance;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAccountInfo;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
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

            await Save();

            return RegisterUserResult.Success;
        }

        public async Task EvaluateAllEmployeeAttendanceLog(DateOnly periodStart, DateOnly periodEnd)
        {
            var employees = await EmployeeRepository.GetManyAsync(includeProperties: "EmployeeAttendanceLogs,EmployeeAttendanceRequests,EmployeeLeaveRequests,EmployeeEvaluatedAttendances");
            var holidays = await HolidayRepository.GetManyAsync(h => IsDateBetween(h.Date, periodStart, periodEnd));
            foreach (var employee in employees)
            {
                var attendanceLogs = employee.EmployeeAttendanceLogs
                    .Where(log => IsDateBetween(log.Date, periodStart, periodEnd))
                    .ToList();
                var attendanceRequests = employee.EmployeeAttendanceRequests
                    .Where(request => IsDateBetween(request.RequestDate, periodStart, periodEnd))
                    .ToList();
                var leaveRequests = employee.EmployeeLeaveRequests
                    .Where(request => IsDateBetween(request.DateOfAbsenceStart, periodStart, periodEnd))
                    .ToList();

                var today = DateOnly.FromDateTime(DateTime.Now);

                //Reset absences
                if (employee.Absences > 0)
                {
                    employee.Absences = 0;
                }

                // Day to day evaluation
                for (var currentDay = periodStart; currentDay <= periodEnd; currentDay = currentDay.AddDays(1))
                {
                    //Check if attendance is still ongoing
                    if (currentDay > today)
                    {
                        break;
                    }

                    //Check whether an evaluated attendance already exists
                    var evaluatedAttendance = employee.EmployeeEvaluatedAttendances
                        .FirstOrDefault(e => e.Date == currentDay);
                    //Creates new evaluated attendance if none exists
                    if (evaluatedAttendance == null)
                    {
                        evaluatedAttendance = new EmployeeEvaluatedAttendanceModel()
                        {
                            EmployeeId = employee.EmployeeId,
                            Employee = employee,
                            Date = currentDay,
                        };
                        employee.EmployeeEvaluatedAttendances.Add(evaluatedAttendance);
                    }
                    evaluatedAttendance.EvaluationTimeStamp = DateTime.Now;

                    //Check if the day is a holiday
                    var holiday = holidays
                        .FirstOrDefault(h => h.Date == currentDay);
                    if (holiday != null)
                    {
                        evaluatedAttendance.DayStatus = EvaluatedAttendanceDayStatus.Holiday;
                        evaluatedAttendance.ExpectedWorkHours = 0;
                        evaluatedAttendance.ActualWorkHours = 0;
                        continue;
                    }

                    //Check if the day is a weekend
                    if (currentDay.DayOfWeek == DayOfWeek.Saturday || currentDay.DayOfWeek == DayOfWeek.Sunday)
                    {
                        evaluatedAttendance.DayStatus = EvaluatedAttendanceDayStatus.RestDay;
                        evaluatedAttendance.ExpectedWorkHours = 0;
                        evaluatedAttendance.ActualWorkHours = 0;
                        continue;
                    }

                    //Check if there are any approved leave requests for the day
                    var leaveRequest = leaveRequests
                        .FirstOrDefault(l => l.DateOfAbsenceStart <= currentDay && l.DateOfReturn > currentDay && l.Status == FormStatus.Approved);
                    if (leaveRequest != null)
                    {
                        //Spend leave credits
                        if (employee.LeaveCredits > 0)
                        {
                            employee.LeaveCredits -= 1;
                        }
                        else
                        {
                            employee.Absences += 1;
                        }

                        evaluatedAttendance.DayStatus = EvaluatedAttendanceDayStatus.OnLeave;
                        evaluatedAttendance.ExpectedWorkHours = 0;
                        evaluatedAttendance.ActualWorkHours = 0;
                        continue;
                    }

                    //Check if there are any approved attendance requests for the day
                    var attendanceRequest = attendanceRequests.FirstOrDefault(r => r.AttendanceDate == currentDay && r.Status == FormStatus.Approved);
                    if (attendanceRequest != null)
                    {
                        evaluatedAttendance.DayStatus = EvaluatedAttendanceDayStatus.Present;
                        evaluatedAttendance.ExpectedWorkHours = employee.ExpectedWorkHours;
                        evaluatedAttendance.ActualWorkHours = attendanceRequest.TotalHours;
                        continue;
                    }

                    //Obtains valid attendance logs for current day
                    var logsToday = attendanceLogs
                    .Where(log => log.Date == currentDay)
                    .ToList();

                    //Check if present, ongoing, or absent
                    if (currentDay == today)
                    {
                        if (logsToday.Count < 4)
                        {
                            evaluatedAttendance.DayStatus = EvaluatedAttendanceDayStatus.OnGoing;
                            evaluatedAttendance.ExpectedWorkHours = employee.ExpectedWorkHours;
                            evaluatedAttendance.ActualWorkHours = 0;
                        }
                        else
                        {
                            evaluatedAttendance.DayStatus = EvaluatedAttendanceDayStatus.Present;
                            evaluatedAttendance.ExpectedWorkHours = employee.ExpectedWorkHours;
                            evaluatedAttendance.ActualWorkHours = CalculateActualWorkHours(logsToday);
                        }
                    }
                    else
                    {
                        if (logsToday.Count < 4)
                        {
                            evaluatedAttendance.DayStatus = EvaluatedAttendanceDayStatus.Absent;
                            evaluatedAttendance.ExpectedWorkHours = employee.ExpectedWorkHours;
                            evaluatedAttendance.ActualWorkHours = 0;
                            employee.Absences += 1;
                        }
                        else
                        {
                            evaluatedAttendance.DayStatus = EvaluatedAttendanceDayStatus.Present;
                            evaluatedAttendance.ExpectedWorkHours = employee.ExpectedWorkHours;
                            evaluatedAttendance.ActualWorkHours = CalculateActualWorkHours(logsToday);
                        }
                    }
                }
            }
            await Save();
        }

        private decimal CalculateActualWorkHours(IEnumerable<EmployeeAttendanceLogModel> logsToday)
        {
            var timeIn = logsToday.FirstOrDefault(l => l.EventType == AttendanceLogEventType.TimeIn);
            var breakStart = logsToday.FirstOrDefault(l => l.EventType == AttendanceLogEventType.BreakStart);
            var breakEnd = logsToday.FirstOrDefault(l => l.EventType == AttendanceLogEventType.BreakEnd);
            var timeOut = logsToday.FirstOrDefault(l => l.EventType == AttendanceLogEventType.TimeOut);

            decimal result = 0;

            if (timeIn != null && timeOut != null)
            {
                result = (decimal)Math.Floor((timeOut.TimeStamp - timeIn.TimeStamp).TotalHours);
                if (breakStart != null && breakEnd != null)
                {
                    result -= (decimal)Math.Floor((breakEnd.TimeStamp - breakStart.TimeStamp).TotalHours);
                }
                else
                    result = 0;
            }
            return result;
        }

        private bool IsDateBetween(DateOnly date, DateOnly startDate, DateOnly endDate)
        {
            return date >= startDate && date <= endDate;
        }
    }
}
