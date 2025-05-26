using DomainLayer.Enums;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.ContractorPayslip;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
using DomainLayer.Models.EmployeeLeave;
using DomainLayer.Models.EmployeePayslip;
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
        public IList<EmployeeEvaluatedAttendanceModel> EmployeeEvaluatedAttendances { get; private set; } = [];
        public IList<EmployeeAttendanceRequestModel> EmployeeAttendanceRequests { get; private set; } = [];
        public IList<EmployeeModel> Employees { get; private set; } = [];
        public IList<HolidayModel> Holidays { get; private set; } = [];
        public IList<RoleModel> Roles { get; private set; } = [];
        public IList<EmployeeLeaveModel> EmployeeLeaves { get; private set; } = [];
        public IList<EmployeeLeaveModel> EmployeeLeaveRequests { get; private set; } = [];
        public IList<ContractorAttendanceLogModel> ContractorAttendanceLogs { get; private set; } = [];
        public IList<UserModel> Users { get; private set; } = [];
        //public IList<UserModel> EmployeeRequests { get; private set; } = [];
        public IList<EmployeePayslipModel> EmployeePayslips { get; private set; } = [];
        public IList<ContractorPayslipModel> ContractorPayslips { get; private set; } = [];

        public int EmployeeCount { get; private set; } = 0;
        public int ContractorCount { get; private set; } = 0;
        public int UserTotalCount { get; private set; } = 0;

        public IDictionary<string, PayDateTotalPair> SummarizedPayrolls { get; private set; } = new Dictionary<string, PayDateTotalPair>();


        public AdminOperationsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel?> InitializeService(Guid adminUserGuid)
        {
            var user = await _unitOfWork.UserRepository
                .GetAsync(u => u.UserId == adminUserGuid
                    , includeProperties: "Role,Admin,AccountInfo");

            if (user == null)
                throw new ArgumentException("User not found.");

            if (!user.Role.NormalizedName.Equals("admin", StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("This user has no administrator privillages!");

            AdminUser = user;

            //Cache all data
            await RefreshContractorAttendanceLogs();
            await RefreshContractorPayslips();
            await RefreshContractors();
            await RefreshDepartments();
            await RefreshHolidays();
            await RefreshRoles();
            await RefreshEmployeeAttendanceLogs();
            await RefreshEmployeeAttendanceRequests();
            await RefreshEmployeeEvaluatedAttendances();
            await RefreshEmployees();
            await RefreshEmployeeLeaves();
            await RefreshEmployeePayslips();
            await RefreshUsers();
            await RecountPopulation();
            await SummarizePayrolls();

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

        public async Task RefreshHolidays()
        {
            var holidays = await _unitOfWork.HolidayRepository.GetAllAsync();
            Holidays = holidays.ToList();
        }

        public async Task RefreshDepartments()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetManyAsync(d => d.NormalizedName != "unassigned".ToUpperInvariant());
            Departments = departments.ToList();
        }

        public async Task RefreshRoles()
        {
            var roles = await _unitOfWork.RoleRepository.GetManyAsync(r => r.NormalizedName != "no access".ToUpperInvariant());
            Roles = roles.ToList();
        }

        public async Task RefreshContractors()
        {
            var contractors = await _unitOfWork.ContractorRepository.GetAllAsync();
            Contractors = contractors.ToList();
        }

        public async Task RefreshEmployees()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            Employees = employees.ToList();
        }

        public async Task RefreshEmployeeAttendanceRequests()
        {
            var requests = await _unitOfWork.EmployeeAttendanceRequestRepository.GetAllAsync();
            EmployeeAttendanceRequests = requests.ToList();
        }

        public async Task RefreshEmployeePayslips()
        {
            var employeePayslips = await _unitOfWork.EmployeePayslipRepository.GetAllAsync(include =>
                include.Include(p => p.Employee.User.AccountInfo));
            EmployeePayslips = employeePayslips.ToList();
        }

        public async Task RefreshContractorPayslips()
        {
            var contractorPayslips = await _unitOfWork.ContractorPayslipRepository.GetAllAsync(include =>
                include.Include(c => c.Contractor.User.AccountInfo));
            ContractorPayslips = contractorPayslips.ToList();
        }

        public async Task RecountPopulation()
        {
            var noAccessRole = await _unitOfWork.RoleRepository.GetAsync(r => r.NormalizedName == "no access".ToUpperInvariant());
            var users = await _unitOfWork.UserRepository.GetManyAsync(u => u.RoleId != noAccessRole.RoleId, includeProperties: "Role,Department");
            UserTotalCount = users.Count();
            ContractorCount = users.Where(u => u.Role.NormalizedName == "contractor".ToUpperInvariant()).Count();
            EmployeeCount = UserTotalCount - ContractorCount;
        }

        public async Task RefreshEmployeeAttendanceLogs()
        {
            var logs = await _unitOfWork.EmployeeAttendanceLogRepository.GetAllAsync(include =>
                include.Include(e => e.Employee.User.AccountInfo));
            EmployeeAttendanceLogs = logs.ToList();
        }

        public async Task RefreshEmployeeEvaluatedAttendances()
        {
            var evals = await _unitOfWork.EmployeeEvaluatedAttendanceRepository.GetAllAsync(include =>
                include.Include(e => e.Employee.User.AccountInfo));
            EmployeeEvaluatedAttendances = evals.ToList();
        }

        public async Task RefreshContractorAttendanceLogs()
        {
            var logs = await _unitOfWork.ContractorAttendanceLogRepository.GetAllAsync(include =>
                include.Include(c => c.Contractor.User.AccountInfo));
            ContractorAttendanceLogs = logs.ToList();
        }

        public async Task RefreshEmployeeLeaves()
        {
            var leaves = await _unitOfWork.EmployeeLeaveRepository.GetAllAsync(include =>
                include.Include(e => e.Employee.User.AccountInfo));
            EmployeeLeaves = leaves.ToList();
        }

        public async Task SummarizePayrolls()
        {
            var employeePayslips = await _unitOfWork.EmployeePayslipRepository.GetAllAsync();
            var contractorPayslips = await _unitOfWork.ContractorPayslipRepository.GetAllAsync();

            var groupedEmployeePayslips = employeePayslips.OrderBy(e => e.PeriodStart).GroupBy(e => e.PayPeriod);
            var groupedContractorPayslips = contractorPayslips.OrderBy(c => c.PeriodStart).GroupBy(e => e.PayPeriod);

            var dictionary = new Dictionary<string, PayDateTotalPair>();

            foreach (var group in groupedEmployeePayslips)
            {
                var sum = group.Sum(p => p.NetSalary);
                if (!dictionary.ContainsKey(group.Key))
                {
                    dictionary
                        .Add(group.Key,
                            new PayDateTotalPair()
                            {
                                PayDate = group.First().PayDate.ToString("MMMM dd, yyyy"),
                                Total = sum
                            });
                }
                else
                {
                    dictionary[group.Key].Total += sum;
                }
            }

            foreach (var group in groupedContractorPayslips)
            {
                var sum = group.Sum(c => c.NetPay);
                if (!dictionary.ContainsKey(group.Key))
                {
                    dictionary
                        .Add(group.Key,
                            new PayDateTotalPair()
                            {
                                PayDate = group.First().PayDate.ToString("MMMM dd, yyyy")
                            });

                }
                else
                {
                    dictionary[group.Key].Total += sum;
                }
            }
            SummarizedPayrolls = dictionary;
        }

        public async Task RevertInitialState()
        {
            await _unitOfWork.Save();

            //Clear all lists
            Contractors.Clear();
            Departments.Clear();
            EmployeeAttendanceLogs.Clear();
            EmployeeEvaluatedAttendances.Clear();
            EmployeeAttendanceRequests.Clear();
            Employees.Clear();
            Holidays.Clear();
            Roles.Clear();
            EmployeeLeaves.Clear();
            ContractorAttendanceLogs.Clear();
            ContractorPayslips.Clear();
            EmployeePayslips.Clear();
            Users.Clear();


            EmployeeCount = 0;
            ContractorCount = 0;

            SummarizedPayrolls.Clear();

            AdminUser = null;
        }

        public async Task AddDepartment(DepartmentModel department)
        {
            try
            {
                _unitOfWork.DepartmentRepository.ValidateModelDataAnnotations(department);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            await _unitOfWork.DepartmentRepository.AddAsync(department);
            await _unitOfWork.Save();
        }

        public async Task DeleteDepartment(DepartmentModel department)
        {
            try
            {
                await _unitOfWork.DepartmentRepository.RemoveAsync(department);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task AddHoliday(HolidayModel holiday)
        {
            try
            {
                _unitOfWork.HolidayRepository.ValidateModelDataAnnotations(holiday);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            await _unitOfWork.HolidayRepository.AddAsync(holiday);
            await _unitOfWork.Save();
        }

        public async Task DeleteHoliday(HolidayModel holiday)
        {
            try
            {
                await _unitOfWork.HolidayRepository.RemoveAsync(holiday);
                await _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task RefreshUsers()
        {
            var users = await _unitOfWork.UserRepository.GetManyAsync(includeProperties: "Role,Department,AccountInfo");
            Users = users.ToList();
        }
    }
    public class PayDateTotalPair
    {
        public string PayDate { get; set; } = string.Empty;
        public decimal Total { get; set; }

    }
}
