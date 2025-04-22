using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendance;
using DomainLayer.Models.Holiday;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeePayslip
{
    public class EmployeePayslipModel
    {
        private const uint _annualWorkDays = 243;
        private const uint _workHoursPerDay = 8;
        private const decimal _nightDifferential = 0.1m;
        private const decimal _specialNonWorkingOrRestDayPremium = 0.3m;
        private const decimal _regularOvertimePremium = 0.25m;
        private const decimal _holidayOvertimePremium = 0.3m;

        [Key]
        public Guid EmployeePayslipId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [Column(TypeName = "date")]
        public required DateOnly PeriodStart { get; set; }
        [Column(TypeName = "date")]
        public required DateOnly PeriodEnd { get; set; }

        //CALCULATED MONEY VALUES TO BE DISPLAYED
        [Column(TypeName = "money")]
        public decimal BasicPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal OvertimePay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal HolidayPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal NightShiftDifferentialPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal PaidLeaves { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal Allowance { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal GrossPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal Deduction { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal NetPay { get; set; } = 0;

        //CALCULATED TIME VALUES
        public uint TotalHolidays { get; set; } = 0;

        public uint RegularDaysWorked { get; set; } = 0;
        public uint RegularLateMinutes { get; set; } = 0;
        public uint RegularOTMinutes { get; set; } = 0;
        public uint RegularUTMinutes { get; set; } = 0;

        public uint HolidayHoursWorked { get; set; } = 0;


        //RUN THIS BEFORE SAVING TO DATABASE
        public void CalculatePaySlip(uint totalRegularHolidaysWithinPeriod)
        {
            TotalHolidays = totalRegularHolidaysWithinPeriod;
            var validAttendances = Employee.EmployeeAttendances
                .Where(e => IsDateBetween(e.Date, PeriodStart, PeriodEnd) && e.Status == FormStatus.Approved)
                .ToList();
            var dailyRate = Employee.BasicMonthlyRate * 12 / _annualWorkDays;
            var hourlyRate = dailyRate / _workHoursPerDay;
            
        }


        private bool IsDateBetween(DateOnly date, DateOnly start, DateOnly end)
        {
            return date >= start && date <= end;
        }
        private void AnalyzeAttendanceLog(IEnumerable<EmployeeAttendanceModel> validAttendances)
        {
            foreach (var attendance in validAttendances)
            {
                if (attendance.HolidayStatus == HolidayType.No)
                {
                    RegularDaysWorked++;
                    RegularLateMinutes += attendance.LateMinutes;
                    RegularOTMinutes += attendance.OTMinutes;
                    RegularUTMinutes += attendance.UTMinutes;
                }
            }
        }
    }
}
