using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DomainLayer.Models.ContractorAttendanceLog;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.IdentityModel.Tokens;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardIC
{
    public class ICDashboard_ViewModel : Base_ViewModel
    {
        private TimeInOut _timeInOutState = TimeInOut.TimeIn;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContractorTrackerService _contractorTrackerService;

        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection 
        { 
            get => _seriesCollection; 
            set
            {
                _seriesCollection = value;
                OnPropertyChanged(nameof(SeriesCollection));
            }
        }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        //Header
        private string _employeeFirstName = "First Name";
        public string EmployeeFirstName
        {
            get => _employeeFirstName;
            set
            {
                _employeeFirstName = value;
                OnPropertyChanged();
            }
        }

        private string _employeeCompanyId = "#000000";
        public string EmployeeCompanyId
        {
            get => _employeeCompanyId;
            set
            {
                _employeeCompanyId = value;
                OnPropertyChanged();
            }
        }
        private string _employeeRole = "Role";
        public string EmployeeRole
        {
            get => _employeeRole;
            set
            {
                _employeeRole = value;
                OnPropertyChanged();
            }
        }

        //Side Panel
        private string _upcoming = DateOnly.FromDateTime(DateTime.Now).ToString("MMMM dd, yyyy");
        public string Upcoming
        {
            get => _upcoming;
            set
            {
                _upcoming = value;
                OnPropertyChanged();
            }
        }

        private decimal _targetHours = 40;
        public decimal TargetHours
        {
            get => _targetHours;
            set
            {
                _targetHours = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Target));
                OnPropertyChanged(nameof(HoursRemaining));
            }
        }
        private decimal _hoursRendered = 0;
        public decimal HoursRendered
        {
            get => _hoursRendered;
            set
            {
                _hoursRendered = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HoursRemaining));
            }
        }

        public string Target
        {
            get => $"{_targetHours:G29} hours";
        }

        public string HoursRemaining
        {
            get => $"{(_targetHours - _hoursRendered):F2} hours remaining";
        }

        private string _hourlyRate = "Php 0.00 per hour";
        public string HourlyRate
        {
            get => _hourlyRate;
            set
            {
                _hourlyRate = value;
                OnPropertyChanged();
            }
        }

        //Buttons
        private bool _isTimeInEnabled = true;
        public bool IsTimeInEnabled
        {
            get => _isTimeInEnabled;
            set
            {
                _isTimeInEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _isTimeOutEnabled = false;
        public bool IsTimeOutEnabled
        {
            get => _isTimeOutEnabled;
            set
            {
                _isTimeOutEnabled = value;
                OnPropertyChanged();
            }
        }

        //Attendance Log List
        public IList<ContractorAttendanceLogModel> AttendanceLogList
        {
            get => _contractorTrackerService.CurrentWeekAttendanceLogs;
        }

        //Commands
        public ICommand TimeIn { get; set; }
        public ICommand TimeOut { get; set; }
        public ICommand Logout { get; set; }

        //Not live yet :(
        private decimal[] _weeklyHoursChartValues = { 0, 0, 0, 0 ,0 ,0 ,0};


        //Constructor
        public ICDashboard_ViewModel(IUnitOfWork unitOfWork, IContractorTrackerService contractorTrackerService)
        {
            _unitOfWork = unitOfWork;
            _contractorTrackerService = contractorTrackerService;

            CalculateChartValues();

            //Table
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Daily\nHours",
                    Values = new ChartValues<decimal> 
                    { 
                        _weeklyHoursChartValues[0], 
                        _weeklyHoursChartValues[1],
                        _weeklyHoursChartValues[2],
                        _weeklyHoursChartValues[3], 
                        _weeklyHoursChartValues[4],
                        _weeklyHoursChartValues[5],
                        _weeklyHoursChartValues[6] },
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 5,
                    Stroke = Brushes.DodgerBlue,
                    Fill = Brushes.Transparent
                }
            };

            DateTime today = DateTime.Today;

            int daysSinceSunday = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-daysSinceSunday);

            Labels = Enumerable.Range(0, 7)
                .Select(i =>
                {
                    DateTime date = sunday.AddDays(i);
                    return date.ToString("MMM dd"); // e.g. "May 18"
                })
                .ToArray();


            YFormatter = value => $"{value:0.##}";

            TimeIn = new RelayCommand(ExecuteTimeIn, _ => true);
            TimeOut = new RelayCommand(ExecuteTimeOut, _ => true);
            Logout = new RelayCommand(ExecuteLogout, _ => true);


            LoadOnceInfo();
            RealTimeInfo();
        }

        private void CalculateChartValues()
        {
            if (_contractorTrackerService.CurrentContractor != null)
            {
                foreach (var attendanceLog in _contractorTrackerService.CurrentContractor.ContractorAttendanceLogs)
                {
                    _weeklyHoursChartValues[(int)attendanceLog.Date.DayOfWeek] += attendanceLog.Duration;
                }
            }
        }

        //Methods
        private async void LoadOnceInfo()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid
            , includeProperties: "AccountInfo");
            if (user != null && user.AccountInfo != null)
            {
                EmployeeFirstName = user.AccountInfo.FirstName;
                EmployeeCompanyId = user.AccountInfo.CompanyId.ToString();
                EmployeeRole = user.AccountInfo.Role.ToString();
                if (_contractorTrackerService.CurrentContractor != null)
                {
                    TargetHours = _contractorTrackerService.CurrentContractor.MaximumWeeklyHours;
                    HourlyRate = $"Php {_contractorTrackerService.CurrentContractor.BasicHourlyRate:F2}";
                }
                if (_contractorTrackerService.CurrentAttendanceLog != null && _contractorTrackerService.CurrentAttendanceLog.TimeIn.HasValue)
                {
                    UpdateTimeInOutState();
                }
            }
        }

        private async void RealTimeInfo()
        {
            if (_contractorTrackerService.CurrentContractor != null)
            {
                await Task.Run(() =>
                {
                    while (true)
                    {
                        HoursRendered = Math.Floor(_contractorTrackerService.TotalWeeklyHoursRendered * 100) / 100;
                        Task.Delay(1000).Wait();
                    }
                });
            }
        }

        private void UpdateTimeInOutState()
        {
            if (_timeInOutState == TimeInOut.TimeIn)
            {
                _timeInOutState = TimeInOut.TimeOut;
                IsTimeInEnabled = false;
                IsTimeOutEnabled = true;
            }
            else
            {
                _timeInOutState = TimeInOut.TimeIn;
                IsTimeInEnabled = true;
                IsTimeOutEnabled = false;
            }
        }

        private void ExecuteLogout(object? obj)
        {
            
        }

        private async void ExecuteTimeOut(object? obj)
        {
            var log = await _contractorTrackerService.EndSession();
            if (log != null)
            {
                OnPropertyChanged(nameof(AttendanceLogList));
                UpdateTimeInOutState();
            }
            else
                MessageBox.Show("Log not saved properly! Please contact administrator.");
        }

        private async void ExecuteTimeIn(object? obj)
        {
            var log = await _contractorTrackerService.StartSession();
            if (log != null)
                UpdateTimeInOutState();
            else
                MessageBox.Show("Session was not started properly! Please contact administrator.");
        }

        //Tests
        //private async void TestProgressBar()
        //{
        //    await Task.Run(() => 
        //    {
        //        for (decimal i = 0; i <= TargetHours; i += 1m)
        //        {
        //            HoursRendered = i;
        //            Task.Delay(1000).Wait();
        //        }
        //    });
        //}

    }
    enum TimeInOut
    {
        TimeIn,
        TimeOut
    }
}