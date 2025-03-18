using DomainLayer.Models.Employee;

namespace DomainLayer.Models.Contribution
{
    public interface IContributionModel
    {
        EmployeeModel Employee { get; set; }
        Guid EmployeeId { get; set; }
        Guid Id { get; set; }
        decimal PagIbigAmount { get; set; }
        decimal PhilHealthAmount { get; set; }
        decimal SSSAmount { get; set; }
        decimal TotalContributions { get; set; }
    }
}