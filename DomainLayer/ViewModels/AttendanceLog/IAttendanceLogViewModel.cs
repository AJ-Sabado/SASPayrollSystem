namespace DomainLayer.ViewModels.AttendanceLog
{
    public interface IAttendanceLogViewModel
    {
        string Date { get; set; }
        string Status { get; set; }
        string TimeIn { get; set; }
        string TimeOut { get; set; }
        string TotalHours { get; set; }
    }
}