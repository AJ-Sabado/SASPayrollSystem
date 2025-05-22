using System.Runtime.CompilerServices;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.IdentityModel.Tokens;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardIC
{
    public class ICDashboard_ViewModel : Base_ViewModel
    {
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
            }
        }
        private decimal _hoursRendered = 20;
        public decimal HoursRendered
        {
            get => _hoursRendered;
            set
            {
                _hoursRendered = value;
                OnPropertyChanged();
            }
        }

        public string Target
        {
            get => $"{_targetHours} hours per week";
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

        public ICDashboard_ViewModel()
        {
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
        }
    }
}
