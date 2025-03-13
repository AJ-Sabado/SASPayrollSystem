using DomainLayer.Common;
using DomainLayer.Exceptions;
using DomainLayer.Models.Attendance;
using DomainLayer.Models.ForgotPasswordRequest;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.Leave;
using DomainLayer.Models.NewUserRequest;
using DomainLayer.Models.Payroll;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using InfrastructureLayer.DataAccess;
using InfrastructureLayer.DataAccess.Repositories.Common;
using ServicesLayer.Common;
using ServicesLayer.Exceptions;
using System.Text;



namespace ServicesLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;

        public IUserModel? CurrentUser { get; private set; } = null;

        //Repositories
        private readonly IBaseRepository<AttendanceModel> _attendanceRepository;
        private readonly IBaseRepository<ForgotPasswordRequestModel> _forgotPasswordRequestRepository;
        private readonly IBaseRepository<DepartmentModel> _departmentRepository;
        private readonly IBaseRepository<EmployeeModel> _employeeRepository;
        private readonly IBaseRepository<LeaveModel> _leaveRepository;
        private readonly IBaseRepository<NewUserRequestModel> _newUserRequestRepository;
        private readonly IBaseRepository<PayrollModel> _payrollRepository;
        private readonly IBaseRepository<RoleModel> _roleRepository;
        private readonly IBaseRepository<UserModel> _userRepository;

        //Common Services
        private IModelDataAnnotationsCheck _modelDataAnnotationsCheck;

        //Services List
        public IBaseServices<AttendanceModel> AttendanceRepository { get; private set; }
        public IBaseServices<ForgotPasswordRequestModel> ForgotPasswordRequestRepository { get; private set; }
        public IBaseServices<DepartmentModel> DepartmentRepository { get; private set; }
        public IBaseServices<EmployeeModel> EmployeeRepository { get; private set; }
        public IBaseServices<LeaveModel> LeaveRepository { get; private set; }
        public IBaseServices<NewUserRequestModel> NewUserRequestRepository { get; private set; }
        public IBaseServices<PayrollModel> PayrollRepository { get; private set; }
        public IBaseServices<RoleModel> RoleRepository { get; private set; }
        public IBaseServices<UserModel> UserRepository { get; private set; }

        public UnitOfWork()
        {
            _db = new AppDbContext();

            _attendanceRepository ??= new BaseRepository<AttendanceModel>();
            _forgotPasswordRequestRepository ??= new BaseRepository<ForgotPasswordRequestModel>();
            _departmentRepository ??= new BaseRepository<DepartmentModel>();
            _employeeRepository ??= new BaseRepository<EmployeeModel>();
            _leaveRepository ??= new BaseRepository<LeaveModel>();
            _newUserRequestRepository ??= new BaseRepository<NewUserRequestModel>();
            _payrollRepository ??= new BaseRepository<PayrollModel>();
            _roleRepository ??= new BaseRepository<RoleModel>();
            _userRepository ??= new BaseRepository<UserModel>();

            _modelDataAnnotationsCheck ??= new ModelDataAnnotationsCheck();

            AttendanceRepository ??= new BaseServices<AttendanceModel>(_attendanceRepository, _modelDataAnnotationsCheck);
            ForgotPasswordRequestRepository ??= new BaseServices<ForgotPasswordRequestModel>(_forgotPasswordRequestRepository, _modelDataAnnotationsCheck);
            DepartmentRepository ??= new BaseServices<DepartmentModel>(_departmentRepository, _modelDataAnnotationsCheck);
            EmployeeRepository ??= new BaseServices<EmployeeModel>(_employeeRepository, _modelDataAnnotationsCheck);
            LeaveRepository ??= new BaseServices<LeaveModel>(_leaveRepository, _modelDataAnnotationsCheck);
            NewUserRequestRepository ??= new BaseServices<NewUserRequestModel>(_newUserRequestRepository, _modelDataAnnotationsCheck);
            PayrollRepository ??= new BaseServices<PayrollModel>(_payrollRepository, _modelDataAnnotationsCheck);
            RoleRepository ??= new BaseServices<RoleModel>(_roleRepository, _modelDataAnnotationsCheck);
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
            await SeedAdmin();
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
            }
            await RoleRepository.AddRangeAsync(rolemodels);
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
            _db.SaveChanges();
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
    }
}
