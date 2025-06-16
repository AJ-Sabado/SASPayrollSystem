using System.Windows.Input;
using DomainLayer.Enums;
using DomainLayer.Models.User;
using PresentationLayer.WPF.Services;
using ServicesLayer;

namespace PresentationLayer.WPF.ViewModel.PopUpViewModel
{
    public class AssignLeave_ViewModel : Base_ViewModel
    {
        private readonly IPopUpService _popUpService;
        private readonly IAdminOperationsService _adminOperationsService;
        private readonly MyMessageBox _messageBox;

        private UserModel? _selectedUser = null;

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
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Duration), "Duration cannot be negative.");
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
                OnPropertyChanged(nameof(ReturnDate));
            }
        }

        public ICommand Assign { get; set; }
        public ICommand Cancel { get; set; }

        public AssignLeave_ViewModel(IPopUpService popUpService, IAdminOperationsService adminOperationsService, MyMessageBox messageBox)
        {
            _popUpService = popUpService;
            _adminOperationsService = adminOperationsService;
            _messageBox = messageBox;

            Assign = new RelayCommand(ExecuteAssign, _ => true);
            Cancel = new RelayCommand(ExecuteCancel, _ => true);
        }

        private void ExecuteCancel(object? obj)
        {
            _popUpService.ClosePopup();
        }

        private void ExecuteAssign(object? obj)
        {
            _popUpService.ClosePopup();
        }
    }
}
