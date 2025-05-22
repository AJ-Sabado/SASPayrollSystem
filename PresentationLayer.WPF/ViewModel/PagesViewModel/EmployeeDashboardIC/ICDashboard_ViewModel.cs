using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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

        public SeriesCollection SeriesCollection { get; set; }
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
            get => $"{(_targetHours - _hoursRendered):G29} hours remaining";
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

        //Commands
        public ICommand TimeIn { get; set; }
        public ICommand TimeOut { get; set; }
        public ICommand Logout { get; set; }

        //Constructor
        public ICDashboard_ViewModel(IUnitOfWork unitOfWork, IContractorTrackerService contractorTrackerService)
        {
            _unitOfWork = unitOfWork;
            _contractorTrackerService = contractorTrackerService;

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Daily\nHours",
                    Values = new ChartValues<double> { 4, 6, 5, 2, 4, 1, 0.5 },
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


            LoadUserAccountInfoData();
            ReloadContractorInfo();
            //TestProgressBar();
        }

        //Methods
        private async void LoadUserAccountInfoData()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid
            , includeProperties: "AccountInfo");
            if (user != null && user.AccountInfo != null)
            {
                EmployeeFirstName = user.AccountInfo.FirstName;
                EmployeeCompanyId = user.AccountInfo.CompanyId.ToString();
                EmployeeRole = user.AccountInfo.Role.ToString();
            }
        }

        private void ReloadContractorInfo()
        {
            var contractor = _contractorTrackerService.CurrentContractor;
            if (contractor != null)
            {
                TargetHours = contractor.MaximumWeeklyHours;
                HoursRendered = _contractorTrackerService.TotalWeeklyHoursRendered;
                HourlyRate = $"Php {contractor.BasicHourlyRate:F2}";
            }
            else
                MessageBox.Show("Not loaded");
        }

        //private async void UpdateGraphsLiveAsync()
        //{

        //}

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
            MessageBox.Show("Not implemented yet!");
        }

        private void ExecuteTimeOut(object? obj)
        {
            UpdateTimeInOutState();
        }

        private void ExecuteTimeIn(object? obj)
        {
            UpdateTimeInOutState();
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