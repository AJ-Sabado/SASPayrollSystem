using Microsoft.Win32;
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

        private string _employeeName = "John Jane Doe S. Smith";
        private string _employeeID = "0000000";
        private string _department = "Accounting Management";
        private string _role = "Senior Software Developer";
        private DateTime _date = DateTime.Now;
        private DateTime? _timeIn;
        private DateTime? _timeOut;
        private string _totalHours = "0 hours";
        private string _proofFiles = "Attach File";

        public AttendanceRequest_ViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            AttachFileCommand = new RelayCommand(AttachFiles);
            Request = new RelayCommand(RequestAttendance, _ => true);
            LoadEmployeeData();
        }

        private async void LoadEmployeeData()
        {
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(e => e.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "EmployeeAccountInfo");
            var user = await _unitOfWork.UserRepository.GetAsync(u => u.UserId == Properties.Settings.Default.CurrentUserGuid, includeProperties: "Department");
            if (employee != null && employee.EmployeeAccountInfo != null && user.Department != null)
            {
                EmployeeName = employee.EmployeeAccountInfo.FullName;
                EmployeeID = employee.EmployeeAccountInfo.CompanyId;
                Department = user.Department.Name;
                Role = employee.EmployeeAccountInfo.Role;
                TimeIn = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, employee.WorkShiftStart.Hour, employee.WorkShiftStart.Minute, employee.WorkShiftStart.Second);
                TimeOut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, employee.WorkShiftEnd.Hour, employee.WorkShiftEnd.Minute, employee.WorkShiftEnd.Second);
                //TO DO - Add AttendanceRequestModel
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

        private void ValidateTimes()
        {
            if (TimeIn.HasValue && TimeOut.HasValue && TimeOut < TimeIn)
            {
                MessageBox.Show("Time Out cannot be earlier than Time In.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                // Reset TimeOut back to null (or TimeIn, whichever you want)
                TimeOut = null;
            }
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
            //TO DO - Add AttendanceRequestModel 
            MessageBox.Show("Attendance request submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
                //Account mandated work break
                hours = Math.Max(0, hours) - 1;
                TotalHours = $"{hours:0.#} hour{(hours == 1 ? "" : "s")}";
            }
            else
            {
                TotalHours = "0 hours";
            }
        }

    }
}
