using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;
using DomainLayer.Enums.EmployeePayslip;
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
        public uint HolidayHours { get; set; } = 0;
        [NotMapped]
        public decimal HolidayPay
        {
            get
            {
                return AppliedHourlyRate * HolidayHours;
            }
        }

        [Column(TypeName = "smallint")]
        public uint NDOnWorkingDayHours { get; set; } = 0;
        [NotMapped]
        public decimal NightDifferentialPay
        {
            get
            {
                return NDOnWorkingDayHours * AppliedHourlyRate * AppliedNDRate;
            }
        }

        [Column(TypeName = "smallint")]
        public uint OTHoursWorkedRegular { get; set; } = 0;
        [NotMapped]
        public decimal OvertimePay
        {
            get
            {
                return OTHoursWorkedRegular * AppliedHourlyRate * AppliedOvertimeRate;
            }
        }

        [Column(TypeName = "smallint")]
        public uint PaidLeaveHours { get; set; } = 0;
        [NotMapped]
        public decimal PaidLeaves
        {
            get
            {
                return PaidLeaveHours * AppliedHourlyRate;
            }
        }

        //Bonus
        [Column(TypeName = "money")]
        public decimal Legal13thMonthPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal PerfectAttendanceBonus { get; set; } = 0;
        [NotMapped]
        public decimal Bonus
        {
            get
            {
                return Legal13thMonthPay + PerfectAttendanceBonus;
            }
        }


        //Allowance
        [Column(TypeName = "money")]
        public decimal UtilityAllowance { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal MealAllowance { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal LoadAllowance { get; set; } = 0;
        [NotMapped]
        public decimal Allowances
        {
            get
            {
                return UtilityAllowance + MealAllowance + LoadAllowance;
            }
        }

        [Column(TypeName = "money")]
        public decimal GrossPay { get; set; }

        //Deductions
        [Column(TypeName = "money")]
        public decimal WithholdingTax { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal PHIC { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal HDMF { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal DecemberSSS { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal DecemberHDMF { get; set; } = 0;

        [NotMapped]
        public decimal GovernmentContributions
        {
            get
            {
                return PHIC + HDMF + DecemberSSS + DecemberHDMF;
            }
        }

        [Column(TypeName = "money")]
        public decimal GovernmentLoans { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal CompanyLoans { get; set; } = 0;
        [NotMapped]
        public decimal LoanDeductions
        {
            get 
            {
                return GovernmentLoans + CompanyLoans;
            }
        }

        [Column(TypeName = "smallint")]
        public uint UTMinutes { get; set; } = 0;
        [NotMapped]
        public decimal UTDeductions
        {
            get
            {
                return UTMinutes * AppliedHourlyRate / 60;
            }
        }


        [Column(TypeName = "money")]
        public decimal TotalDeductions { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal NetSalary { get; set; } = 0;

        [Column(TypeName = "tinyint")]
        public EmployeePayslipStatus PayslipStatus = EmployeePayslipStatus.Pending;
    }
}
