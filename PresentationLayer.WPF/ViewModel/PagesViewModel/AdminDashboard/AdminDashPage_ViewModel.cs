using System.Windows.Media;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceLog;
using LiveCharts;
using LiveCharts.Wpf;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.AdminDashboard
{
    public class AdminDashPage_ViewModel : Base_ViewModel
    {
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly MyMessageBox _myMessageBox;
        private IDictionary<string, PayDateTotalPair> _summarizedPayrolls = new Dictionary<string, PayDateTotalPair>();

        //Header
        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string _companyId = string.Empty;
        public string CompanyId
        {
            get => _companyId;
            set
            {
                _companyId = value;
                OnPropertyChanged(nameof(CompanyId));
            }
        }
        private string _role = string.Empty;
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        //Last 5 Pay Periods Graph

        private string _previousAmount = "Php 0.00";
        public string PreviousAmount
        {
            get => _previousAmount;
            set
            {
                _previousAmount = value;
                OnPropertyChanged(nameof(PreviousAmount));
            }
        }
        private string _previousDate = "-";
        public string PreviousDate
        {
            get => _previousDate;
            set
            {
                _previousDate = value;
                OnPropertyChanged(nameof(PreviousDate));
            }
        }
        private string _currentAmount = "Php 0.00";
        public string CurrentAmount
        {
            get => _currentAmount;
            set
            {
                _currentAmount = value;
                OnPropertyChanged(nameof(CurrentAmount));
            }
        }
        private string _currentDate = "-";
        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection PayrollSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public IList<EmployeeAttendanceLogModel> EmployeeAttendanceLogs { get; private set; } = [];
        public IList<ContractorAttendanceLogModel> ContractorAttendanceLogModels { get; private set; } = [];

        public AdminDashPage_ViewModel(IAdminOperationsService adminOperationService, MyMessageBox myMessageBox)
        {
            _adminOperationsService = adminOperationService;
            _myMessageBox = myMessageBox;

            LoadFromDb();
        }

        private void LoadFromDb()
        {
            //Header
            if (_adminOperationsService.AdminUser != null && _adminOperationsService.AdminUser.AccountInfo != null)
            {
                FirstName = _adminOperationsService.AdminUser.AccountInfo.FirstName;
                CompanyId = _adminOperationsService.AdminUser.AccountInfo.CompanyId;
                Role = _adminOperationsService.AdminUser.AccountInfo.Role;
                _summarizedPayrolls = _adminOperationsService.SummarizedPayrolls;
                EmployeeAttendanceLogs = _adminOperationsService.EmployeeAttendanceLogs
                    .Skip(_adminOperationsService.EmployeeAttendanceLogs.Count - 5)
                    .OrderByDescending(l => l.TimeStamp)
                    .OrderByDescending(l => l.Date)
                    .ToList();
                ContractorAttendanceLogModels = _adminOperationsService.ContractorAttendanceLogs
                    .Skip(_adminOperationsService.ContractorAttendanceLogs.Count - 5)
                    .OrderByDescending(l => l.TimeIn)
                    .ToList();
            }
            else
            {
                _myMessageBox.ShowDialog("Access denied!", MyMessageBoxType.Error);
                throw new ArgumentException("Null access on IAdminOperationsSerice.AdminUser");
            }

            SetPreviousUpcomingPayrolls();

            //Charts
            LoadPieChartData();
            LoadLineChartData();
        }

        private void SetPreviousUpcomingPayrolls()
        {
            if (_summarizedPayrolls.Count == 0)
                return;
            if (_summarizedPayrolls.Count == 1)
            {
                CurrentAmount = $"Php {_summarizedPayrolls.First().Value.Total:F2}";
                CurrentDate = _summarizedPayrolls.First().Value.PayDate;
            }
            else if (_summarizedPayrolls.Count == 2)
            {
                PreviousAmount = $"Php {_summarizedPayrolls.First().Value.Total:F2}";
                PreviousDate = _summarizedPayrolls.First().Value.PayDate;
                var second = _summarizedPayrolls.Skip(1).First().Value;
                CurrentAmount = $"Php {second.Total:F2}";
                CurrentDate = second.PayDate;
            }
            else
            {
                var lastTwo = _summarizedPayrolls.Skip(_summarizedPayrolls.Count - 2);
                PreviousAmount = $"Php {lastTwo.First().Value.Total:F2}";
                PreviousDate = lastTwo.First().Value.PayDate;
                var last = lastTwo.Skip(1).First().Value;
                CurrentAmount = $"Php {last.Total:F2}";
                CurrentDate = last.PayDate;
            }
        }

        private void LoadPieChartData()
        {
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Independent\nContractor",
                    Values = new ChartValues<int> { _adminOperationsService.ContractorCount },
                    Fill = Brushes.SteelBlue,
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y}"
                },
                new PieSeries
                {
                    Title = "Regular",
                    Values = new ChartValues<int> { _adminOperationsService.EmployeeCount },
                    Fill = Brushes.DodgerBlue,
                    DataLabels = true,
                    LabelPoint = chartPoint => $"{chartPoint.Y}"
                }
            };
        }

        private void LoadLineChartData()
        {
            // Example payroll data for the last 5 bi-weekly periods
            var payrollAmounts = new ChartValues<decimal> { 0, 0, 0, 0, 0 };
            var payrollDates = new[]
            {
                "-",
                "-",
                "-",
                "-",
                "-"
            };

            //Fill in amounts
            if (_summarizedPayrolls.Count <= 5 && _summarizedPayrolls.Count > 0)
            {
                int start = 5 - _summarizedPayrolls.Count;
                int skips = 0;
                for (int i = start; i < 5; ++i)
                {
                    var val = _summarizedPayrolls.Skip(skips).First().Value;
                    payrollAmounts[i] = val.Total;
                    payrollDates[i] = val.PayDate;
                    skips++;
                }
            }
            else if (_summarizedPayrolls.Count > 6)
            {
                var last5 = _summarizedPayrolls.Skip(_summarizedPayrolls.Count - 5).ToArray();
                for (int i = 0; i < 5; ++i)
                {
                    payrollAmounts[i] = last5[i].Value.Total;
                    payrollDates[i] = last5[i].Value.PayDate;
                }
            }


            PayrollSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Payroll",
                    Values = payrollAmounts,
                    PointGeometry = DefaultGeometries.Circle,
                    Stroke = Brushes.SteelBlue,
                    Fill = Brushes.LightSteelBlue,
                    StrokeThickness = 2,
                    LineSmoothness = 0.3
                }
            };

            Labels = payrollDates;
            Formatter = value => $"₱{value:N0}";
        }
    }


}

