using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DomainLayer.Enums;
using DomainLayer.Models.EmployeeLeave;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class LeaveRequestView_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly MyMessageBox _messageBox;

        private EmployeeLeaveModel? _currentLeaveRequest = null;

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

        private LeaveType _selectedLeaveType = LeaveType.Emergency;
        public LeaveType SelectedLeaveType
        {
            get => _selectedLeaveType;
            set
            {
                _selectedLeaveType = value;
                OnPropertyChanged(nameof(SelectedLeaveType));
            }
        }

        private uint _duration = 0;
        public uint Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        private DateTime? _startDate = null;
        public DateTime? StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                Duration = (uint)(ReturnDate?.Subtract(StartDate ?? DateTime.Now).TotalDays ?? 0);
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime? _returnDate = null;
        public DateTime? ReturnDate
        {
            get => _returnDate;
            set
            {
                _returnDate = value;
                Duration = (uint)(ReturnDate?.Subtract(StartDate ?? DateTime.Now).TotalDays ?? 0);
                OnPropertyChanged(nameof(ReturnDate));
            }
        }

        private string _attachment = string.Empty;
        

        public string Attachment
        {
            get => _attachment;
            set
            {
                _attachment = value;
                OnPropertyChanged(nameof(Attachment));
            }
        }

        public ICommand Approve { get; set; }
        public ICommand Disapprove { get; set; }

        public LeaveRequestView_ViewModel(IPopUpService popUpService, IAdminOperationsService adminOperationsService, MyMessageBox messageBox)
        {
            _popUpService = popUpService;
            _adminOperationsService = adminOperationsService;
            _messageBox = messageBox;

            Approve = new RelayCommand(ExecuteApprove, _ => true);
            Disapprove = new RelayCommand(ExecuteDisapprove, _ => true);

            LoadDataFromDb();
        }

        private async Task LoadDataFromDb()
        {
            _currentLeaveRequest = await _adminOperationsService
                .GetEmployeeLeaveRequest(_popUpService.IdSource ?? Guid.Empty);
            if (_currentLeaveRequest != null)
            {
                if (_currentLeaveRequest.Employee.User.AccountInfo != null)
                {
                    EmployeeName = _currentLeaveRequest.Employee.User.AccountInfo.FullName;
                    EmployeeId = _currentLeaveRequest.Employee.User.AccountInfo.CompanyId;
                }
                Department = _currentLeaveRequest.Employee.User.Department.Name;
                SelectedLeaveType = _currentLeaveRequest.Type;
                StartDate = _currentLeaveRequest.DateOfAbsenceStart.ToDateTime(TimeOnly.MinValue);
                ReturnDate = _currentLeaveRequest.DateOfReturn.ToDateTime(TimeOnly.MinValue);

                if (Duration != _currentLeaveRequest.Duration)
                    Duration = _currentLeaveRequest.Duration;
            }
        }

        private async void ExecuteDisapprove(object? obj)
        {
            var result = _messageBox.ShowDialog("Are you sure you want to disapprove this leave request?", MyMessageBoxType.Confirmation);

            if (result == null || result.MyMessageBoxDialogResult != MyMessageBoxDialogResult.Yes)
                return;

            if (_currentLeaveRequest == null)
            {
                _messageBox.ShowDialog("Leave request does not exist!", MyMessageBoxType.Error);
                return;
            }

            _currentLeaveRequest.Status = FormStatus.Denied;
            await UpdateLeaveRequest();
        }

        private async Task UpdateLeaveRequest()
        {
            if (_currentLeaveRequest == null)
                return;

            if (!StartDate.HasValue || !ReturnDate.HasValue)
            {
                _messageBox.ShowDialog("Start date and return date must not be empty!", MyMessageBoxType.Error);
                return;
            }

            _currentLeaveRequest.Type = SelectedLeaveType;
            _currentLeaveRequest.Duration = Duration;
            _currentLeaveRequest.DateOfAbsenceStart = DateOnly.FromDateTime(StartDate.Value);
            _currentLeaveRequest.DateOfReturn = DateOnly.FromDateTime(ReturnDate.Value);

            await _adminOperationsService.UpdateEmployeeLeaveRequest(_currentLeaveRequest);
            _messageBox.ShowDialog("Action successful!", MyMessageBoxType.Success);
            _popUpService.ClosePopup();
        }

        private async void ExecuteApprove(object? obj)
        {
            var result = _messageBox.ShowDialog("Are you sure you want to approve this leave request?", MyMessageBoxType.Confirmation);

            if (result == null || result.MyMessageBoxDialogResult != MyMessageBoxDialogResult.Yes)
                return;

            if (_currentLeaveRequest == null)
            {
                _messageBox.ShowDialog("Leave request does not exist!", MyMessageBoxType.Error);
                return;
            }

            _currentLeaveRequest.Status = FormStatus.Approved;
            await UpdateLeaveRequest();
        }
    }
}
