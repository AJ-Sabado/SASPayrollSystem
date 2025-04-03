using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Views.FileLeaveForm
{
    internal interface IFileLeave_Form
    {
        string EmployeeName { get; set; }
        string EmployeeID { get; set; }
        string Department { get; set; }
        string LeaveType { get; }
        int Duration { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        string AttachmentFileName { get; set; }

    }
}
