using DomainLayer.Models.Employee;
using DomainLayer.Models.Payroll;

namespace DomainLayer.Models.Salary
{
    public interface ISalaryModel
    {
        decimal AddAdjustments { get; set; }
        decimal AllowancesAmount { get; set; }
        decimal BonusesAmount { get; set; }
        decimal CashAdvance { get; set; }
        decimal DaysAmount { get; set; }
        uint DaysWorked { get; set; }
        decimal DisposableIncome { get; set; }
        EmployeeModel Employee { get; set; }
        Guid EmployeeId { get; set; }
        Guid Id { get; set; }
        decimal IncomeNetOfTax { get; set; }
        decimal LatesDeductionAmount { get; set; }
        decimal LatesMinutes { get; set; }
        decimal NetPay { get; set; }
        decimal NightsAmount { get; set; }
        uint NightsOT { get; set; }
        decimal NightsOTAmount { get; set; }
        uint NightsWorked { get; set; }
        decimal PagIbigLoanAmount { get; set; }
        PayrollModel Payroll { get; set; }
        Guid PayrollId { get; set; }
        uint RegularHolidayNightOT { get; set; }
        decimal RegularHolidayNightsAmount { get; set; }
        uint RegularHolidayNightsWorked { get; set; }
        uint RegularHolidayOT { get; set; }
        decimal RegularHolidayOTAmount { get; set; }
        decimal RegularHolidayOTNightAmount { get; set; }
        decimal RegularHolidaysAmount { get; set; }
        uint RegularHolidaysWorked { get; set; }
        uint RegularOT { get; set; }
        decimal RegularOTAmount { get; set; }
        uint SpecialHolidayNightOT { get; set; }
        decimal SpecialHolidayNightOTAmount { get; set; }
        decimal SpecialHolidayNightsAmount { get; set; }
        uint SpecialHolidayNightsWorked { get; set; }
        uint SpecialHolidayOT { get; set; }
        decimal SpecialHolidayOTAmount { get; set; }
        decimal SpecialHolidaysAmount { get; set; }
        uint SpecialHolidaysWorked { get; set; }
        decimal SSSLoanAmount { get; set; }
        decimal SubtractAdjustments { get; set; }
        decimal TaxableIncome { get; set; }
        decimal TaxWithholdings { get; set; }
        decimal TotalBasic { get; }
        decimal TotalCompanyLoans { get; set; }
        decimal TotalOT { get; set; }
        decimal Vale { get; set; }

        void UpdateSalary(bool skipAttendances = false, bool skipLeaves = false);
    }
}