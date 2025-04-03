using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeeFinancialInfo
{
    public interface IEmployeeFinancialInfoModel
    {
        string BankingDetails { get; set; }
        EmployeeModel Employee { get; set; }
        Guid EmployeeId { get; set; }
        Guid Id { get; set; }
        string PagIbigIdNumber { get; set; }
        string PhilHealthIdNumber { get; set; }
        string SSSIdNumber { get; set; }
        string TaxIdNumber { get; set; }
    }
}