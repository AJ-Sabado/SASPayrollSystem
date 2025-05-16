namespace PresentationLayer.WPF.ViewModel.Tables
{
    public class LeaveRequest
    {
        public DateOnly RequestDate { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string TotalDays { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

    }
}
