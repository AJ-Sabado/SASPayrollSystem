using System.Threading.Tasks;
using System.Windows;
using DomainLayer.Models.Department;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using Microsoft.Identity.Client;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminAdminPage_ViewModel : Base_ViewModel
    {
        private readonly IAdminOperationsService _adminOperationsService;

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

        public AdminAdminPage_ViewModel(IAdminOperationsService adminOperationsService)
        {
            _adminOperationsService = adminOperationsService;
            LoadData();
        }

        private async void LoadData()
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
