using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModels.AttendanceLog
{
    public class AttendanceLogViewModel : IAttendanceLogViewModel
    {
        public string Date { get; set; } = string.Empty;
        public string TimeIn { get; set; } = string.Empty;
        public string TimeOut { get; set; } = string.Empty;
        public string TotalHours { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
