using System.Windows.Input;
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

        public ICommand FileLeaveCommand { get; }
        public ICommand AttendanceRequestCommand { get; }

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
        public RegJobDesk_ViewModel(IPopUpService popUpService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _popUpService = popUpService;
            LoadUserData();
            FileLeaveCommand = new RelayCommand(FileLeave);
            AttendanceRequestCommand = new RelayCommand(AttendanceRequest);
        }

        private async void LoadUserData()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "Employee,Department");
            if (user != null)
            {
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(e => e.UserId == user.UserId,
                    includeProperties: "EmployeeAccountInfo,EmployeeEvaluatedAttendances,EmployeeLeaveRequests,EmployeePayslips,EmployeeAttendanceRequests");
                if (employee != null)
                {
                    // Load Side Bar Data
                    if (employee.EmployeeAccountInfo != null)
                    {
                        FullName = employee.EmployeeAccountInfo.FullName;
                        Role = employee.EmployeeAccountInfo.Role;
                        Department = user.Department.Name;
                        DailyRate = $"Php {employee.BasicDailyRate.ToString("N2")} per day";
                        WorkShift = $"{employee.DefaultWorkShiftStart} - {employee.DefaultWorkShiftEnd}";
                        Email = user.Email;
                        Phone = employee.EmployeeAccountInfo.PrimaryPhoneNumber;
                        Website = employee.EmployeeAccountInfo.WebsiteUrl;
                        if (employee.EmployeeAccountInfo.EmploymentType == EmploymentType.Regular)
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
                    if (employee.EmployeeAttendanceRequests != null && employee.EmployeeAttendanceRequests.Count > 0)
                    {
                        AttendanceRequestList = employee.EmployeeAttendanceRequests.ToList();
                    }

                    //Load Leaves
                    if (employee.EmployeeLeaveRequests != null && employee.EmployeeLeaveRequests.Count > 0)
                    {
                        LeaveRequestList = employee.EmployeeLeaveRequests.ToList();
                    }
                }
            }
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
