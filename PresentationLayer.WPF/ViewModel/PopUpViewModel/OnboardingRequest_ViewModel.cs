using System.Windows.Input;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAccountInfo;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using DomainLayer.Services;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class OnboardingRequest_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly MyMessageBox _myMessageBox;
        private UserModel? _currentUser;

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value.Trim();
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string _middleInitial = string.Empty;
        public string MiddleInitial
        {
            get => _middleInitial;
            set
            {
                _middleInitial = value.Trim();
                OnPropertyChanged(nameof(MiddleInitial));
            }
        }
        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value.Trim();
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                _username = value.Trim();
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                _email = value.Trim();
                OnPropertyChanged(nameof(Email));
            }
        }
        private IList<DepartmentModel> _departments = [];
        public IList<DepartmentModel> Departments
        {
            get => _departments;
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }
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
        private IList<RoleModel> _roles = [];
        public IList<RoleModel> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
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
        private string _jobTitle = string.Empty;


        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                _jobTitle = value.Trim();
                OnPropertyChanged(nameof(JobTitle));
            }
        }

        public ICommand Approve { get; set; }
        public ICommand Cancel { get; set; }

        public OnboardingRequest_ViewModel(IPopUpService popUpService, IAdminOperationsService adminOperationsService, MyMessageBox myMessageBox)
        {
            _myMessageBox = myMessageBox;
            _popUpService = popUpService;
            _adminOperationsService = adminOperationsService;

            Approve = new RelayCommand(ExecuteApproveCommand, _ => true);
            Cancel = new RelayCommand(ExecuteCancelCommand, _ => true);

            LoadDataFromDb();
        }

        private async Task LoadDataFromDb()
        {
            Departments = _adminOperationsService.Departments;
            Roles = _adminOperationsService.Roles;

            if (_popUpService.IdSource != null && _popUpService.IdSource != Guid.Empty)
            {
                _currentUser = _adminOperationsService.Users.FirstOrDefault(u => u.UserId == _popUpService.IdSource);
                if (_currentUser != null)
                {
                    Username = _currentUser.Username;
                    Email = _currentUser.Email;
                }
            }
        }

        private void ExecuteCancelCommand(object? obj)
        {
            _popUpService.ClosePopup();
        }

        private async void ExecuteApproveCommand(object? obj)
        {
            if (_currentUser == null)
            {
                _myMessageBox.ShowDialog("User request does not exist! Closing this window...", MyMessageBoxType.Error);
                _popUpService.ClosePopup();
                return;
            }

            if (string.IsNullOrEmpty(FirstName)
                || string.IsNullOrEmpty(MiddleInitial)
                || string.IsNullOrEmpty(LastName)
                || string.IsNullOrEmpty(Username)
                || string.IsNullOrEmpty(Email)
                || SelectedRole == null
                || SelectedDepartment == null
                || string.IsNullOrEmpty(JobTitle)
                )
            {
                _myMessageBox.ShowDialog("Please fill in all the fields!", MyMessageBoxType.Error);
                return;
            }

            if (_currentUser.AccountInfo == null)
            {
                _currentUser.AccountInfo = new AccountInfoModel()
                {
                    User = _currentUser,
                    UserId = _currentUser.UserId
                };
            }

            _currentUser.AccountInfo.FirstName = FirstName;
            _currentUser.AccountInfo.MiddleInitial = MiddleInitial;
            _currentUser.AccountInfo.LastName = LastName;
            _currentUser.AccountInfo.Role = JobTitle;
            _currentUser.AccountInfo.CompanyId = BusinessIdGenerator.GenerateUserId();

            _currentUser.DepartmentId = SelectedDepartment.DepartmentId;
            _currentUser.Department = SelectedDepartment;

            _currentUser.RoleId = SelectedRole.RoleId;
            _currentUser.Role = SelectedRole;

            try
            {
                await _adminOperationsService.UpdateUser(_currentUser);
                if (SelectedRole.NormalizedName == "contractor".ToUpperInvariant())
                {
                    var contractor = new ContractorModel()
                    {
                        User = _currentUser,
                        UserId = _currentUser.UserId
                    };
                    await _adminOperationsService.AddContractor(contractor);
                }
                else
                {
                    var employee = new EmployeeModel()
                    {
                        User = _currentUser,
                        UserId = _currentUser.UserId
                    };
                    await _adminOperationsService.AddEmployee(employee);
                }
                _myMessageBox.ShowDialog("Employee added successful! You can now edit their work information under Employees->View", MyMessageBoxType.Success);
            }
            catch (Exception ex)
            {
                _myMessageBox.ShowDialog($"Error message: {ex.Message}", MyMessageBoxType.Error);
            }
        }
    }
}
