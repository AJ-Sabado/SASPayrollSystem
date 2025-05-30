using System.Threading.Tasks;
using System.Windows.Input;
using DomainLayer.Models.Department;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Windows.PopUps;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminEmployee_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly MyMessageBox _messageBox;

        //Employees Tab
        //Header
        private string _employeeNameFilter = string.Empty;
        public string EmployeeNameFilter
        {
            get => _employeeNameFilter;
            set
            {
                _employeeNameFilter = value;
                OnPropertyChanged(nameof(EmployeeNameFilter));
                FilterCurrentEmployeesByName();
            }
        }

        private int _employeeCount = 0;
        public int EmployeeCount
        {
            get => _employeeCount;
            set
            {
                _employeeCount = value;
                OnPropertyChanged(nameof(EmployeeCount));
            }
        }

        //Filter by Role and Department
        public IList<RoleModel> Roles { get; private set; } = [];
        private RoleModel? _selectedRole = null;
        public RoleModel? SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
                FilterEmployeeByRole();
            }
        }



        public IList<DepartmentModel> Departments { get; private set; } = [];
        private DepartmentModel? _selectedDepartment = null;
        public DepartmentModel? SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
                FilterEmployeeByDepartment();
            }
        }

        //Table
        public IList<UserModel> CurrentEmployees { get; private set; } = [];

        //Onboarding Tab
        //Table
        public IList<UserModel> EmployeeRequests { get; private set; } = [];

        //Commands
        //Employee Tab
        public ICommand ResetFiltersCommand { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand ViewEmployeeCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        //Onboarding Tab
        public ICommand ViewOnboardingCommand { get; set; }
        public ICommand DeleteOnboardingCommand { get; set; }

        //CONSTRUCTOR
        public AdminEmployee_ViewModel(IPopUpService popUpService, IAdminOperationsService adminOperationsService, MyMessageBox messageBox)
        {
            _popUpService = popUpService;
            _adminOperationsService = adminOperationsService;
            _messageBox = messageBox;

            ResetFiltersCommand = new RelayCommand(ExecuteResetFilters, _ => true);
            AddEmployeeCommand = new RelayCommand(ExecuteAddEmployee, _ => true);
            ViewEmployeeCommand = new RelayCommand(ExecuteViewEmployee, _ => true);
            DeleteEmployeeCommand = new RelayCommand(ExecuteDeleteEmployee, _ => true);

            ViewOnboardingCommand = new RelayCommand(ExecuteViewOnboarding, _ => true);
            DeleteOnboardingCommand = new RelayCommand(ExecuteDeleteOnboarding, _ => true);

            LoadFromDb();
        }

        private async void ExecuteViewOnboarding(object? obj)
        {
            if (obj != null && obj is UserModel)
            {
                UserModel? user = obj as UserModel;
                if (user != null)
                {
                    _popUpService.ShowPopUp<OnboardingRequest_View>(user.UserId);
                    await _adminOperationsService.RefreshUsers();
                    await _adminOperationsService.RecountPopulation();
                    EmployeeCount = _adminOperationsService.UserTotalCount;
                    EmployeeRequests = _adminOperationsService.Users
                        .Where(e => e.Role.NormalizedName == "no access".ToUpperInvariant())
                        .OrderByDescending(e => e.DateOfRegistry)
                        .ToList();
                    OnPropertyChanged(nameof(EmployeeRequests));
                    CurrentEmployees = _adminOperationsService.Users
                        .Where(u => u.Role.NormalizedName != "no access".ToUpperInvariant())
                        .ToList();
                    OnPropertyChanged(nameof(CurrentEmployees));
                }
            }
        }

        //METHODS
        private async void ExecuteDeleteOnboarding(object? obj)
        {
            if (_adminOperationsService.AdminUser == null)
                return;

            var password = _messageBox.ShowDialog("", MyMessageBoxType.Password, _adminOperationsService.AdminUser.Salt, _adminOperationsService.AdminUser.PasswordHash);

            if (password == null || password.DialogResult != true)
                return;

            if (!password.PasswordMatch)
            {
                _messageBox.ShowDialog("Incorrect password!", MyMessageBoxType.Error);
                return;
            }

            if (obj is UserModel)
            {
                UserModel? user = obj as UserModel;
                if (user != null)
                {
                    try
                    {
                        await _adminOperationsService.DeleteUser(user);
                    }
                    catch (Exception ex)
                    {
                        _messageBox.ShowDialog($"Error message: {ex.Message}");
                    }
                }
            }
            await _adminOperationsService.RefreshUsers();
            EmployeeRequests = _adminOperationsService.Users
                .Where(e => e.Role.NormalizedName == "no access".ToUpperInvariant())
                .OrderByDescending(e => e.DateOfRegistry)
                .ToList();
            OnPropertyChanged(nameof(EmployeeRequests));
            _messageBox.ShowDialog("Operation successful!", MyMessageBoxType.Success);
        }

        private async void ExecuteDeleteEmployee(object? obj)
        {
            var warning = _messageBox.ShowDialog("All information under this employee will also be deleted, including payslips and work logs. Please backup these information before proceeding.");

            if (warning == null || warning.MyMessageBoxDialogResult == MyMessageBoxDialogResult.Cancel)
                return;

            if (_adminOperationsService.AdminUser == null)
            {
                _messageBox.ShowDialog("Admin service not initialized! Please restart application.", MyMessageBoxType.Error);
                return;
            }

            var password = _messageBox.ShowDialog("", MyMessageBoxType.Password, _adminOperationsService.AdminUser.Salt, _adminOperationsService.AdminUser.PasswordHash);

            if (password == null || password.DialogResult != true)
                return;

            if (!password.PasswordMatch)
            {
                _messageBox.ShowDialog("Incorrect password!", MyMessageBoxType.Error);
                return;
            }

            if (obj is UserModel)
            {
                UserModel? user = obj as UserModel;
                if (user != null)
                {
                    try
                    {
                        await _adminOperationsService.DeleteUser(user);
                    }
                    catch (Exception ex)
                    {
                        _messageBox.ShowDialog($"Error message: {ex.Message}");
                    }
                }
            }
            await _adminOperationsService.RefreshUsers();
            await _adminOperationsService.RecountPopulation();
            EmployeeCount = _adminOperationsService.UserTotalCount;
            CurrentEmployees = _adminOperationsService.Users
                .Where(u => u.Role.NormalizedName != "no access".ToUpperInvariant())
                .ToList();
            OnPropertyChanged(nameof(CurrentEmployees));
            _messageBox.ShowDialog("Operation successful!", MyMessageBoxType.Success);
        }

        private void ExecuteViewEmployee(object? obj)
        {
            _popUpService.ShowPopUp<EmployeeDetails_View>();
        }
        private async void LoadFromDb()
        {
            await _adminOperationsService.RefreshUsers();
            await _adminOperationsService.RecountPopulation();
            await _adminOperationsService.RefreshRoles();
            await _adminOperationsService.RefreshDepartments();

            EmployeeCount = _adminOperationsService.UserTotalCount;
            Roles = _adminOperationsService.Roles;
            OnPropertyChanged(nameof(Roles));
            Departments = _adminOperationsService.Departments;
            OnPropertyChanged(nameof(Departments));
            CurrentEmployees = _adminOperationsService.Users
                .Where(e => e.Role.NormalizedName != "no access".ToUpperInvariant())
                .ToList();
            OnPropertyChanged(nameof(CurrentEmployees));
            EmployeeRequests = _adminOperationsService.Users
                .Where(e => e.Role.NormalizedName == "no access".ToUpperInvariant())
                .OrderByDescending(e => e.DateOfRegistry)
                .ToList();
            OnPropertyChanged(nameof(EmployeeRequests));
        }

        private void FilterEmployeeByRole()
        {
            if (SelectedRole == null)
                return;

            CurrentEmployees = _adminOperationsService.Users
                .Where(u => u.Role.RoleId == SelectedRole.RoleId)
                .ToList();
            OnPropertyChanged(nameof(CurrentEmployees));
        }

        private void FilterEmployeeByDepartment()
        {
            if (SelectedDepartment == null)
                return;

            CurrentEmployees = _adminOperationsService.Users
                .Where(u => u.Department.DepartmentId == SelectedDepartment.DepartmentId)
                .ToList();
            OnPropertyChanged(nameof(CurrentEmployees));
        }

        private void ExecuteResetFilters(object? obj)
        {
            SelectedRole = null;
            SelectedDepartment = null;
            EmployeeNameFilter = string.Empty;

            CurrentEmployees = _adminOperationsService.Users
                .Where(e => e.Role.NormalizedName != "no access".ToUpperInvariant())
                .ToList();
            OnPropertyChanged(nameof(CurrentEmployees));
        }

        private void FilterCurrentEmployeesByName()
        {
            if (string.IsNullOrWhiteSpace(EmployeeNameFilter))
                return;

            CurrentEmployees = _adminOperationsService.Users
                .Where(e => e.Role.NormalizedName != "no access".ToUpperInvariant() &&
                            e.AccountInfo != null &&
                            e.AccountInfo.FullName.Contains(EmployeeNameFilter, StringComparison.OrdinalIgnoreCase))
                .ToList();
            OnPropertyChanged(nameof(CurrentEmployees));
        }

        private void ExecuteAddEmployee(object? obj)
        {
            _popUpService.ShowPopUp<EmployeeAdd_View>();
        }

    }
}
