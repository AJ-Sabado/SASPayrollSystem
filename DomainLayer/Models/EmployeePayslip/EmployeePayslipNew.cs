using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeePayslip
{
    public class EmployeePayslipNew
    {
        [Key]
        public Guid EmployeePayslipId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Guid EmployeeId { get; set; }
        public EmployeeModel Employee { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateOnly PeriodStart { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PeriodEnd { get; set; }

        //Gross Salary
        [Column(TypeName = "smallint")]
        public uint HoursWorked { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal BasicPay { get; set; } = 0;

        [Column(TypeName = "smallint")]
        public uint LegalHolidaysHours { get; set; } = 0;
        [Column(TypeName ="money")]
        public decimal LegalHolidaysPay { get; set; } = 0;

        [Column(TypeName = "smallint")]
        public uint NDOnWorkingDayHours { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal NDOnWorkingDayPay { get; set; } = 0;


        //Allowance
        [Column(TypeName = "money")]
        public decimal UtilityAllowance { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal MealAllowance { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal LoadAllowance { get; set; } = 0;

        [NotMapped]
        public decimal TotalGrossPay
        {
            get
            {
                return BasicPay + LegalHolidaysPay + NDOnWorkingDayPay + UtilityAllowance + MealAllowance + LoadAllowance;
            }
        }

        //Deductions
        [Column(TypeName = "money")]
        public decimal PHIC { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal HDMF { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal DecemberSSS { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal DecemberHDMF { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal WithholdingTax { get; set; } = 0;

        [NotMapped]
        public decimal TotalDeductions
        {
            get
            {
                return PHIC + HDMF + DecemberSSS + DecemberHDMF + WithholdingTax;
            }
        }

        [NotMapped]
        public decimal TotalNetPay
        {
            get
            {
                return TotalGrossPay - TotalDeductions;
            }
        }
    }
}
