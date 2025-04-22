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
        [Key]
        public Guid EmployeePayslipId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [Column(TypeName = "date")]
        public required DateOnly PeriodStart { get; set; }
        [Column(TypeName = "date")]
        public required DateOnly PeriodEnd { get; set; }

        //CALCULATED TIME VALUES
        [Column(TypeName = "tinyint")]
        public uint TotalRegularWorkHoursNeeded { get; private set; } = 0;
        [Column(TypeName = "tinyint")]
        public uint RegularHoursWorked { get; private set; } = 0;
        
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

        //RUN THIS BEFORE SAVING TO DATABASE
        public void CalculatePaySlip(IEnumerable<HolidayModel> validHolidays)
        {
            TotalRegularWorkHoursNeeded = CalculateTotalRegularWorkHoursNeeded(PeriodStart, PeriodEnd, validHolidays);
            
            NetPay = GrossPay - Deduction;
        }

        //TIME CALCULATION METHODS
        private uint CalculateTotalRegularWorkHoursNeeded(DateOnly periodStart, DateOnly periodEnd, IEnumerable<HolidayModel> validHolidays)
        {
            uint totalWorkDays = 0;
            var nDays = periodStart.AddDays(0);
            while (nDays <= periodEnd)
            {
                if (nDays.DayOfWeek != DayOfWeek.Saturday && nDays.DayOfWeek != DayOfWeek.Sunday)
                {
                    var holiday = validHolidays.Where(h => h.Date == nDays).FirstOrDefault();
                    if (holiday != null)
                    {
                        if (holiday.Type == HolidayType.SpecialWorking)
                            totalWorkDays++;
                    }
                    else
                        totalWorkDays++;
                }
                nDays = nDays.AddDays(1);
            }
            return totalWorkDays * 8;
        }
    }
}
