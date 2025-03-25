namespace DomainLayer.ViewModels.JobDeskDetails
{
    public interface IJobDeskDetailsViewModel
    {
        string BaseSalary { get; set; }
        string Department { get; set; }
        string Email { get; set; }
        string EmployeeName { get; set; }
        string EmploymentDate { get; set; }
        string EmploymentStatus { get; set; }
        string Phone { get; set; }
        string Website { get; set; }
        string WorkShift { get; set; }
    }
}