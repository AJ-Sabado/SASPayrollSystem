using System.Windows;
using System.Windows.Input;
using DomainLayer.Enums;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
using DomainLayer.Models.EmployeeLeave;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Windows;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class RegJobDesk_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IUnitOfWork _unitOfWork;
        
        //Commands
        public ICommand FileLeaveCommand { get; }
        public ICommand AttendanceRequestCommand { get; }
        public ICommand EditAttendanceRequest { get; }
        public ICommand DeleteAttendanceRequest { get; }
        public ICommand EditLeaveRequest { get; }
        public ICommand DeleteLeaveRequest { get; }

        //Tables
        private IList<EmployeeEvaluatedAttendanceModel> _evaluatedAttendanceList = [];
        public IList<EmployeeEvaluatedAttendanceModel> EvaluatedAttendanceList
        {
            get => _evaluatedAttendanceList;
            private set
            {
                _evaluatedAttendanceList = value;
                OnPropertyChanged();
            }
        }
        private IList<EmployeeAttendanceRequestModel> _attendanceRequestList = [];
        public IList<EmployeeAttendanceRequestModel> AttendanceRequestList
        {
            get => _attendanceRequestList;
            private set
            {
                _attendanceRequestList = value;
                OnPropertyChanged();
            }
        }
        private IList<EmployeeLeaveModel> _leaveRequestList = [];
        public IList<EmployeeLeaveModel> LeaveRequestList
        {
            get => _leaveRequestList;
            private set
            {
                _leaveRequestList = value;
                OnPropertyChanged();
            }
        }

        //Information
        private string _fullName = "Full Name";
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
        private string _employmentStatus = "Employment Status";
        public string EmploymentStatus
        {
            get => _employmentStatus;
            set
            {
                _employmentStatus = value;
                OnPropertyChanged();
            }
        }
        private string _workShift = "Work Shift";
        public string WorkShift
        {
            get => _workShift;
            set
            {
                _workShift = value;
                OnPropertyChanged();
            }
        }
        private string _dailyRate = "Daily Rate";
        public string DailyRate
        {
            get => _dailyRate;
            set
            {
                _dailyRate = value;
                OnPropertyChanged();
            }
        }
        //Contacts
        private string _email = "Email";
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private string _phone = "Phone";
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
        private string _website = "Website";
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
        private string _basicPay = "Php 0.00";
        public string BasicPay
        {
            get => _basicPay;
            set
            {
                _basicPay = value;
                OnPropertyChanged();
            }
        }
        private string _holidayPay = "Php 0.00";
        public string HolidayPay
        {
            get => _holidayPay;
            set
            {
                _holidayPay = value;
                OnPropertyChanged();
            }
        }
        private string _nightDifferentialPay = "Php 0.00";
        public string NightDifferentialPay
        {
            get => _nightDifferentialPay;
            set
            {
                _nightDifferentialPay = value;
                OnPropertyChanged();
            }
        }
        private string _overtimePay = "Php 0.00";
        public string OvertimePay
        {
            get => _overtimePay;
            set
            {
                _overtimePay = value;
                OnPropertyChanged();
            }
        }
        private string _paidLeaves = "Php 0.00";
        public string PaidLeaves
        {
            get => _paidLeaves;
            set
            {
                _paidLeaves = value;
                OnPropertyChanged();
            }
        }
        private string _bonus = "Php 0.00";
        public string Bonus
        {
            get => _bonus;
            set
            {
                _bonus = value;
                OnPropertyChanged();
            }
        }
        private string _allowances = "Php 0.00";
        public string Allowances
        {
            get => _allowances;
            set
            {
                _allowances = value;
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
        private string _withholdingTax = "Php 0.00";
        public string WithholdingTax
        {
            get => _withholdingTax;
            set
            {
                _withholdingTax = value;
                OnPropertyChanged();
            }
        }
        private string _governmentContributions = "Php 0.00";
        public string GovernmentContributions
        {
            get => _governmentContributions;
            set
            {
                _governmentContributions = value;
                OnPropertyChanged();
            }
        }
        private string _loanDeductions = "Php 0.00";
        public string LoanDeductions
        {
            get => _loanDeductions;
            set
            {
                _loanDeductions = value;
                OnPropertyChanged();
            }
        }
        private string _uTDeductions = "Php 0.00";
        public string UTDeductions
        {
            get => _uTDeductions;
            set
            {
                _uTDeductions = value;
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
        private string _payrollStatus = "No payslip information available.";
        public string PayrollStatus
        {
            get => _payrollStatus;
            set
            {
                _payrollStatus = value;
                OnPropertyChanged();
            }
        }

        public RegJobDesk_ViewModel(IPopUpService popUpService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _popUpService = popUpService;
            FileLeaveCommand = new RelayCommand(FileLeave);
            AttendanceRequestCommand = new RelayCommand(AttendanceRequest);
            EditAttendanceRequest = new RelayCommand(ExecuteEditAttendanceRequest);
            DeleteAttendanceRequest = new RelayCommand(ExecuteDeleteAttendanceRequest);
            EditLeaveRequest = new RelayCommand(ExecuteEditLeaveRequest);
            DeleteLeaveRequest = new RelayCommand(ExecuteDeleteLeaveRequest);
            LoadUserData();
        }

        private async void ExecuteDeleteLeaveRequest(object? item)
        {
            var leaveRequest = item as EmployeeLeaveModel;
            if (leaveRequest != null)
            {
                // Check if the leave request is pending
                if (leaveRequest.Status == FormStatus.Pending)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this leave request?", "Delete Leave Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        var employee = await _unitOfWork.EmployeeRepository.GetAsync(e => e.UserId == Properties.Settings.Default.CurrentUserGuid,
                            includeProperties: "EmployeeLeaveRequests");
                        if (employee != null && employee.EmployeeLeaveRequests != null)
                        {
                            employee.EmployeeLeaveRequests.Remove(leaveRequest);
                            await _unitOfWork.Save();
                            MessageBox.Show("Leave request deleted successfuly!");
                            LoadUserData();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You cannot delete this leave request because it is already approved or rejected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        //Methods
        private void ExecuteEditLeaveRequest(object? item)
        {
            var leaveRequest = item as EmployeeLeaveModel;
            if (leaveRequest != null)
            {
                // Check if the leave request is pending
                if (leaveRequest.Status == FormStatus.Pending)
                {
                    _popUpService.ShowPopUp<FileLeaveForm_View>(leaveRequest.EmployeeLeaveId);
                }
                else
                {
                    MessageBox.Show("You cannot edit this leave request because it is already approved or rejected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private async void LoadUserData()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "AccountInfo,Employee,Department");
            if (user != null)
            {
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(e => e.UserId == user.UserId,
                    includeProperties: "EmployeeEvaluatedAttendances,EmployeeLeaveRequests,EmployeePayslips,EmployeeAttendanceRequests");
                if (employee != null)
                {
                    // Load Side Bar Data
                    if (user.AccountInfo != null)
                    {
                        FullName = user.AccountInfo.FullName;
                        Role = user.AccountInfo.Role;
                        Department = user.Department.Name;
                        DailyRate = $"Php {employee.BasicDailyRate.ToString("N2")} per day";
                        WorkShift = $"{employee.DefaultWorkShiftStart} - {employee.DefaultWorkShiftEnd}";
                        Email = user.Email != null ? user.Email : string.Empty;
                        Phone = user.AccountInfo.PrimaryPhoneNumber;
                        Website = user.AccountInfo.WebsiteUrl;
                        if (user.AccountInfo.EmploymentType == EmploymentType.Regular)
                            EmploymentStatus = "Regular Employee";
                        else
                            EmploymentStatus = "Independent Contractor";
                    }

                    //Load Attendance Logs
                    if (employee.EmployeeEvaluatedAttendances != null && employee.EmployeeEvaluatedAttendances.Count > 0)
                    {
                        EvaluatedAttendanceList = employee.EmployeeEvaluatedAttendances.ToList();
                    }

                    //Load Attendance Requests
                    if (employee.EmployeeAttendanceRequests != null)
                    {
                        AttendanceRequestList = employee.EmployeeAttendanceRequests.ToList();
                    }

                    //Load Leaves
                    if (employee.EmployeeLeaveRequests != null)
                    {
                        LeaveRequestList = employee.EmployeeLeaveRequests.ToList();
                    }

                    //Load Payslips
                    if (employee.EmployeePayslips != null)
                    {
                        var payslip = employee.EmployeePayslips.LastOrDefault();
                        if (payslip != null)
                        {
                            BasicPay = $"Php {payslip.BasicPay.ToString("N2")}";
                            HolidayPay = $"Php {payslip.HolidayHours.ToString("N2")}";
                            NightDifferentialPay = $"Php {payslip.NightDifferentialPay.ToString("N2")}";
                            OvertimePay = $"Php {payslip.OvertimePay.ToString("N2")}";
                            PaidLeaves = $"Php {payslip.PaidLeaves.ToString("N2")}";
                            Bonus = $"Php {payslip.Bonus.ToString("N2")}";
                            Allowances = $"Php {payslip.Allowances.ToString("N2")}";
                            GrossPay = $"Php {payslip.GrossPay.ToString("N2")}";
                            WithholdingTax = $"Php {payslip.WithholdingTax.ToString("N2")}";
                            GovernmentContributions = $"Php {payslip.GovernmentContributions.ToString("N2")}";
                            LoanDeductions = $"Php {payslip.LoanDeductions.ToString("N2")}";
                            UTDeductions = $"Php {payslip.UTDeductions.ToString("N2")}";
                            NetSalary = $"Php {payslip.NetSalary.ToString("N2")}";
                            PayrollDate = $"{payslip.PeriodStart:MMMM dd, yyyy} - {payslip.PeriodEnd:MMMM dd, yyyy}";
                            PayrollStatus = $"{payslip.PayslipStatus.ToString()} - {payslip.PayDate:MMMM dd, yyyy}";
                        }
                    }
                    else
                    {
                        PayrollStatus = "No payslip information available.";
                    }
                }
            }
        }

        private async void ExecuteDeleteAttendanceRequest(object? item)
        {
            var attendanceRequest = item as EmployeeAttendanceRequestModel;
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(e => e.UserId == Properties.Settings.Default.CurrentUserGuid,
                includeProperties: "EmployeeAttendanceRequests");
            if (employee != null && employee.EmployeeAttendanceRequests != null && attendanceRequest != null)
            {
                // Check if the attendance request is pending
                if (attendanceRequest.Status == FormStatus.Pending)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this attendance request?", "Delete Attendance Request", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        employee.EmployeeAttendanceRequests.Remove(attendanceRequest);
                        await _unitOfWork.Save();
                        MessageBox.Show("Attendance request deleted successfuly!");
                        LoadUserData();
                    }
                }
                else
                {
                    MessageBox.Show("You cannot delete this attendance request because it is already approved or rejected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Operation failed!");
            }
        }

        private void ExecuteEditAttendanceRequest(object? item)
        {
            var attendanceRequest = item as EmployeeAttendanceRequestModel;
            if (attendanceRequest != null)
            {
                // Check if the attendance request is pending
                if (attendanceRequest.Status == FormStatus.Pending)
                {
                    _popUpService.ShowPopUp<AttendanceRequest_View>(attendanceRequest.Id);
                }
                else
                {
                    MessageBox.Show("You cannot edit this attendance request because it is already approved or rejected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            OnPropertyChanged(nameof(AttendanceRequestList));
        }

        private void FileLeave(object? obj)
        {
            _popUpService.ShowPopUp<FileLeaveForm_View>();
        }

        // ATTENDANCE REQUEST FUNCTIONS  
        private void AttendanceRequest(object? obj)
        {
            _popUpService.ShowPopUp<AttendanceRequest_View>();
        }
    }
}
