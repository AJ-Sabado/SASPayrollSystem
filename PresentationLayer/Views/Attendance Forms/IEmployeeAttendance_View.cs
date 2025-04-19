using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Views.Attendance_Forms
{
    internal interface IEmployeeAttendance_View
    {
        bool isWorking { get; set; }
        int formStatus { get; }
        void UpdateLogButtonProperties(string buttonText);
        void ShowMessage(string message);
        void CloseForm();
        void FormStatus(int status);

        public Action<bool> OnAttendanceLogged { get; set; }

        string ShowFileDialog(string filter, string title);
        void SetAttachedFile(string fileName, byte[] fileData);
    }
}
