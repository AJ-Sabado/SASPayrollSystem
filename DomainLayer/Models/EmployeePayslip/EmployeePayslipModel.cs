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
        public decimal BasicPay { get; set; }
        [Column(TypeName = "money")]
        public decimal OverTimePay { get; set; }
        [Column(TypeName = "money")]
        public decimal Allowance { get; set; }
        [Column(TypeName = "money")]
        public decimal GrossPay { get; set; }
        [Column(TypeName = "money")]
        public decimal Deduction { get; set; }
        [Column(TypeName = "money")]
        public decimal NetPay { get; set; }

        //VALUES USED FOR CALCULATING VALUES ABOVE
        [Column(TypeName = "tinyint")]
        public uint TotalHoursWorked { get; private set; } = 0;
        private uint FullRegularDaysWorked = 0;
        private uint OTHours = 0;
        private uint UTHours = 0;
        private uint Absences = 0;


        //RUN THIS BEFORE SAVING TO DATABASE
        public void CalculatePaySlip(IEnumerable<HolidayModel> validHolidays)
        {
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

        }
    }
}
