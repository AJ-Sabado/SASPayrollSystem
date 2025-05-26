using System.Threading.Tasks;
using System.Windows;
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

        public IList<RoleModel> Roles { get; private set; } = [];
        private RoleModel? _selectedRole = null;
        public RoleModel? SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
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
            }
        }


        public IList<UserModel> CurrentEmployees { get; private set; } = [];
        public IList<UserModel> EmployeeRequests { get; private set; } = [];

        public ICommand AddEmployeeCommand { get; set; }

        //CONSTRUCTOR
        public AdminEmployee_ViewModel(IPopUpService popUpService, IAdminOperationsService adminOperationsService, MyMessageBox messageBox)
        {
            _popUpService = popUpService;
            _adminOperationsService = adminOperationsService;
            _messageBox = messageBox;

            AddEmployeeCommand = new RelayCommand(addEmployeeCommand);

            LoadFromDb();
        }
        //METHODS

        private async void LoadFromDb()
        {
            await _adminOperationsService.RefreshUsers();
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

        private void addEmployeeCommand(object? obj)
        {
            _popUpService.ShowPopUp<EmployeeAdd_View>();
        }

        private void FilterCurrentEmployeesByName()
        {
            if (string.IsNullOrWhiteSpace(EmployeeNameFilter))
            {
                CurrentEmployees = _adminOperationsService.Users
                    .Where(e => e.Role.NormalizedName != "no access".ToUpperInvariant())
                    .ToList();
            }
            else
            {
                CurrentEmployees = _adminOperationsService.Users
                    .Where(e => e.Role.NormalizedName != "no access".ToUpperInvariant() &&
                                e.AccountInfo.FullName.Contains(EmployeeNameFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            OnPropertyChanged(nameof(CurrentEmployees));
        }
    }
}
