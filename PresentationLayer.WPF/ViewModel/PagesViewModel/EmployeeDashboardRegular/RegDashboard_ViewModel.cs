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

        public IList<AttendanceLog> AttendanceLogList {get; set;} = [];

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

        public RegDashboard_ViewModel(IUnitOfWork unitOfWork, IWindowService windowService)
        {
            _unitOfWork = unitOfWork;
            _windowService = windowService;
            Logout = new RelayCommand(LogoutExecute);
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

                if (employee.EmployeeAttendances != null && employee.EmployeeAttendances.Count > 0)
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
                        Status = FormStatus.Approved.ToString(),
                        Overtime = FormStatus.Denied.ToString(),
                        OTDuration = "0 hours"
                    });
                }
            }
        }
    }
}
