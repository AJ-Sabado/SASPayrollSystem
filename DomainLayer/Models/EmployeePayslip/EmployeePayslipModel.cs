using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendance;
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

        /*--------------------CALCULATED MONEY VALUES--------------------*/
        [Column(TypeName = "money")]
        public decimal BasicPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal NightShiftDifferentialPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal OvertimePay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal HolidayPay { get; set; } = 0;

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

        /*--------------------CALCULATED TIME VALUES--------------------*/
        public uint TotalHolidays { get; set; } = 0;

        //Basic Pay
        public uint OrdinaryHoursWorked { get; set; } = 0;

        //Night Shift Differential Pay
        public uint OrdinaryNightHoursWorked { get; set; } = 0;
        public uint OrdinaryNightOTHoursWorked { get; set; } = 0;

        //OvertimePay
        public uint OrdinaryOTMinutes { get; set; } = 0;
        public uint OrdinaryNightOTMinutes { get; set; } = 0;

        //Holiday Pay
        public uint HolidayHoursWorked { get; set; } = 0;
        public uint HolidayOTHoursWorked { get; set; } = 0;
        public uint HolidayNightHoursWorked { get; set; } = 0;
        public uint HolidayOTNightHoursWorked { get; set; } = 0;
        public uint SpecialHolidayHoursWorked { get; set; } = 0;
        public uint SpecialHolidayOTHoursWorked { get; set; } = 0;
        public uint SpecialHolidayNightHoursWorked { get; set; } = 0;
        public uint SpecialHolidayOTNightHoursWorked { get; set; } = 0;

        //Paid Leaves
        public uint TotalApprovedLeaves { get; set; } = 0;

        //Deductions due to infractions
        public uint OridinaryLateMinutes { get; set; } = 0;
        public uint OrdinaryUTMinutes { get; set; } = 0;

        /*--------------------PUBLIC METHODS--------------------*/
        public void CalculatePaySlip(uint totalRegularHolidaysWithinPeriod)
        {
            TotalHolidays = totalRegularHolidaysWithinPeriod;
            var validAttendances = Employee.EmployeeAttendances
                .Where(e => IsDateBetween(e.Date, PeriodStart, PeriodEnd) && e.Status == FormStatus.Approved)
                .ToList();
            var dailyRate = Employee.BasicMonthlyRate * 12 / _annualWorkDays;
            var hourlyRate = dailyRate / _workHoursPerDay;

            //TO DO - Fill in Time Calculations

            //TO DO - Fill in Money Calculations
        }


        private bool IsDateBetween(DateOnly date, DateOnly start, DateOnly end)
        {
            return date >= start && date <= end;
        }
        private void AnalyzeAttendanceLog(IEnumerable<EmployeeAttendanceModel> validAttendances)
        {
            foreach (var attendance in validAttendances)
            {
                if (attendance.HolidayStatus == HolidayType.No || attendance.HolidayStatus == HolidayType.SpecialWorking)
                {
                    //TO DO - Fill in ordinary/special working time readings
                }
                else if (attendance.HolidayStatus == HolidayType.Regular)
                {
                    //TO DO - Fill in regular holiday time readings
                }
                else if (attendance.HolidayStatus == HolidayType.SpecialNonWorking)
                {
                    //TO DO - Fill in special non working time readings
                }
            }
        }
    }
}
