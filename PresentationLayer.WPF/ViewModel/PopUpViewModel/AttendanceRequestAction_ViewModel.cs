using System.Windows.Input;
using DomainLayer.Enums;
using DomainLayer.Models.EmployeeAttendanceRequest;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class AttendanceRequestAction_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly MyMessageBox _messageBox;

        private EmployeeAttendanceRequestModel? _currentEmployeeAttendanceRequest = null;

        private string _employeeName = string.Empty;
        public string EmployeeName
        {
            get => _employeeName;
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
            }
        }

        private string _employeeId = string.Empty;
        public string EmployeeId
        {
            get => _employeeId;
            set
            {
                _employeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }

        private string _department = string.Empty;
        public string Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged(nameof(Department));
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

        private DateTime? _selectedDate = null;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private string _reason = string.Empty;
        public string Reason
        {
            get => _reason;
            set
            {
                _reason = value;
                OnPropertyChanged(nameof(Reason));
            }
        }

        private DateTime? _timeIn = null;
        public DateTime? TimeIn
        {
            get => _timeIn;
            set
            {
                _timeIn = value;
                OnPropertyChanged(nameof(TimeIn));
                OnPropertyChanged(nameof(TotalHours));
            }
        }

        private DateTime? _timeOut = null;
        public DateTime? TimeOut
        {
            get => _timeOut;
            set
            {
                _timeOut = value;
                OnPropertyChanged(nameof(TimeOut));
                OnPropertyChanged(nameof(TotalHours));
            }
        }

        public string TotalHours
        {
            get
            {
                TimeSpan timeSpan = TimeSpan.Zero;
                if (TimeIn.HasValue && TimeOut.HasValue)
                {
                    timeSpan = TimeOut.Value - TimeIn.Value;
                    if (BreakStart.HasValue && BreakEnd.HasValue)
                    {
                        timeSpan -= (BreakEnd.Value - BreakStart.Value);
                    }
                    var totalHours = Math.Ceiling(timeSpan.TotalHours * 100) / 100;
                    return $"{totalHours:G29} Hour{(totalHours == 1 ? "" : "s")}";
                }
                return "0 Hours";
            }
        }

        private DateTime? _breakStart = null;
        public DateTime? BreakStart
        {
            get => _breakStart;
            set
            {
                _breakStart = value;
                OnPropertyChanged(nameof(BreakStart));
                OnPropertyChanged(nameof(BreakTotal));
                OnPropertyChanged(nameof(TotalHours));
            }
        }

        private DateTime? _breakEnd = null;
        public DateTime? BreakEnd
        {
            get => _breakEnd;
            set
            {
                _breakEnd = value;
                OnPropertyChanged(nameof(BreakEnd));
                OnPropertyChanged(nameof(BreakTotal));
                OnPropertyChanged(nameof(TotalHours));
            }
        }

        public string BreakTotal
        {
            get
            {
                if (BreakStart.HasValue && BreakEnd.HasValue)
                {
                    TimeSpan breakTime = BreakEnd.Value - BreakStart.Value;
                    var totalHours = Math.Ceiling(breakTime.TotalHours * 100) / 100;
                    return $"{totalHours:G29} Hour{(totalHours == 1 ? "" : "s")}";
                }
                return "0 Hours";
            }
        }

        public ICommand Approve { get; set; }
        public ICommand Disapprove { get; set; }

        public AttendanceRequestAction_ViewModel(IPopUpService popUpService, IAdminOperationsService adminOperationsService, MyMessageBox messageBox)
        {
            _popUpService = popUpService;
            _adminOperationsService = adminOperationsService;
            _messageBox = messageBox;

            Approve = new RelayCommand(ExecuteApprove, _ => true);
            Disapprove = new RelayCommand(ExecuteDisapprove, _ => true);

            LoadData();
        }

        private async Task LoadData()
        {
            _currentEmployeeAttendanceRequest = await _adminOperationsService.GetEmployeeAttendanceRequest(_popUpService.IdSource ?? Guid.Empty);
            if (_currentEmployeeAttendanceRequest != null)
            {
                EmployeeName = _currentEmployeeAttendanceRequest.Employee.User.AccountInfo?.FullName ?? "N/A";
                EmployeeId = _currentEmployeeAttendanceRequest.Employee.User.AccountInfo?.CompanyId ?? "N/A";
                Department = _currentEmployeeAttendanceRequest.Employee.User.Department.Name;
                Role = _currentEmployeeAttendanceRequest.Employee.User.Role.Name;
                SelectedDate = _currentEmployeeAttendanceRequest.AttendanceDate.ToDateTime(TimeOnly.MinValue);
                Reason = _currentEmployeeAttendanceRequest.Reason;
                TimeIn = DateTime.MinValue.Add(_currentEmployeeAttendanceRequest.TimeIn.ToTimeSpan());
                TimeOut = DateTime.MinValue.Add(_currentEmployeeAttendanceRequest.TimeOut.ToTimeSpan());
                if (_currentEmployeeAttendanceRequest.BreakStart.HasValue)
                    BreakStart = DateTime.MinValue.Add(_currentEmployeeAttendanceRequest.BreakStart.Value.ToTimeSpan());
                if (_currentEmployeeAttendanceRequest.BreakEnd.HasValue)
                    BreakEnd = DateTime.MinValue.Add(_currentEmployeeAttendanceRequest.BreakEnd.Value.ToTimeSpan());
            }
        }

        private async void ExecuteDisapprove(object? obj)
        {
            var confirm = _messageBox.ShowDialog("Are you sure you want to deny this attendance request?", MyMessageBoxType.Confirmation);
            if (confirm == null || confirm.MyMessageBoxDialogResult != MyMessageBoxDialogResult.Yes)
            {
                //_popUpService.ClosePopup();
                return;
            }

            if (_currentEmployeeAttendanceRequest != null)
            {
                _currentEmployeeAttendanceRequest.Status = FormStatus.Denied;
                await UpdateAttendanceRequest();
            }
            _popUpService.ClosePopup();
        }

        private async void ExecuteApprove(object? obj)
        {
            var confirm = _messageBox.ShowDialog("Are you sure you want to approve this attendance request?", MyMessageBoxType.Confirmation);
            if (confirm == null || confirm.MyMessageBoxDialogResult != MyMessageBoxDialogResult.Yes)
            {
                //_popUpService.ClosePopup();
                return;
            }

            if (_currentEmployeeAttendanceRequest != null)
            {
                _currentEmployeeAttendanceRequest.Status = FormStatus.Approved;
                await UpdateAttendanceRequest();
            }
            _popUpService.ClosePopup();
        }

        private async Task UpdateAttendanceRequest()
        {
            if (SelectedDate == null || TimeIn == null || TimeOut == null)
            {
                _messageBox.ShowDialog("Please fill in all required fields.", MyMessageBoxType.Error);
                return;
            }

            if (_currentEmployeeAttendanceRequest != null)
            {
                _currentEmployeeAttendanceRequest.AttendanceDate = DateOnly.FromDateTime(SelectedDate.Value);
                _currentEmployeeAttendanceRequest.TimeIn = TimeOnly.FromDateTime(TimeIn.Value);
                _currentEmployeeAttendanceRequest.TimeOut = TimeOnly.FromDateTime(TimeOut.Value);
                if (BreakStart.HasValue)
                    _currentEmployeeAttendanceRequest.BreakStart = TimeOnly.FromDateTime(BreakStart.Value);
                if (BreakEnd.HasValue)
                    _currentEmployeeAttendanceRequest.BreakEnd = TimeOnly.FromDateTime(BreakEnd.Value);
                // Save changes to the database
                await _adminOperationsService.UpdateEmployeeAttendanceRequest(_currentEmployeeAttendanceRequest);
            }
        }
    }
}
