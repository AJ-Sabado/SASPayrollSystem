using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DomainLayer.Models.Department;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using Microsoft.Identity.Client;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Windows.PopUps;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminAdminPage_ViewModel : Base_ViewModel
    {
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly IPopUpService _popUpService;
        private readonly MyMessageBox _messageBox;

        //Header
        private int _employeeCount = 0;
        public string EmployeeCount
        {
            get => $"{_employeeCount}";
        }


        public string DepartmentsCount
        {
            get => $"{DepartmentsTable.Count}";
        }

        public string RolesCount
        {
            get => $"{RolesTable.Count}";
        }

        //Tables
        public IList<DepartmentModel> DepartmentsTable { get; private set; } = [];
        public IList<RoleModel> RolesTable { get; private set; } = [];
        public IList<HolidayModel> HolidaysTable { get; private set; } = [];

        public ICommand AddDepartment { get; set; }
        public ICommand DeleteDepartment { get; set; }
        public ICommand AddHoliday { get; set; }
        public ICommand DeleteHoliday { get; set; }

        public AdminAdminPage_ViewModel(IAdminOperationsService adminOperationsService, IPopUpService popUpService, MyMessageBox messageBox)
        {
            _adminOperationsService = adminOperationsService;
            _popUpService = popUpService;
            _messageBox = messageBox;

            AddDepartment = new RelayCommand(ExecuteAddDepartment, _ => true);
            DeleteDepartment = new RelayCommand(ExecuteDeleteDepartment, _ => true);
            AddHoliday = new RelayCommand(ExecuteAddHoliday, _ => true);
            DeleteHoliday = new RelayCommand(ExecuteDeleteHoliday, _ => true);

            LoadData();
        }

        private async void ExecuteDeleteHoliday(object? obj)
        {
            if (_adminOperationsService.AdminUser == null)
                return;

            var result = _messageBox.ShowDialog("", MyMessageBoxType.Password, _adminOperationsService.AdminUser.Salt, _adminOperationsService.AdminUser.PasswordHash);

            if (result == null || result.DialogResult == null || result.DialogResult == false)
                return;
            if (!result.PasswordMatch)
            {
                _messageBox.ShowDialog("Incorrect password!", MyMessageBoxType.Error);
                return;
            }

            if (obj != null && obj is HolidayModel)
            {
                HolidayModel? holiday = obj as HolidayModel;
                try
                {
                    if (holiday != null)
                        await _adminOperationsService.DeleteHoliday(holiday);
                }
                catch (Exception ex)
                {
                    _messageBox.ShowDialog($"Error message: {ex.Message}", MyMessageBoxType.Error);
                }
            }

            _messageBox.ShowDialog("Delete successful!", MyMessageBoxType.Success);

            await _adminOperationsService.RefreshHolidays();
            await LoadData();
        }

        private async void ExecuteAddHoliday(object? obj)
        {
            _popUpService.ShowPopUp<AddHolidays_View>();
            await _adminOperationsService.RefreshHolidays();
            await LoadData();
        }

        private async void ExecuteDeleteDepartment(object? obj)
        {
            if (_adminOperationsService.AdminUser == null)
                return;

            var warning = _messageBox.ShowDialog("This will also delete all employees under this department. Make sure to migrate all employees under this department first.");

            if (warning == null || warning.MyMessageBoxDialogResult != MyMessageBoxDialogResult.Yes)
                return;

            var result = _messageBox.ShowDialog("", MyMessageBoxType.Password, _adminOperationsService.AdminUser.Salt, _adminOperationsService.AdminUser.PasswordHash);

            if (result == null || result.DialogResult == null || result.DialogResult == false)
                return;

            if (!result.PasswordMatch)
            {
                _messageBox.ShowDialog("Incorrect password!", MyMessageBoxType.Error);
                return;
            }

            if (obj != null && obj is DepartmentModel)
            {
                DepartmentModel? department = obj as DepartmentModel;
                try
                {
                    if (department != null)
                        await _adminOperationsService.DeleteDepartment(department);
                }
                catch(Exception ex)
                {
                    _messageBox.ShowDialog($"Error message: {ex.Message}", MyMessageBoxType.Error);
                }
            }
            _messageBox.ShowDialog("Delete successful!", MyMessageBoxType.Success);

            await _adminOperationsService.RefreshDepartments();
            await LoadData();
        }

        private async void ExecuteAddDepartment(object? obj)
        {
            _popUpService.ShowPopUp<AddDepartment_View>();
            await _adminOperationsService.RefreshDepartments();
            await LoadData();
        }

        private async Task LoadData()
        {
            await LoadHolidayTable();
            await LoadDepartmentTable();
            await LoadRolesTable();
            await LoadEmployeeCount();
        }

        private Task LoadEmployeeCount()
        {
            if (_adminOperationsService.AdminUser != null)
            {
                _employeeCount = _adminOperationsService.Employees.Count + _adminOperationsService.Contractors.Count;
                OnPropertyChanged(nameof(EmployeeCount));
            }
            return Task.CompletedTask;
        }

        private Task LoadHolidayTable()
        {
            if (_adminOperationsService.AdminUser != null)
            {
                HolidaysTable = _adminOperationsService.Holidays;
                OnPropertyChanged(nameof(HolidaysTable));
            }
            return Task.CompletedTask;
        }

        private Task LoadDepartmentTable()
        {
            if (_adminOperationsService.AdminUser != null)
            {
                DepartmentsTable = _adminOperationsService.Departments;
                OnPropertyChanged(nameof(DepartmentsTable));
                OnPropertyChanged(nameof(DepartmentsCount));
            }
            return Task.CompletedTask;
        }

        private Task LoadRolesTable()
        {
            if (_adminOperationsService.AdminUser != null)
            {
                RolesTable = _adminOperationsService.Roles;
                OnPropertyChanged(nameof(RolesTable));
                OnPropertyChanged(nameof(RolesCount));
            }
            return Task.CompletedTask;
        }
    }
}
