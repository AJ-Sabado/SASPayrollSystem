namespace PresentationLayer.WPF.ViewModel.Tables
{
    public class AttendanceLog
    {
        public DateOnly Date { get; set; }
        public TimeOnly TimeIn { get; set; }
        public TimeOnly TimeOut { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Overtime { get; set; } = string.Empty;
        public string OTDuration { get; set; } = string.Empty;
    }
}
