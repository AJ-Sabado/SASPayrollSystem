using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DomainLayer.Enums;
using DomainLayer.Models.EmployeeAttendance;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.ViewModel.Tables;
using SASPayrolSystemProject;
using ServicesLayer;
using Syncfusion.XlsIO.Parser.Biff_Records;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardRegular
{
    public class RegDashboard_ViewModel : Base_ViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowService _windowService;
        private EmployeeAttendanceModel? _currentAttendance = null;
        private AttendanceState _attendanceState = AttendanceState.NoAttendance;

        public IList<AttendanceLog> AttendanceLogList { get; set; } = [];


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
                    _currentAttendance.TimeIn = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    _attendanceState = AttendanceState.TimedIn;
                    break;
                case AttendanceState.DoneBreak:
                    _currentAttendance.TimeOut = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    _currentAttendance.Status = AttendanceStatus.Present;
                    _attendanceState = AttendanceState.TimedOut;
                    break;
            }
            await _unitOfWork.Save();
            UpdateAttendanceState();
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

        }

        private async void BreakBtn(object? parameter)
        {
            switch(_attendanceState)
            {
                case AttendanceState.TimedIn:
                    _currentAttendance.BreakTimeIn = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    _attendanceState = AttendanceState.OnBreak;
                    break;
                case AttendanceState.OnBreak:
                    _currentAttendance.BreakTimeOut = new TimeOnly(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
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

        private async void LoadUserData()
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(x => x.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "User,EmployeeAccountInfo,EmployeeAttendances");
            if (employee != null)
            {
                //Load Side Bar Info
                if (employee.EmployeeAccountInfo != null)
                {
                    EmployeeFirstName = employee.EmployeeAccountInfo.FirstName;
                    EmployeeCompanyId = employee.EmployeeAccountInfo.CompanyId;
                    EmployeeRole = employee.EmployeeAccountInfo.Role;
                }

                //Load today's attendance
                var today = DateOnly.FromDateTime(DateTime.Now);
                _currentAttendance = employee.EmployeeAttendances.FirstOrDefault(x => x.Date == today);
                if (_currentAttendance != null)
                {
                    //If attendance record for today exists, set the current attendance
                    //MessageBox.Show("Reloaded attendance data for today!");

                    //checks for updating state
                    if (_currentAttendance.TimeIn != TimeOnly.MinValue)
                    {
                        _attendanceState = AttendanceState.TimedIn;
                        if (_currentAttendance.BreakTimeIn != TimeOnly.MinValue)
                        {
                            _attendanceState = AttendanceState.OnBreak;
                            if (_currentAttendance.BreakTimeOut != TimeOnly.MinValue)
                            {
                                _attendanceState = AttendanceState.DoneBreak;
                                if (_currentAttendance.TimeOut != TimeOnly.MinValue)
                                {
                                    _attendanceState = AttendanceState.TimedOut;
                                }
                            }
                        }
                    }

                }
                //New attendance is added
                else
                {
                    MessageBox.Show("No attendance data for today!");
                    _currentAttendance = new EmployeeAttendanceModel
                    {
                        EmployeeId = employee.EmployeeId,
                        Employee = employee,
                        Date = today,
                        TimeIn = new TimeOnly(0, 0, 0),
                        BreakTimeIn = new TimeOnly(0, 0, 0),
                        BreakTimeOut = new TimeOnly(0, 0, 0),
                        TimeOut = new TimeOnly(0, 0, 0),
                        Status = AttendanceStatus.Absent,
                        OTStatus = FormStatus.Pending
                    };
                    employee.EmployeeAttendances.Add(_currentAttendance);
                    await _unitOfWork.Save();
                }
                UpdateAttendanceState();

                //Load Attendance Log Table
                if (employee.EmployeeAttendances.Count > 0 && AttendanceLogList.Count == 0)
                {
                    foreach (var attendance in employee.EmployeeAttendances)
                    {
                        if (attendance.Date == DateOnly.FromDateTime(DateTime.Now) && attendance.TimeOut == TimeOnly.MinValue)
                        {
                            // Skip today's attendance record to avoid duplication
                            continue;
                        }
                        AttendanceLogList.Add(new AttendanceLog
                        {
                            Date = attendance.Date,
                            TimeIn = attendance.TimeIn,
                            TimeOut = attendance.TimeOut,
                            Status = attendance.Status.ToString(),
                            Overtime = attendance.OTStatus.ToString(),
                            OTDuration = $"{attendance.OTHours} hours"
                        });
                    }
                }
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
