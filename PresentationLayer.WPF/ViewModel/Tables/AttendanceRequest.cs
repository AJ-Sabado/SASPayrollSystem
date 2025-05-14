using System.Security.Policy;

namespace PresentationLayer.WPF.ViewModel.Tables
{
    public class AttendanceRequest
    {
        public DateOnly RequestDate { get; set; }
        public DateOnly AttendaceDate { get; set; }
        public TimeOnly TimeIn { get; set; }
        public TimeOnly TimeOut { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
