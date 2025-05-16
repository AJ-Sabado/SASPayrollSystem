using System.Windows.Input;
using DomainLayer.Enums;
using DomainLayer.Models.EmployeeLeave;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class LeaveRequest_ViewModel : Base_ViewModel
    {
        private string _employeeName = "Employee name";
        private string _employeeId = "Employee Id";
        private string _department = "Department";
        private LeaveType _selectedLeaveType = LeaveType.Sick;
        private uint _duration = 0;
        private DateTime? startDate;
        private DateTime? returnDate;
        private string _attachment = string.Empty;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPopUpService _popUpService;

        //Binding properties

        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }
        public string EmployeeId
        {
            get => _employeeId;
            set
            {
                _employeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }
        public string Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged(nameof(Department));
            }
        }
        public LeaveType SelectedLeaveType
        {
            get => _selectedLeaveType;
            set
            {
                _selectedLeaveType = value;
                OnPropertyChanged(nameof(SelectedLeaveType));
            }
        }
        public uint Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime? ReturnDate
        {
            get => returnDate;
            set
            {
                returnDate = value;
                Duration = (uint)(ReturnDate?.Subtract(StartDate ?? DateTime.Now).TotalDays ?? 0);
                OnPropertyChanged(nameof(ReturnDate));
            }
        }
        public string Attachment
        {
            get => _attachment;
            set
            {
                _attachment = value;
                OnPropertyChanged(nameof(Attachment));
            }
        }

        public ICommand SendRequest { get; }

        //Constructor
        public LeaveRequest_ViewModel(IUnitOfWork unitOfWork, IPopUpService popUpService)
        {
            _unitOfWork = unitOfWork;
            _popUpService = popUpService;
            SendRequest = new RelayCommand(ExecuteSendRequest, _ => true);
            LoadData();
        }

        private async void ExecuteSendRequest(object? obj)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(e => e.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "EmployeeLeaves");
            if (employee != null && StartDate.HasValue && ReturnDate.HasValue)
            {
                var leave = new EmployeeLeaveModel()
                {
                    EmployeeId = employee.EmployeeId,
                    Employee = employee,
                    DateOfFiling = DateOnly.FromDateTime(DateTime.Now),
                    DateOfAbsenceStart = DateOnly.FromDateTime(StartDate.Value),
                    DateOfAbsenceEnd = DateOnly.FromDateTime(ReturnDate.Value),
                    Duration = this.Duration,
                    Type = this.SelectedLeaveType
                };
                employee.EmployeeLeaves.Add(leave);
                await _unitOfWork.Save();
                System.Windows.MessageBox.Show("Leave Request Filled!");
            }
            _popUpService.ClosePopup();

        }

        private async void LoadData()
        {
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "Employee,Department");
            if (user != null && user.Employee != null && user.Employee.EmployeeAccountInfo != null)
            {
                EmployeeName = user.Employee.EmployeeAccountInfo.FullName;
                EmployeeId = user.Employee.EmployeeAccountInfo.CompanyId;
                Department = user.Department.Name;

            }
        }
    }
}
