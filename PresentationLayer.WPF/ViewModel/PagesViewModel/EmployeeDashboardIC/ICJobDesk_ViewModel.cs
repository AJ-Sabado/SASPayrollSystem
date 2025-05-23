using System.Windows.Controls;
using DomainLayer.Models.ContractorAttendanceLog;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PagesViewModel.EmployeeDashboardIC
{
    public class ICJobDesk_ViewModel : Base_ViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContractorTrackerService _contractorTrackerService;

        public ICJobDesk_ViewModel(IUnitOfWork unitOfWork, IContractorTrackerService contractorTrackerService)
        {
            _unitOfWork = unitOfWork;
            _contractorTrackerService = contractorTrackerService;

            LoadUserData();
        }

        private async void LoadUserData()
        {
            await LoadSidePanel();
            await LoadPayslipData();
            await LoadAttendanceData();
        }

        private Task LoadAttendanceData()
        {
            if (_contractorTrackerService.CurrentContractor != null)
            {
                AttendanceLogList = _contractorTrackerService.CurrentContractor.ContractorAttendanceLogs.ToList();
                OnPropertyChanged(nameof(AttendanceLogList));
                //Summary to be added
            }
            return Task.CompletedTask;
        }

        private Task LoadPayslipData()
        {
            if (_contractorTrackerService.CurrentContractor != null)
            {
                var currentPayslip = _contractorTrackerService.CurrentContractor.ContractorPayslips.LastOrDefault();
                if (currentPayslip != null)
                {
                    TotalHoursRendered = $"{currentPayslip.TotalHoursRendered:F2} hours";
                    GrossPay = $"Php {currentPayslip.NetPay:F2}";
                    NetSalary = $"Php {currentPayslip.NetPay:F2}";
                    PayrollDate = $"{currentPayslip.PeriodStart} - {currentPayslip.PeriodEnd}";
                    PayrollStatus = $"{currentPayslip.PayslipStatus.ToString()} - {currentPayslip.PayDate}";
                }
            }
            return Task.CompletedTask;
        }

        private async Task LoadSidePanel()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid
                , includeProperties: "AccountInfo,Department");
            if (user != null && _contractorTrackerService.CurrentContractor != null && user.AccountInfo != null)
            {
                FullName = user.AccountInfo.FullName;
                Role = user.AccountInfo.Role;
                Department = user.Department.Name;
                EmploymentStatus = user.AccountInfo.EmploymentType.ToString();
                WeeklyTarget = $"{_contractorTrackerService.CurrentContractor.MaximumWeeklyHours:G29} hours per week";
                HourlyRate = $"Php {_contractorTrackerService.CurrentContractor.BasicHourlyRate:F2} per hour";
                Email = user.Email != null ? user.Email : Email;
                Phone = user.AccountInfo.PrimaryPhoneNumber;
                Website = user.AccountInfo.WebsiteUrl;
            }
        }

        //Binding properties

        //Side panel
        private string _fullName = "Full T. Name";
        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }

        private string _role = "Role";
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged();
            }
        }

        private string _department = "Department";
        public string Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged();
            }
        }

        private string _employmentStatus = "Status";
        public string EmploymentStatus
        {
            get => _employmentStatus;
            set
            {
                _employmentStatus = value;
                OnPropertyChanged();
            }
        }

        private string _weeklyTarget = "0 hours per week";
        public string WeeklyTarget
        {
            get => _weeklyTarget;
            set
            {
                _weeklyTarget = value;
                OnPropertyChanged();
            }
        }

        private string _hourlyRate = "Php 0.00 per week";
        public string HourlyRate
        {
            get => _hourlyRate;
            set
            {
                _hourlyRate = value;
                OnPropertyChanged();
            }
        }

        private string _email = "Email@email.com";
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _phone = "+630000000000";
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        private string _website = "https://site.com";
        public string Website
        {
            get => _website;
            set
            {
                _website = value;
                OnPropertyChanged();
            }
        }

        //Payslip
        private string _totalHoursRendered = "0 hours";
        public string TotalHoursRendered
        {
            get => _totalHoursRendered;
            set
            {
                _totalHoursRendered = value;
                OnPropertyChanged();
            }
        }

        private string _grossPay = "Php 0.00";
        public string GrossPay
        {
            get => _grossPay;
            set
            {
                _grossPay = value;
                OnPropertyChanged();
            }
        }

        private string _netSalary = "Php 0.00";
        public string NetSalary
        {
            get => _netSalary;
            set
            {
                _netSalary = value;
                OnPropertyChanged();
            }
        }

        private string _payrollDate = "-";
        public string PayrollDate
        {
            get => _payrollDate;
            set
            {
                _payrollDate = value;
                OnPropertyChanged();
            }
        }

        private string _payrollStatus = "No payslip information found.";
        public string PayrollStatus
        {
            get => _payrollStatus;
            set
            {
                _payrollStatus = value;
                OnPropertyChanged();
            }
        }

        //Attendance Tab
        public IList<ContractorAttendanceLogModel> AttendanceLogList { get; private set; } = [];

        private string _summaryTotalHoursTaken = "0 hours";
        public string SummaryTotalHoursTaken
        {
            get => _summaryTotalHoursTaken;
            set
            {
                _summaryTotalHoursTaken = value;
                OnPropertyChanged();
            }
        }

        private string _summaryTotalHoursRemaining = "0 hours";
        public string SummaryTotalHoursRemaining
        {
            get => _summaryTotalHoursRemaining;
            set
            {
                _summaryTotalHoursRemaining = value;
                OnPropertyChanged();
            }
        }

        private string _summaryAverageDailyHours = "0 hours";
        public string SummaryAverageDailyHours
        {
            get => _summaryAverageDailyHours;
            set
            {
                _summaryAverageDailyHours = value;
                OnPropertyChanged();
            }
        }

        private string _summaryLongestSession = "0 hours";
        public string SummaryLongestSession
        {
            get => _summaryLongestSession;
            set
            {
                _summaryLongestSession = value;
                OnPropertyChanged();
            }
        }

        //Commands

        //Methods

    }
}
