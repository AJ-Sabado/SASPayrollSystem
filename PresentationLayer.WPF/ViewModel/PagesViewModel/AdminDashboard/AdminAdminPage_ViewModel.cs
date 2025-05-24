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
            await LoadEmployeeCount();
            await LoadDepartmentsTable();
            await LoadRolesTable();
            await LoadHolidaysTable();
        }
        private async Task LoadEmployeeCount()
        {
            _employeeCount = await _adminOperationsService.GetEmployeeCount();
            OnPropertyChanged(nameof(EmployeeCount));
        }
        private async Task LoadDepartmentsTable()
        {
            DepartmentsTable = await _adminOperationsService.GetDepartmentsList();
            OnPropertyChanged(nameof(DepartmentsTable));
            OnPropertyChanged(nameof(DepartmentsCount));
        }

        private async Task LoadRolesTable()
        {
            RolesTable = await _adminOperationsService.GetRolesList();
            OnPropertyChanged(nameof(RolesTable));
            OnPropertyChanged(nameof(RolesCount));
        }


        private async Task LoadHolidaysTable()
        {
            HolidaysTable = await _adminOperationsService.GetHolidayList();
            OnPropertyChanged(nameof(HolidaysTable));
        }
    }
}
