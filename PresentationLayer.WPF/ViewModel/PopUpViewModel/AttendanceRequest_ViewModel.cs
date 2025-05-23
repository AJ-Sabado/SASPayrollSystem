using DomainLayer.Enums;
using DomainLayer.Models.EmployeeAttendanceRequest;
using Microsoft.Win32;
using PresentationLayer.WPF.Services;
using ServicesLayer;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.WPF.ViewModel
{
    public class AttendanceRequest_ViewModel : Base_ViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPopUpService _popUpService;
        private Guid _employeeId;
        private string _employeeName = "John Jane Doe S. Smith";
        private string _employeeID = "0000000";
        private string _department = "Accounting Management";
        private string _role = "Senior Software Developer";
        private DateTime _date = DateTime.Now;
        private DateTime? _timeIn;
        private DateTime? _timeOut;
        private DateTime? _breakStart;
        private DateTime? _breakEnd;
        private string _totalHours = "0 hours";
        private string _proofFiles = "Attach File";
        private string _reason = string.Empty;

        public AttendanceRequest_ViewModel(IUnitOfWork unitOfWork, IPopUpService popUpService)
        {
            _unitOfWork = unitOfWork;
            _popUpService = popUpService;
            AttachFileCommand = new RelayCommand(AttachFiles);
            Request = new RelayCommand(RequestAttendance, _ => true);
            LoadEmployeeData();
        }

        private async void LoadEmployeeData()
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(e => e.UserId == Properties.Settings.Default.CurrentUserGuid);
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "Department,AccountInfo");
            if (employee != null && user.AccountInfo != null && user.Department != null)
            {
                _employeeId = employee.EmployeeId;
                EmployeeName = user.AccountInfo.FullName;
                EmployeeID = user.AccountInfo.CompanyId;
                Department = user.Department.Name;
                Role = user.AccountInfo.Role;
                if (_popUpService.IdSource != null)
                {
                    var currentAttendanceRequest = employee.EmployeeAttendanceRequests.FirstOrDefault(r => r.Id == _popUpService.IdSource);
                    if (currentAttendanceRequest != null)
                    {
                        Date = currentAttendanceRequest.AttendanceDate.ToDateTime(new TimeOnly());
                        TimeIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentAttendanceRequest.TimeIn.Hour, currentAttendanceRequest.TimeIn.Minute, currentAttendanceRequest.TimeIn.Second);
                        TimeOut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentAttendanceRequest.TimeOut.Hour, currentAttendanceRequest.TimeOut.Minute, currentAttendanceRequest.TimeOut.Second);
                        if (currentAttendanceRequest.BreakStart.HasValue)
                            BreakStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentAttendanceRequest.BreakStart.Value.Hour, currentAttendanceRequest.BreakStart.Value.Minute, currentAttendanceRequest.BreakStart.Value.Second);
                        if (currentAttendanceRequest.BreakEnd.HasValue)
                            BreakEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, currentAttendanceRequest.BreakEnd.Value.Hour, currentAttendanceRequest.BreakEnd.Value.Minute, currentAttendanceRequest.BreakEnd.Value.Second);
                        Reason = currentAttendanceRequest.Reason;
                        TotalHours = $"{currentAttendanceRequest.TotalHours} hours";
                    }
                    else
                    {
                        MessageBox.Show("Attendance request not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    TimeIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, employee.DefaultWorkShiftStart.Hour, employee.DefaultWorkShiftStart.Minute, employee.DefaultWorkShiftStart.Second);
                    TimeOut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, employee.DefaultWorkShiftEnd.Hour, employee.DefaultWorkShiftEnd.Minute, employee.DefaultWorkShiftEnd.Second);
                }
            }
            else
            {
                MessageBox.Show("Employee not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string EmployeeName
        {
            get => _employeeName;
            set => SetProperty(ref _employeeName, value);
        }

        public string EmployeeID
        {
            get => _employeeID;
            set => SetProperty(ref _employeeID, value);
        }

        public string Department
        {
            get => _department;
            set => SetProperty(ref _department, value);
        }

        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public DateTime? TimeIn
        {
            get => _timeIn;
            set
            {
                if (SetProperty(ref _timeIn, value))
                {
                    ValidateTimes();
                    CalculateTotalHours();
                }
            }
        }

        public DateTime? TimeOut
        {
            get => _timeOut;
            set
            {
                if (SetProperty(ref _timeOut, value))
                {
                    ValidateTimes();
                    CalculateTotalHours();
                }
            }
        }

        public DateTime? BreakStart
        {
            get => _breakStart;
            set
            {
                if (SetProperty(ref _breakStart, value))
                {
                    ValidateTimes();
                    CalculateTotalHours();
                }
            }
        }

        public DateTime? BreakEnd
        {
            get => _breakEnd;
            set
            {
                if (SetProperty(ref _breakEnd, value))
                {
                    ValidateTimes();
                    CalculateTotalHours();
                }
            }
        }

        private string _totalBreakHours = "0 hours";
        public string TotalBreakHours
        {
            get => _totalBreakHours;
            set => SetProperty(ref _totalBreakHours, value);
        }

        private void ValidateTimes()
        {
            if (TimeIn.HasValue && TimeOut.HasValue && TimeOut < TimeIn)
            {
                MessageBox.Show("Time Out cannot be earlier than Time In.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Reset TimeOut back to null (or TimeIn, whichever you want)
                TimeIn = null;
                TimeOut = null;
            }
            //Break checks
            if (BreakStart.HasValue && BreakEnd.HasValue)
            {
                if (BreakEnd < BreakStart)
                {
                    MessageBox.Show("Break End cannot be earlier than Break Start.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                    BreakStart = null;
                    BreakEnd = null;
                }
            }
            if (TimeIn.HasValue && TimeOut.HasValue && BreakStart.HasValue && BreakEnd.HasValue)
            {
                //Check if breaks are within time in and time out
                bool pass = BreakStart < BreakEnd && TimeIn < BreakStart && TimeOut > BreakEnd;
                if (!pass)
                {
                    MessageBox.Show("Breaks must be between Time In and Time Out.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                    BreakStart = null;
                    BreakEnd = null;
                }
            }
        }

        public string Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
        }


        public string TotalHours
        {
            get => _totalHours;
            set => SetProperty(ref _totalHours, value);
        }

        public string ProofFiles
        {
            get => _proofFiles;
            set => SetProperty(ref _proofFiles, value);
        }

        public ICommand AttachFileCommand { get; }
        public ICommand Request { get; }
        private async void RequestAttendance(object? parameter)
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByIdAsync(_employeeId);
            if (employee != null && TimeIn.HasValue && TimeOut.HasValue)
            {
                //Checks
                if (TimeIn > TimeOut)
                {
                    MessageBox.Show("Time Out cannot be earlier than Time In.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(Reason))
                {
                    MessageBox.Show("Please provide a reason for the attendance request.", "Missing Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_popUpService.IdSource != null)
                {
                    var currentAttendanceRequest = employee.EmployeeAttendanceRequests.FirstOrDefault(r => r.Id == _popUpService.IdSource);
                    if (currentAttendanceRequest != null)
                    {
                        currentAttendanceRequest.AttendanceDate = DateOnly.FromDateTime(Date);
                        currentAttendanceRequest.TimeIn = TimeOnly.FromDateTime((DateTime)this.TimeIn);
                        currentAttendanceRequest.TimeOut = TimeOnly.FromDateTime((DateTime)this.TimeOut);
                        if (BreakStart.HasValue)
                            currentAttendanceRequest.BreakStart = TimeOnly.FromDateTime((DateTime)this.BreakStart);
                        if (BreakEnd.HasValue)
                            currentAttendanceRequest.BreakEnd = TimeOnly.FromDateTime((DateTime)this.BreakEnd);
                        currentAttendanceRequest.Reason = this.Reason;
                        currentAttendanceRequest.Status = FormStatus.Pending;
                        await _unitOfWork.Save();
                        MessageBox.Show("Attendance request updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Attendance request not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    var attendanceRequest = new EmployeeAttendanceRequestModel()
                    {
                        EmployeeId = employee.EmployeeId,
                        Employee = employee,
                        RequestDate = DateOnly.FromDateTime(DateTime.Now),
                        AttendanceDate = DateOnly.FromDateTime(this.Date),
                        TimeIn = TimeOnly.FromDateTime((DateTime)this.TimeIn),
                        TimeOut = TimeOnly.FromDateTime((DateTime)this.TimeOut),
                        BreakStart = this.BreakStart.HasValue ? TimeOnly.FromDateTime((DateTime)this.BreakStart) : null,
                        BreakEnd = this.BreakEnd.HasValue ? TimeOnly.FromDateTime((DateTime)this.BreakEnd) : null,
                        Status = FormStatus.Pending,
                        Reason = this.Reason
                    };
                    employee.EmployeeAttendanceRequests.Add(attendanceRequest);
                    await _unitOfWork.Save();
                    MessageBox.Show("Attendance request submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            _popUpService.ClosePopup();
        }

        private void AttachFiles(object? obj)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Allowed Files|*.jpg;*.jpeg;*.png;*.pdf;*.docx"
            };

            if (dialog.ShowDialog() == true)
            {
                ProofFiles = string.Join(", ", dialog.SafeFileNames);
            }
        }

        private void CalculateTotalHours()
        {
            if (TimeIn.HasValue && TimeOut.HasValue)
            {
                var hours = (TimeOut.Value - TimeIn.Value).TotalHours;
                if (BreakStart.HasValue && BreakEnd.HasValue)
                {
                    var breakHours = (BreakEnd.Value - BreakStart.Value).TotalHours;
                    breakHours = Math.Max(0, breakHours);
                    TotalBreakHours = $"{breakHours:0.#} hour{(breakHours == 1 ? "" : "s")}";
                    hours -= breakHours;
                }
                //Account mandated work break
                hours = Math.Max(0, hours);
                TotalHours = $"{hours:0.#} hour{(hours == 1 ? "" : "s")}";
            }
            else
            {
                TotalHours = "0 hours";
            }
        }

    }
}
