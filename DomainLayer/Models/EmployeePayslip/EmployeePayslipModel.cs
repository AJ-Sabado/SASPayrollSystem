using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Column(TypeName = "money")]
        public decimal AppliedHourlyRate { get; set; } = 0;

        public decimal AppliedNDRate { get; set; } = 0.1m;
        public decimal AppliedLegalHolidayRate { get; set; } = 1.3m;
        public decimal AppliedOvertimeRate { get; set; } = 1.25m;


        //Gross Salary
        public decimal HoursWorkedRegular { get; set; } = 0;
        [NotMapped]
        public decimal BasicPay
        {
            get
            {
                return Math.Floor(AppliedHourlyRate * HoursWorkedRegular * 100) / 100;
            }
        }

        public decimal HolidayHours { get; set; } = 0;
        [NotMapped]
        public decimal HolidayPay
        {
            get
            {
                return Math.Floor(AppliedHourlyRate * HolidayHours * 100) / 100;
            }
        }

        public decimal NDOnWorkingDayHours { get; set; } = 0;
        [NotMapped]
        public decimal NightDifferentialPay
        {
            get
            {
                return Math.Floor(NDOnWorkingDayHours * AppliedHourlyRate * AppliedNDRate * 100) / 100;
            }
        }

        public decimal OTHoursWorkedRegular { get; set; } = 0;
        [NotMapped]
        public decimal OvertimePay
        {
            get
            {
                return Math.Floor(OTHoursWorkedRegular * AppliedHourlyRate * AppliedOvertimeRate * 100) / 100;
            }
        }

        public decimal PaidLeaveHours { get; set; } = 0;
        [NotMapped]
        public decimal PaidLeaves
        {
            get
            {
                return Math.Floor(PaidLeaveHours * AppliedHourlyRate * 100) / 100;
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
        public decimal MealAllowance { get; set; } = 750;
        [Column(TypeName = "money")]
        public decimal LoadAllowance { get; set; } = 300;
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

        public decimal UTMinutes { get; set; } = 0;
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
