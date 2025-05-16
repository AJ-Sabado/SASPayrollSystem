using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeePayslip
{
    public class EmployeePayslipModel
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
        [Column(TypeName = "date")]
        public DateOnly PayDate { get; set; }

        //Historical Data
        public decimal AppliedHourlyRate { get; set; } = 0;
        public decimal AppliedNDRate { get; set; } = 0.1m;
        public decimal AppliedLegalHolidayRate { get; set; } = 1.3m;
        public decimal AppliedOvertimeRate { get; set; } = 1.25m;


        //Gross Salary
        [Column(TypeName = "smallint")]
        public uint HoursWorkedRegular { get; set; } = 0;
        [NotMapped]
        public decimal BasicPay 
        {
            get
            {
                return AppliedHourlyRate * HoursWorkedRegular;
            }
        }


        [Column(TypeName = "smallint")]
        public uint LegalHolidaysHours { get; set; } = 0;
        [NotMapped]
        public decimal LegalHolidaysPay
        {
            get
            {
                return LegalHolidaysHours * LegalHolidaysHours;
            }
        }

        [Column(TypeName = "smallint")]
        public uint NDOnWorkingDayHours { get; set; } = 0;
        [NotMapped]
        public decimal NDOnWorkingDayPay
        {
            get
            {
                return NDOnWorkingDayHours * AppliedHourlyRate * AppliedNDRate;
            }
        }

        [Column(TypeName = "smallint")]
        public uint OTHoursWorkedRegular { get; set; } = 0;
        [NotMapped]
        public decimal OTHoursWorkedRegularPay
        {
            get
            {
                return OTHoursWorkedRegular * AppliedHourlyRate * AppliedOvertimeRate;
            }
        }


        //Allowance
        [Column(TypeName = "money")]
        public decimal UtilityAllowance { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal MealAllowance { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal LoadAllowance { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal TotalGrossPay { get; set; }

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

        [Column(TypeName = "money")]
        public decimal TotalDeductions { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal NetPay { get; set; } = 0;

    }
}
