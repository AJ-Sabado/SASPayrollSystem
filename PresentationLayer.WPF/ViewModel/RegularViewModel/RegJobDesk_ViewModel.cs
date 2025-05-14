using System.Windows.Input;
using DomainLayer.Enums.EmployeePersonalInfo;
using PresentationLayer.WPF.Services;
using PresentationLayer.WPF.View.Windows;
using PresentationLayer.WPF.ViewModel.Tables;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.RegularViewModel
{
    public class RegJobDesk_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IUnitOfWork _unitOfWork;

        public ICommand FileLeaveCommand { get; }
        public ICommand AttendanceRequestCommand { get; }

        public IList<AttendanceLog> AttendanceLogList { get; set; } = []; 

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
                var employee = await _unitOfWork.EmployeeRepository.GetAsync(e => e.UserId == user.UserId, includeProperties: "EmployeeAccountInfo,EmployeeAttendances,EmployeeLeaves,EmployeePayslips");
                if (employee != null)
                {
                    // Load Side Bar Data
                    if (employee.EmployeeAccountInfo != null)
                    { 
                        FullName = employee.EmployeeAccountInfo.FullName;
                        Role = employee.EmployeeAccountInfo.Role;
                        Department = user.Department.Name;
                        DailyRate = $"Php {employee.BasicDailyRate.ToString("N2")} per day";
                        WorkShift = $"{employee.WorkShiftStart} - {employee.WorkShiftEnd}";
                        Email = user.Email;
                        Phone = employee.EmployeeAccountInfo.PrimaryPhoneNumber;
                        Website = employee.EmployeeAccountInfo.WebsiteUrl;
                        if (employee.EmployeeAccountInfo.EmploymentType == EmploymentType.Regular)
                            EmploymentStatus = "Regular Employee";
                        else
                            EmploymentStatus = "Independent Contractor";
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
