using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using DomainLayer.Enums;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.ViewModel.Tables;
using SASPayrolSystemProject;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardRegular
{
    public class RegDashboard_ViewModel : Base_ViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWindowService _windowService;

        public IList<AttendanceLog> AttendanceLogList { get; set; } = [];

        //Binded properties
        public ICommand Logout { get; set; }

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

        //==============================TIME IN==================

        public ICommand ToggleTimeCommand { get; }
        public ICommand BreakCommand { get; }

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
                    UpdateTimeInState();
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

        private bool _isOnBreak;
        public bool IsOnBreak
        {
            get => _isOnBreak;
            set
            {
                if (_isOnBreak != value)
                {
                    _isOnBreak = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BreakButtonText));
                }
            }
        }

        private bool _hasTakenBreak;
        public bool HasTakenBreak
        {
            get => _hasTakenBreak;
            set
            {
                if (_hasTakenBreak != value)
                {
                    _hasTakenBreak = value;
                    OnPropertyChanged();
                }
            }
        }

        private void ToggleTimeIn(object? parameter)
        {
            IsTimedIn = !IsTimedIn;
        }

        // Handles logic when IsTimedIn is set
        private void UpdateTimeInState()
        {
            if (IsTimedIn)
            {
                HasTakenBreak = false;
                IsBreakEnabled = true;
                IsOnBreak = false;
                ShowMessage("Timed In", "Timed In", MessageBoxImage.Information);
            }
            else
            {
                IsBreakEnabled = false;
                ShowMessage("Timed Out", "Timed Out", MessageBoxImage.Information);
            }
        }

        // Break button logic
        private void BreakBtn(object? parameter)
        {
            if (!HasTakenBreak)
            {
                IsOnBreak = true;
                ShowMessage("1-hour break started!", "Break", MessageBoxImage.Information);
            }
            else
            {
                IsOnBreak = false;
                IsBreakEnabled = false;
                ShowMessage("1-hour break ended!", "Resume", MessageBoxImage.Warning);
            }
            HasTakenBreak = !HasTakenBreak;
        }

        // Helper for messages
        private void ShowMessage(string message, string title, MessageBoxImage icon)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, icon);
        }

        public string BreakButtonText => IsOnBreak ? "   Resume" : "   Break";

        //===================CONSTRUCTOR===============================================

        public RegDashboard_ViewModel(IUnitOfWork unitOfWork, IWindowService windowService)
        {
            _unitOfWork = unitOfWork;
            _windowService = windowService;
            Logout = new RelayCommand(LogoutExecute);
            ToggleTimeCommand = new RelayCommand(ToggleTimeIn);
            BreakCommand = new RelayCommand(BreakBtn);
            LoadUserData();
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
                if (employee.EmployeeAccountInfo != null)
                {
                    EmployeeFirstName = employee.EmployeeAccountInfo.FirstName;
                    EmployeeCompanyId = employee.EmployeeAccountInfo.CompanyId;
                    EmployeeRole = employee.EmployeeAccountInfo.Role;
                }

                if (employee.EmployeeAttendances != null && employee.EmployeeAttendances.Count > 0 && AttendanceLogList.Count == 0)
                {
                    foreach (var attendance in employee.EmployeeAttendances)
                    {
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
                else
                {
                    //For testing purposes
                    AttendanceLogList.Add(new AttendanceLog
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        TimeIn = new TimeOnly(8, 0, 0),
                        TimeOut = new TimeOnly(17, 0, 0),
                        Status = AttendanceStatus.Present.ToString(),
                        Overtime = FormStatus.Denied.ToString(),
                        OTDuration = "0 hours"
                    });
                }
            }
        }

        
    }
}
