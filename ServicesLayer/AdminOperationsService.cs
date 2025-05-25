using DomainLayer.Enums;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAccountInfo;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
using DomainLayer.Models.EmployeeLeave;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using DomainLayer.Services;
using Microsoft.EntityFrameworkCore;

namespace ServicesLayer
{
    public class AdminOperationsService : IAdminOperationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserModel? AdminUser { get; private set; }

        //To List
        public IList<ContractorModel> Contractors { get; private set; } = [];
        public IList<DepartmentModel> Departments { get; private set; } = [];
        public IList<EmployeeAttendanceLogModel> EmployeeAttendanceLogs { get; private set; } = [];
        public IList<EmployeeEvaluatedAttendanceModel> EvaluatedAttendances { get; private set; } = [];
        public IList<EmployeeAttendanceRequestModel> EmployeeAttendanceRequests { get; private set; } = [];
        public IList<EmployeeModel> Employees { get; private set; } = [];
        public IList<HolidayModel> Holidays { get; private set; } = [];
        public IList<RoleModel> Roles { get; private set; } = [];
        public IList<EmployeeLeaveModel> EmployeesOnLeave { get; private set; } = [];
        public IList<EmployeeLeaveModel> EmployeeLeaveRequests { get; private set; } = [];
        public IList<ContractorAttendanceLogModel> ContractorAttendanceLogs { get; private set; } = [];
        public IList<UserModel> CurrentEmployees { get; private set; } = [];
        public IList<UserModel> EmployeeRequests { get; private set; } = [];

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
            await RefreshEmployeeAttendanceRequests();
            await RefreshEmployeeLeaves();

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

        public async Task RefreshEvaluatedAttendances(DepartmentModel? department = null, DateTime? date = null, string employeeName = null)
        {
            var eval = await _unitOfWork.EmployeeEvaluatedAttendanceRepository.GetAllAsync(include =>
                include.Include(e => e.Employee.User.AccountInfo).Include(e => e.Employee.User.Department));
            if (date.HasValue)
                eval = eval.Where(e => e.Date == DateOnly.FromDateTime(date.Value));
            if (department != null)
                eval = eval.Where(e => e.Employee.User.Department.DepartmentId.Equals(department.DepartmentId));
            if (!string.IsNullOrEmpty(employeeName))
                eval = eval.Where(e => e.Employee.User.AccountInfo.FullName.Contains(employeeName, StringComparison.InvariantCulture));
            EvaluatedAttendances = eval.ToList();
        }

        public async Task RefreshEmployeeAttendanceLogs(DateTime? date = null)
        {
            var logs = await _unitOfWork.EmployeeAttendanceLogRepository.GetAllAsync(include =>
                include.Include(e => e.Employee.User.AccountInfo));
            if (date.HasValue)
                logs = logs.Where(l => l.Date == DateOnly.FromDateTime(date.Value));
            EmployeeAttendanceLogs = logs.ToList();
        }

        public async Task RefreshEmployeeAttendanceRequests()
        {
            var requests = await _unitOfWork.EmployeeAttendanceRequestRepository.GetAllAsync();
            EmployeeAttendanceRequests = requests.ToList();
        }

        public async Task RefreshEmployeeLeaves(string employeeName = null)
        {
            var leaves = await _unitOfWork.EmployeeLeaveRepository.GetAllAsync(include =>
                include.Include(l => l.Employee.User.AccountInfo));
            if (string.IsNullOrEmpty(employeeName))
            {
                EmployeesOnLeave = leaves.Where(l => l.Status == FormStatus.Approved).ToList();
            }
            else
            {
                EmployeesOnLeave = leaves.Where(l => l.Status == FormStatus.Approved 
                    && l.Employee.User.AccountInfo.FullName.Contains(employeeName)).ToList();
            }
            EmployeeLeaveRequests = leaves.Where(l => l.Status == FormStatus.Pending || l.Status == FormStatus.Denied).ToList();
        }

        public async Task RefreshContractorAttendanceLogs(DateTime? date = null)
        {
            var logs = await _unitOfWork.ContractorAttendanceLogRepository.GetAllAsync(include =>
                include.Include(c => c.Contractor.User.AccountInfo));
            if (date.HasValue)
                logs = logs.Where(l => l.Date == DateOnly.FromDateTime(date.Value));
            ContractorAttendanceLogs = logs.ToList();
        }

        public async Task RefreshEmployees(string? name = null, RoleModel? role = null, DepartmentModel? department = null)
        {
            

            var user = await _unitOfWork.UserRepository
                .GetManyAsync(includeProperties: "AccountInfo,Department,Role");

            //Populate Current Employee List
            var filteredUser = user
                .Where(u => u.Department.NormalizedName != "unassigned".ToUpperInvariant() 
                && u.Role.NormalizedName != "no access".ToUpperInvariant());
            if (!string.IsNullOrEmpty(name))
                filteredUser = filteredUser.Where(u => u.AccountInfo.FullName.Contains(name));
            if (role != null)
                filteredUser = filteredUser.Where(u => u.RoleId == role.RoleId);
            if (department != null)
                filteredUser = filteredUser.Where(u => u.DepartmentId == department.DepartmentId);
            CurrentEmployees = filteredUser.ToList();

            //Populate Requests
            EmployeeRequests = user
                .Where(u => u.Department.NormalizedName == "unassigned".ToUpperInvariant())
                .Where(u => u.Role.NormalizedName == "no access".ToUpperInvariant())
                .ToList();
        }
    }
}
