using DomainLayer.Models.Department;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminWorkforce_ViewModel : Base_ViewModel
    {
        //Employee Evaluated Attendance Tab
        public IList<EmployeeEvaluatedAttendanceModel> EvaluatedAttendances { get; private set; } = [];
        public IList<DepartmentModel> Departments { get; private set; } = [];
        public IList<EmployeeAttendanceRequestModel> EmployeeAttendanceRequests { get; private set; } = [];
        public IList<EmployeeAttendanceLogModel> EmployeeAttendanceLogs { get; private set; } = [];

        private DepartmentModel? _selectedDepartment = null;
        public DepartmentModel? SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                if (_selectedDepartment != value)
                {
                    _selectedDepartment = value;
                    OnPropertyChanged(nameof(SelectedDepartment));
                    FilterEvaluatedAttendances();
                }
            }
        }
        private DateTime? _selectedEvaluatedAttendanceDate = null;
        public DateTime? SelectedEvaluatedAttendanceDate
        {
            get => _selectedEvaluatedAttendanceDate;
            set
            {
                _selectedEvaluatedAttendanceDate = value;
                OnPropertyChanged(nameof(SelectedEvaluatedAttendanceDate));
                FilterEvaluatedAttendances();
            }
        }

        //Employee Attendance Logs Tab
        private readonly IAdminOperationsService _adminOperationService;
        private readonly MyMessageBox _messageBox;

        private DateTime? _selectedEmployeeAttendanceLogDate = null;
        public DateTime? SelectedEmployeeAttendanceLogDate
        {
            get => _selectedEmployeeAttendanceLogDate;
            set
            {
                // Clamp the date to today if it's in the future
                if (value > DateTime.Today)
                {
                    _selectedEmployeeAttendanceLogDate = DateTime.Today;
                }
                else
                {
                    _selectedEmployeeAttendanceLogDate = value;
                }
                OnPropertyChanged(nameof(SelectedEmployeeAttendanceLogDate));
                FilterEmployeeAttendanceLogs();
            }
        }

        public AdminWorkforce_ViewModel(IAdminOperationsService adminOperationsService, MyMessageBox messageBox)
        {
            _adminOperationService = adminOperationsService;
            _messageBox = messageBox;

            LoadDataFromDb();
        }

        private async void LoadDataFromDb()
        {
            await LoadDepartments();
            await LoadEmployeeAttendanceRequests();
            await FilterEvaluatedAttendances();
            await FilterEmployeeAttendanceLogs();
            //await LoadEmployeeAttendanceLogs();
        }

        private async Task FilterEmployeeAttendanceLogs()
        {
            await _adminOperationService.RefreshEmployeeAttendanceLogs(SelectedEmployeeAttendanceLogDate);
            await LoadEmployeeAttendanceLogs();
        }

        private Task LoadEmployeeAttendanceLogs()
        {
            EmployeeAttendanceLogs = _adminOperationService.EmployeeAttendanceLogs;
            OnPropertyChanged(nameof(EmployeeAttendanceLogs));
            return Task.CompletedTask;
        }

        private Task LoadEmployeeAttendanceRequests()
        {
            EmployeeAttendanceRequests = _adminOperationService.EmployeeAttendanceRequests;
            OnPropertyChanged(nameof(EmployeeAttendanceRequests));
            return Task.CompletedTask;
        }

        private async Task FilterEvaluatedAttendances()
        {
            await _adminOperationService.RefreshEvaluatedAttendances(SelectedDepartment, SelectedEvaluatedAttendanceDate);
            await LoadEvaluatedAttendances();
        }

        private Task LoadEvaluatedAttendances()
        {
            EvaluatedAttendances = _adminOperationService.EvaluatedAttendances;
            OnPropertyChanged(nameof(EvaluatedAttendances));
            return Task.CompletedTask;
        }

        private Task LoadDepartments()
        {
            Departments = _adminOperationService.Departments;
            OnPropertyChanged(nameof(Departments));
            return Task.CompletedTask;
        }
    }
}
