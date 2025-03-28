﻿using DomainLayer.Common;
using DomainLayer.Exceptions;
using DomainLayer.Models.Attendance;
using DomainLayer.Models.ForgotPasswordRequest;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.Leave;
using DomainLayer.Models.NewUserRequest;
using DomainLayer.Models.Salary;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using InfrastructureLayer.DataAccess;
using InfrastructureLayer.DataAccess.Repositories.Common;
using ServicesLayer.Common;
using ServicesLayer.Exceptions;
using System.Text;
using DomainLayer.Models.Holiday;
using DomainLayer.Defaults;
using DomainLayer.Models.Payroll;
using DomainLayer.Models.Contribution;
using DomainLayer.ViewModels.DashboardDetails;
using DomainLayer.ViewModels.AttendanceLog;
using DomainLayer.ViewModels.JobDeskDetails;



namespace ServicesLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserModel? CurrentUser { get; private set; } = null;

        //Repositories
        private readonly IBaseRepository<AttendanceModel> _attendanceRepository;
        private readonly IBaseRepository<ContributionModel> _contributionRepository;
        private readonly IBaseRepository<DepartmentModel> _departmentRepository;
        private readonly IBaseRepository<EmployeeModel> _employeeRepository;
        private readonly IBaseRepository<ForgotPasswordRequestModel> _forgotPasswordRequestRepository;
        private readonly IBaseRepository<HolidayModel> _holidayRepository;
        private readonly IBaseRepository<LeaveModel> _leaveRepository;
        private readonly IBaseRepository<NewUserRequestModel> _newUserRequestRepository;
        private readonly IBaseRepository<PayrollModel> _payrollRepository;
        private readonly IBaseRepository<RoleModel> _roleRepository;
        private readonly IBaseRepository<SalaryModel> _salaryRepository;
        private readonly IBaseRepository<UserModel> _userRepository;

        //Common Services
        private IModelDataAnnotationsCheck _modelDataAnnotationsCheck;

        //Services List
        public IBaseServices<AttendanceModel> AttendanceRepository { get; private set; }
        public IBaseServices<ContributionModel> ContributionRepository { get; private set; }
        public IBaseServices<DepartmentModel> DepartmentRepository { get; private set; }
        public IBaseServices<EmployeeModel> EmployeeRepository { get; private set; }
        public IBaseServices<ForgotPasswordRequestModel> ForgotPasswordRequestRepository { get; private set; }
        public IBaseServices<HolidayModel> HolidayRepository { get; private set; }
        public IBaseServices<LeaveModel> LeaveRepository { get; private set; }
        public IBaseServices<NewUserRequestModel> NewUserRequestRepository { get; private set; }
        public IBaseServices<PayrollModel> PayrollRepository { get; private set; }
        public IBaseServices<RoleModel> RoleRepository { get; private set; }
        public IBaseServices<SalaryModel> SalaryRepository { get; private set; }
        public IBaseServices<UserModel> UserRepository { get; private set; }

        public UnitOfWork()
        {
            _attendanceRepository ??= new BaseRepository<AttendanceModel>();
            _contributionRepository ??= new BaseRepository<ContributionModel>();
            _departmentRepository ??= new BaseRepository<DepartmentModel>();
            _forgotPasswordRequestRepository ??= new BaseRepository<ForgotPasswordRequestModel>();
            _employeeRepository ??= new BaseRepository<EmployeeModel>();
            _holidayRepository ??= new BaseRepository<HolidayModel>();
            _leaveRepository ??= new BaseRepository<LeaveModel>();
            _newUserRequestRepository ??= new BaseRepository<NewUserRequestModel>();
            _payrollRepository ??= new BaseRepository<PayrollModel>();
            _roleRepository ??= new BaseRepository<RoleModel>();
            _salaryRepository ??= new BaseRepository<SalaryModel>();
            _userRepository ??= new BaseRepository<UserModel>();

            _modelDataAnnotationsCheck ??= new ModelDataAnnotationsCheck();

            AttendanceRepository ??= new BaseServices<AttendanceModel>(_attendanceRepository, _modelDataAnnotationsCheck);
            ContributionRepository ??= new BaseServices<ContributionModel>(_contributionRepository, _modelDataAnnotationsCheck);
            DepartmentRepository ??= new BaseServices<DepartmentModel>(_departmentRepository, _modelDataAnnotationsCheck);
            ForgotPasswordRequestRepository ??= new BaseServices<ForgotPasswordRequestModel>(_forgotPasswordRequestRepository, _modelDataAnnotationsCheck);
            EmployeeRepository ??= new BaseServices<EmployeeModel>(_employeeRepository, _modelDataAnnotationsCheck);
            HolidayRepository ??= new BaseServices<HolidayModel>(_holidayRepository, _modelDataAnnotationsCheck);
            LeaveRepository ??= new BaseServices<LeaveModel>(_leaveRepository, _modelDataAnnotationsCheck);
            NewUserRequestRepository ??= new BaseServices<NewUserRequestModel>(_newUserRequestRepository, _modelDataAnnotationsCheck);
            PayrollRepository ??= new BaseServices<PayrollModel>(_payrollRepository, _modelDataAnnotationsCheck);
            RoleRepository ??= new BaseServices<RoleModel>(_roleRepository, _modelDataAnnotationsCheck);
            SalaryRepository ??= new BaseServices<SalaryModel>(_salaryRepository, _modelDataAnnotationsCheck);
            UserRepository ??= new BaseServices<UserModel>(_userRepository, _modelDataAnnotationsCheck);
        }

        public async Task LoginUser(string username, string password)
        {
            var user = await UserRepository.GetAsync(u => u.UserName == username.Trim(), includeProperties: "Role")
                ?? throw new UserNotFoundException();
            var encryption = new Encryption();
            var saltedHashedPassword = encryption.GenerateHash(password, user.Salt);

            if (!saltedHashedPassword.SequenceEqual(user.PasswordHash))
            {
                throw new IncorrectPasswordException();
            }
            else
            {
                CurrentUser = user;
            }
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
            var department = await DepartmentRepository.GetAsync(d => d.NormalizedName == "management".ToUpperInvariant());

            if (employeeRole.Users.Count() == 0)
            {
                var employeeUser = new UserModel()
                {
                    UserName = "user1",
                    Password = "password",
                    Email = "test@test.com",
                    PhoneNumber = "+639000000000",
                    Url = "https://www.google.com/",
                    RoleId = employeeRole.Id,
                    Role = employeeRole
                };


                var employee = new EmployeeModel()
                {
                    FullName = "Sample Employee",
                    BirthDay = new DateOnly(2007, 1, 1),
                    EmploymentDate = DateOnly.FromDateTime(DateTime.Now),
                    JobTitle = "Sample Job Title",
                    BasicSemiMonthlyRate = 10000,
                    LeaveCredits = 0,
                    WorkShiftStart = new TimeOnly(8, 0),
                    WorkShiftEnd = new TimeOnly(17, 0),
                    DepartmentId = department.Id,
                    Department = department,
                    UserId = employeeUser.Id,
                    User = employeeUser
                };

                employee.Contribution = new ContributionModel()
                {
                    EmployeeId = employee.Id,
                    Employee = employee
                };

                employeeUser.Employee = employee;

                employeeRole.Users.Add(employeeUser);
                await RoleRepository.UpdateAsync(employeeRole);
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
                    UserName = "admin",
                    Password = "password",
                    RoleId = adminRole.Id,
                    Role = adminRole
                };
                adminRole.Users.Add(adminUser);
                await RoleRepository.UpdateAsync(adminRole);
            }
        }

        public async Task NewUserRequest(string username, string password, string email)
        {
            var userRequest = new NewUserRequestModel()
            {
                UserName = username,
                Password = password,
                Email = email
            };
            NewUserRequestRepository.ValidateModelDataAnnotations(userRequest);
            await NewUserRequestRepository.AddAsync(userRequest);
        }

        public async Task ApproveNewUserRequest(string requestEmail, string roleName = null)
        {
            var requestModel = await NewUserRequestRepository.GetAsync(r => r.Email == requestEmail)
                ?? throw new NewUserRequestNotFoundException();
            IRoleModel role;
            if (roleName != null)
            {
                role = await RoleRepository.GetAsync(r => r.NormalizedName == roleName.Trim().ToUpperInvariant())
                    ?? throw new RoleNotFoundException();
            }
            else
            {
                role = await RoleRepository.GetAsync(r => r.NormalizedName == "contractor".ToUpperInvariant());
            }
            var newUser = new UserModel(requestModel, role);
            role.Users.Add(newUser);
            await RoleRepository.UpdateAsync((RoleModel)role);
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
            var user = await UserRepository.GetAsync(u => u.UserName == username && u.Email == email)
                ?? throw new UserNotFoundException();
            if (password != confirmPassword)
                throw new MismatchedPasswordsException();
            var request = new ForgotPasswordRequestModel()
            {
                UserName = username,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };
            ForgotPasswordRequestRepository.ValidateModelDataAnnotations(request);
            await ForgotPasswordRequestRepository.AddAsync(request);
        }

        public DashboardDetailsViewModel GetDashboardDetails(IUserModel user)
        {
            var currentSalary = user.Employee.Salaries.LastOrDefault();
            var viewModel = new DashboardDetailsViewModel();

            if (currentSalary != null)
            {
                var totalHours = (currentSalary.DaysWorked * 8); //Temp calc
                var deductions = user.Employee.Contribution.TotalContributions + currentSalary.TaxWithholdings;

                viewModel.UpcomingDate = currentSalary.Payroll.PayrollDate.ToString("MMMM dd, yyyy");
                viewModel.TotalHours = $"{totalHours} hours";
                viewModel.TotalSalary = $"Php {currentSalary.TotalBasic}";
                viewModel.Allowance = $"Php {currentSalary.AllowancesAmount}";
                viewModel.Bonuses = $"Php {currentSalary.BonusesAmount}";
                viewModel.Deductions = $"Php {deductions}";
            }

            return viewModel;
        }

        public IEnumerable<AttendanceLogViewModel> GetAttendanceLog(IUserModel user)
        {
            var list = new List<AttendanceLogViewModel>();
            if (user.Employee != null)
            {
                foreach (var attendance in user.Employee.Attendances)
                {
                    var entry = new AttendanceLogViewModel()
                    {
                        Date = attendance.Date.ToString("MM/dd/yyyy"),
                        TimeIn = attendance.TimeIn.ToString("hh:mm:ss tt"),
                        TimeOut = attendance.TimeOut.ToString("hh:mm:ss tt"),
                        TotalHours = $"{attendance.TotalHours}",
                        Status = attendance.Status.ToString()
                    };
                    list.Add(entry);
                }
            }
            return list;
        }

        public JobDeskDetailsViewModel GetJobDeskDetails(IUserModel user)
        {
            var viewModel = new JobDeskDetailsViewModel();

            if (user.Employee != null)
            {
                viewModel.EmployeeName = user.Employee.FullName;
                viewModel.Department = user.Employee.Department.Name;
                viewModel.BaseSalary = $"Php {user.Employee.BasicSemiMonthlyRate} bimonthly";
                viewModel.WorkShift = $"{user.Employee.WorkShiftStart.ToString("hh:mm tt")}";
                viewModel.EmploymentStatus = user.Role.Name;
                viewModel.EmploymentDate = user.Employee.EmploymentDate.ToString("MMMM dd, yyyy");
            }

            if (user.Email != null)
                viewModel.Email = user.Email;
            if (user.PhoneNumber != null)
                viewModel.Phone = user.PhoneNumber;
            if (user.Url != null)
                viewModel.Website = user.Url;

            return viewModel;
        }
    }
}
