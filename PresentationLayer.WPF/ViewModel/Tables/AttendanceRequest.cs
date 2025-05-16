using System.Security.Policy;

namespace PresentationLayer.WPF.ViewModel.Tables
{
    public class AttendanceRequest
    {
        public DateOnly RequestDate { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public TimeOnly TimeIn { get; set; }
        public TimeOnly TimeOut { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
