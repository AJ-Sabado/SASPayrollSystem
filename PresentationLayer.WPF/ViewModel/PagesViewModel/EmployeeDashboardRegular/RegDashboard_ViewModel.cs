using System.Windows;
using System.Windows.Input;
using DomainLayer.Enums.EmployeeAttendanceLog;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendanceLog;
using PresentationLayer.WPF.Services;
using SASPayrolSystemProject;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardRegular
{
    public class RegDashboard_ViewModel : Base_ViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowService _windowService;
        private EmployeeModel? _employee;
        private AttendanceState _attendanceState = AttendanceState.NoAttendance;

        private IList<EmployeeAttendanceLogModel> _attendanceLogList = [];
        public IList<EmployeeAttendanceLogModel> AttendanceLogList 
        { 
            get => _attendanceLogList; 
            private set
            {
                _attendanceLogList = value;
                OnPropertyChanged();
            }
        }


        //Information Panel Binded Data
        private string _employeeFirstName = "First Name";
        public string EmployeeFirstName
        {
            get => _employeeFirstName;
            private set
            {
                _employeeFirstName = value;
                OnPropertyChanged();
            }
        }

        private string _employeeCompanyId = "Company ID";
        public string EmployeeCompanyId
        {
            get => _employeeCompanyId;
            private set
            {
                _employeeCompanyId = value;
                OnPropertyChanged();
            }
        }

        private string _employeeRole = "Role";
        public string EmployeeRole
        {
            get => _employeeRole;
            private set
            {
                _employeeRole = value;
                OnPropertyChanged();
            }
        }
        private string _upcoming = "January 1, 2000";
        public string Upcoming
        {
            get => _upcoming;
            private set
            {
                _upcoming = value;
                OnPropertyChanged();
            }
        }
        private string _leaves = "0";
        public string Leaves
        {
            get => _leaves;
            private set
            {
                _leaves = value;
                OnPropertyChanged();
            }
        }
        private string _absences = "0";
        public string Absences
        {
            get => _absences;
            private set
            {
                _absences = value;
                OnPropertyChanged();
            }
        }

        //Commands
        public ICommand Logout { get; set; }
        public ICommand ToggleTimeCommand { get; }
        public ICommand BreakCommand { get; }

        //Attendance System
        private bool _isTimedIn;
        public bool IsTimedIn
        {
            get => _isTimedIn;
            set
            {
                if (_isTimedIn != value)
                {
                    _isTimedIn = value;
                    OnPropertyChanged();
                    //UpdateTimeInState();
                }
            }
        }

        //Attendance Button Activation
        private bool _isTimeEnabled = true;
        public bool IsTimeEnabled
        {
            get => _isTimeEnabled;
            set
            {
                if (_isTimeEnabled != value)
                {
                    _isTimeEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isBreakEnabled;
        public bool IsBreakEnabled
        {
            get => _isBreakEnabled;
            set
            {
                if (_isBreakEnabled != value)
                {
                    _isBreakEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BreakButtonText => _attendanceState == AttendanceState.OnBreak ? "   Resume" : "   Break";

        //Constructor
        public RegDashboard_ViewModel(IUnitOfWork unitOfWork, IWindowService windowService)
        {
            _unitOfWork = unitOfWork;
            _windowService = windowService;
            Logout = new RelayCommand(LogoutExecute);
            ToggleTimeCommand = new RelayCommand(ToggleTimeIn);
            BreakCommand = new RelayCommand(BreakBtn);
            LoadUserData();
        }

        //Methods
        private async void ToggleTimeIn(object? parameter)
        {
            switch (_attendanceState)
            {
                case AttendanceState.NoAttendance:
                    //_currentAttendance.TimeStamp = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    await AddAttendanceLog(AttendanceLogEventType.TimeIn);
                    _attendanceState = AttendanceState.TimedIn;
                    break;
                case AttendanceState.DoneBreak:
                    //_currentAttendance.TimeOut = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    //_currentAttendance.Status = AttendanceStatus.Present;
                    await AddAttendanceLog(AttendanceLogEventType.TimeOut);
                    _attendanceState = AttendanceState.TimedOut;
                    break;
            }
            await _unitOfWork.Save();
            UpdateAttendanceState();
        }

        private async Task AddAttendanceLog(AttendanceLogEventType eventType)
        {
            if (_employee != null)
            {
                var attendanceLog = new EmployeeAttendanceLogModel()
                {
                    EmployeeId = _employee.EmployeeId,
                    Employee = _employee,
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    TimeStamp = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                    EventType = eventType
                };
                _employee.EmployeeAttendanceLogs.Add(attendanceLog);
            }
        }

        private void UpdateAttendanceState()
        {
            switch (_attendanceState)
            {
                case AttendanceState.NoAttendance:
                    IsTimedIn = false;
                    IsTimeEnabled = true;
                    IsBreakEnabled = false;
                    break;
                case AttendanceState.TimedIn:
                    IsTimedIn = false;
                    IsTimeEnabled = false;
                    IsBreakEnabled = true;
                    break;
                case AttendanceState.OnBreak:
                    IsTimedIn = false;
                    IsTimeEnabled = false;
                    IsBreakEnabled = true;
                    OnPropertyChanged(nameof(BreakButtonText));
                    break;
                case AttendanceState.DoneBreak:
                    IsTimedIn = true;
                    IsTimeEnabled = true;
                    IsBreakEnabled = false;
                    OnPropertyChanged(nameof(BreakButtonText));
                    break;
                case AttendanceState.TimedOut:
                    IsTimedIn = true;
                    IsTimeEnabled = false;
                    IsBreakEnabled = false;
                    break;
                    //Handle invalid attendance
            }
            LoadAttendanceLog();
        }

        private async void BreakBtn(object? parameter)
        {
            switch (_attendanceState)
            {
                case AttendanceState.TimedIn:
                    await AddAttendanceLog(AttendanceLogEventType.BreakStart);
                    _attendanceState = AttendanceState.OnBreak;
                    break;
                case AttendanceState.OnBreak:
                    await AddAttendanceLog(AttendanceLogEventType.BreakEnd);
                    _attendanceState = AttendanceState.DoneBreak;
                    break;
            }
            await _unitOfWork.Save();
            UpdateAttendanceState();
        }

        // Helper for messages
        private void ShowMessage(string message, string title, MessageBoxImage icon)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, icon);
        }

        private void LogoutExecute(object? parameter)
        {
            Properties.Settings.Default.CurrentUserGuid = Guid.Empty;
            Properties.Settings.Default.Save();
            _windowService.ShowWindow<MainWindow>();
        }

        private void LoadAttendanceLog()
        {
            if (_employee != null)
            {
                AttendanceLogList = _employee.EmployeeAttendanceLogs.ToList();
            }
        }

        private async void LoadUserData()
        {
            
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(x => x.UserId == Properties.Settings.Default.CurrentUserGuid,
                includeProperties: "EmployeeAccountInfo,EmployeeAttendanceLogs,EmployeePayslips");
            if (employee != null)
            {
                _employee = employee;
                //Load Side Bar Info
                if (employee.EmployeeAccountInfo != null)
                {
                    EmployeeFirstName = employee.EmployeeAccountInfo.FirstName;
                    EmployeeCompanyId = employee.EmployeeAccountInfo.CompanyId;
                    EmployeeRole = employee.EmployeeAccountInfo.Role;
                    Leaves = employee.LeaveCredits.ToString();
                    Absences = employee.Absences.ToString();
                }

                //Load Payslip info
                if (employee.EmployeePayslips != null && employee.EmployeePayslips.Count > 0)
                {
                    var recentPayslip = employee.EmployeePayslips.Last();
                    Upcoming = recentPayslip.PeriodEnd.ToString("MMMM dd, yyyy");
                }
                else
                {
                    Upcoming = "-";
                }

                    //Load today's attendance
                    var today = DateOnly.FromDateTime(DateTime.Now);
                if (employee.EmployeeAttendanceLogs != null && employee.EmployeeAttendanceLogs.Count > 0)
                {
                    var attendances = employee.EmployeeAttendanceLogs.Where(x => x.Date == today).ToList();
                    if (attendances.Count > 0 && attendances.FirstOrDefault(a => a.EventType == AttendanceLogEventType.TimeIn) != null)
                    {
                        _attendanceState = AttendanceState.TimedIn;
                        if (attendances.FirstOrDefault(a => a.EventType == AttendanceLogEventType.BreakStart) != null)
                        {
                            _attendanceState = AttendanceState.OnBreak;
                            if (attendances.FirstOrDefault(a => a.EventType == AttendanceLogEventType.BreakEnd) != null)
                            {
                                _attendanceState = AttendanceState.DoneBreak;
                                if (attendances.FirstOrDefault(a => a.EventType == AttendanceLogEventType.TimeOut) != null)
                                {
                                    _attendanceState = AttendanceState.TimedOut;
                                }
                            }
                        }
                    }

                }
                UpdateAttendanceState();
            }
        }
    }

    enum AttendanceState
    {
        NoAttendance,
        TimedIn,
        OnBreak,
        DoneBreak,
        TimedOut,
        InvalidAttendance
    }
}
