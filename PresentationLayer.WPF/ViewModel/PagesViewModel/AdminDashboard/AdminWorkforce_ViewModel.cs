using System.Threading.Tasks;
using System.Windows.Input;
using DomainLayer.Enums;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.Department;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
using DomainLayer.Models.EmployeeLeave;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Windows.PopUps;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminWorkforce_ViewModel : Base_ViewModel
    {
        private readonly IAdminOperationsService _adminOperationService;
        private readonly IPopUpService _popUpService;
        private readonly MyMessageBox _messageBox;

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
        private string _employeeEvaluatedAttendanceNameFilter = string.Empty;
        public string EmployeeEvaluatedAttendanceNameFilter
        {
            get => _employeeEvaluatedAttendanceNameFilter;
            set
            {
                _employeeEvaluatedAttendanceNameFilter = value;
                OnPropertyChanged(nameof(EmployeeEvaluatedAttendanceNameFilter));
            }
        }

        //Employee Attendance Logs Tab


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

        //Employee Leaves Tab
        private string _leaveNameFilter = string.Empty;
        public string LeaveNameFilter
        {
            get => _leaveNameFilter;
            set
            {
                _leaveNameFilter = value;
                OnPropertyChanged(nameof(LeaveNameFilter));
            }
        }
        private uint _employeeCount = 0;
        public uint EmployeeCount
        {
            get => _employeeCount;
            set
            {
                _employeeCount = value;
                OnPropertyChanged(nameof(EmployeeCount));
            }
        }
        private uint _employeesOnLeaveCount = 0;

        public uint EmployeesOnLeaveCount
        {
            get => _employeesOnLeaveCount;
            set
            {
                _employeesOnLeaveCount = value;
                OnPropertyChanged(nameof(EmployeesOnLeaveCount));
            }
        }
        private uint _employeeLeaveRequestsCount = 0;
        public uint EmployeeLeaveRequestsCount
        {
            get => _employeeLeaveRequestsCount;
            set
            {
                _employeeLeaveRequestsCount = value;
                OnPropertyChanged(nameof(EmployeeLeaveRequestsCount));
            }
        }
        public IList<EmployeeLeaveModel> EmployeesOnLeave { get; private set; } = [];

        //Leave Requests
        public IList<EmployeeLeaveModel> EmployeeLeaveRequests { get; private set; } = [];

        //Independent Contractors Attendance Logs
        private DateTime? _selectedDateICAttendanceLog = null;
        public DateTime? SelectedDateICAttendanceLog
        {
            get => _selectedDateICAttendanceLog;
            set
            {
                _selectedDateICAttendanceLog = value;
                OnPropertyChanged(nameof(SelectedDateICAttendanceLog));
                FilterICAttendanceByDate();
            }
        }

        private async Task FilterICAttendanceByDate()
        {
            await _adminOperationService.RefreshContractorAttendanceLogs();
            await LoadContractorAttendanceLogs();
        }

        private Task LoadContractorAttendanceLogs()
        {
            if (SelectedDateICAttendanceLog.HasValue)
            {
                ContractorAttendanceLogs = _adminOperationService.ContractorAttendanceLogs
                    .Where(log => log.Date == DateOnly.FromDateTime(SelectedDateICAttendanceLog.Value))
                    .ToList();
            }
            else
            {
                ContractorAttendanceLogs = _adminOperationService.ContractorAttendanceLogs;
            }
            //ContractorAttendanceLogs = _adminOperationService.ContractorAttendanceLogs;
            OnPropertyChanged(nameof(ContractorAttendanceLogs));
            return Task.CompletedTask;
        }

        public IList<ContractorAttendanceLogModel> ContractorAttendanceLogs { get; private set; } = [];

        //Commands
        public ICommand FilterEvaluatedAttendanceByName { get; private set; }
        public ICommand FilterLeaveByName { get; private set; }
        public ICommand AssignLeaveCommand { get; set; }
        public ICommand ViewAttendanceRequest { get; set; }
        public ICommand ViewLeaveRequest { get; set; }


        public AdminWorkforce_ViewModel(IAdminOperationsService adminOperationsService, MyMessageBox messageBox, IPopUpService popUpService)
        {
            _adminOperationService = adminOperationsService;
            _messageBox = messageBox;
            _popUpService = popUpService;

            FilterEvaluatedAttendanceByName = new RelayCommand(ExecuteEvaluateAttendanceFilterByName, _ => true);
            FilterLeaveByName = new RelayCommand(ExecuteLeaveFilterByName, _ => true);
            AssignLeaveCommand = new RelayCommand(ExecuteAssignLeave);
            ViewAttendanceRequest = new RelayCommand(ExecuteViewAttendanceRequest, _ => true);
            ViewLeaveRequest = new RelayCommand(ExecuteViewLeaveRequest, _ => true);

            LoadDataFromDb();
        }

        //METHODS
        private async void ExecuteViewLeaveRequest(object? obj)
        {
            if (obj != null && obj is EmployeeLeaveModel leaveRequest)
            {
                try
                {
                    _popUpService.ShowPopUp<LeaveRequests_View>(leaveRequest.EmployeeLeaveId);
                }
                catch (Exception ex)
                {
                    _messageBox.ShowDialog($"Error viewing leave request: {ex.Message}", MyMessageBoxType.Error);
                }
            }
            await _adminOperationService.RefreshEmployeeLeaves();
            await LoadEmployeeOnLeave();
            await LoadEmployeeLeaveRequests();
        }

        private async void ExecuteViewAttendanceRequest(object? obj)
        {
            if (obj != null && obj is EmployeeAttendanceRequestModel attendanceRequest)
            {
                if (attendanceRequest.Status != FormStatus.Pending)
                {
                    _messageBox.ShowDialog("This attendance request has already been processed.", MyMessageBoxType.Warning);
                    return;
                }

                try
                {
                    _popUpService.ShowPopUp<AttendanceRequestAction_View>(attendanceRequest.Id);
                }
                catch (Exception ex)
                {
                    _messageBox.ShowDialog($"Error viewing attendance request: {ex.Message}", MyMessageBoxType.Error);
                }
            }
            else
            {
                _messageBox.ShowDialog("Invalid attendance request data.", MyMessageBoxType.Error);
            }
            await _adminOperationService.RefreshEmployeeAttendanceRequests();
            await LoadEmployeeAttendanceRequests();
        }

        private void ExecuteAssignLeave(object? obj)
        {
            _popUpService.ShowPopUp<AssignLeave_View>();
        }

        private async void ExecuteLeaveFilterByName(object? obj)
        {
            if (!string.IsNullOrEmpty(LeaveNameFilter))
            {
                await _adminOperationService.RefreshEmployeeLeaves();
                await LoadEmployeeOnLeave();
                await LoadEmployeeLeaveRequests();
            }
            else
            {
                await _adminOperationService.RefreshEmployeeLeaves();
                await LoadEmployeeOnLeave();
                await LoadEmployeeLeaveRequests();
            }
        }

        private Task LoadEmployeeLeaveRequests()
        {
            EmployeeLeaveRequests = _adminOperationService.EmployeeLeaves.Where(l => l.Status == FormStatus.Pending).ToList();
            EmployeeLeaveRequestsCount = (uint)EmployeeLeaveRequests.Count;
            OnPropertyChanged(nameof(EmployeeLeaveRequests));
            OnPropertyChanged(nameof(EmployeeLeaveRequestsCount));
            return Task.CompletedTask;
        }

        private Task LoadEmployeeOnLeave()
        {
            EmployeesOnLeave = _adminOperationService.EmployeeLeaves.Where(l => l.Status != FormStatus.Pending).OrderByDescending(l => l.DateOfAbsenceStart).ToList();
            var today = DateOnly.FromDateTime(DateTime.Now);
            //Gets count of employees currently on leave
            var onLeaveNow = _adminOperationService.EmployeeLeaves
                .Where(l => l.DateOfAbsenceStart >= today && l.DateOfReturn < today).ToList();
            EmployeesOnLeaveCount = (uint)(onLeaveNow.Count);
            OnPropertyChanged(nameof(EmployeesOnLeave));
            OnPropertyChanged(nameof(EmployeesOnLeaveCount));
            return Task.CompletedTask;
        }

        private async void ExecuteEvaluateAttendanceFilterByName(object? obj)
        {
            if (!string.IsNullOrEmpty(EmployeeEvaluatedAttendanceNameFilter))
            {
                await _adminOperationService.RefreshEmployeeEvaluatedAttendances();
                await LoadEvaluatedAttendances();
            }
            else
            {
                await _adminOperationService.RefreshEmployeeEvaluatedAttendances();
                await LoadEvaluatedAttendances();
            }
        }

        private async void LoadDataFromDb()
        {
            await LoadDepartments();
            await LoadEmployeeAttendanceRequests();
            await LoadEmployeeOnLeave();
            await LoadEmployeeLeaveRequests();
            await FilterICAttendanceByDate();
            await FilterEvaluatedAttendances();
            await FilterEmployeeAttendanceLogs();
            EmployeeCount = (uint)(_adminOperationService.Employees.Count + _adminOperationService.Contractors.Count);
            OnPropertyChanged(nameof(EmployeeCount));
        }

        private async Task FilterEmployeeAttendanceLogs()
        {
            await _adminOperationService.RefreshEmployeeAttendanceLogs();
            await LoadEmployeeAttendanceLogs();
        }

        private Task LoadEmployeeAttendanceLogs()
        {
            if (SelectedEmployeeAttendanceLogDate.HasValue)
            {
                EmployeeAttendanceLogs = _adminOperationService.EmployeeAttendanceLogs
                    .Where(log => log.Date == DateOnly.FromDateTime(SelectedEmployeeAttendanceLogDate.Value))
                    .ToList();
            }
            else
            {
                EmployeeAttendanceLogs = _adminOperationService.EmployeeAttendanceLogs;
            }
            //EmployeeAttendanceLogs = _adminOperationService.EmployeeAttendanceLogs;
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
            await _adminOperationService.RefreshEmployeeEvaluatedAttendances();
            await LoadEvaluatedAttendances();
        }

        private Task LoadEvaluatedAttendances()
        {
            EvaluatedAttendances = _adminOperationService.EmployeeEvaluatedAttendances;
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
