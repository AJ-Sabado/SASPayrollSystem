using DomainLayer.Models.Attendance;
using PresentationLayer.Views.Attendance_Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters.Employee_Attendance
{
    internal class EmployeeAttendance_Presenter
    {
        private readonly IEmployeeAttendance_View _view;

        public EmployeeAttendance_Presenter(IEmployeeAttendance_View view)
        {
            _view = view;
        }

        public void SetFormStatus(int status)
        {
            _view.FormStatus(status);

            string buttonText = status switch
            {
                1 => "Time In",
                2 => "Time Out",
                _ => ""
            };

            if (!string.IsNullOrEmpty(buttonText))
                _view.UpdateLogButtonProperties(buttonText);
        }

        public void HandleAttendanceLogClick()
        {
            DateTime currentTime = DateTime.UtcNow;
            string formattedTime = currentTime.ToString("d:hh:mm tt");

            if (_view.formStatus == 1)
            {
                _view.ShowMessage($"Time in successful! Time in time @ {formattedTime}");
                _view.OnAttendanceLogged?.Invoke(true); // Notify as working
                _view.isWorking = true; // Set working status
                SetFormStatus(2);
            }
            else if (_view.formStatus == 2)
            {
                _view.ShowMessage($"Time out successful! Time out time @ {formattedTime}, Total hours worked: 12 Hours");
                _view.OnAttendanceLogged?.Invoke(false); // Notify as not working
                _view.isWorking = false; // Set working status
                SetFormStatus(1);
            }

            _view.CloseForm();
        }

        public void HandleAttachFileClick()
        {
            string filePath = _view.ShowFileDialog(
                "PDF Files|*.pdf|Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                "Attach Proof of Attendance");

            if (!string.IsNullOrEmpty(filePath))
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                _view.SetAttachedFile(filePath, fileData);
            }
        }

        public void HandleRequestClick()
        {
            // Handle request logic here
            _view.ShowMessage("Request submitted successfully!");
            _view.CloseForm();
        }
    }
}
