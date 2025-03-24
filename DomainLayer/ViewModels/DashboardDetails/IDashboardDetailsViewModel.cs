namespace DomainLayer.ViewModels.DashboardDetails
{
    public interface IDashboardDetailsViewModel
    {
        string Allowance { get; set; }
        string Bonuses { get; set; }
        string Deductions { get; set; }
        string TotalHours { get; set; }
        string TotalSalary { get; set; }
        string UpcomingDate { get; set; }
    }
}