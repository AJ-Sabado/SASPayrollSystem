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
        /*--------------------------CONSTANTS--------------------------*/
        private const uint _annualWorkDays = 243;
        private const uint _workHoursPerDay = 8;
        private const decimal _nightDifferential = 1.1m;
        private const decimal _specialNonWorkingOrRestDayPremium = 1.3m;
        private const decimal _ordinaryOTPremium = 1.25m;
        private const decimal _holidayOTPremium = 1.3m;

        [Key]
        public Guid EmployeePayslipId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [Column(TypeName = "date")]
        public required DateOnly PeriodStart { get; set; }
        [Column(TypeName = "date")]
        public required DateOnly PeriodEnd { get; set; }

        /*--------------------------------CALCULATED MONEY VALUES--------------------------------*/
        //Note: These values are displayed in the UI payslip. They are broken down further in printed payslip.
        [Column(TypeName = "money")]
        public decimal BasicPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal OvertimePay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal NightShiftDifferentialPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal HolidayPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal PaidLeaves { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal Allowance { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal GrossPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal Contribution { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal Deduction { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal NetPay { get; set; } = 0;

        /*--------------------CALCULATED TIME VALUES--------------------*/
        //Basic Pay
        public uint OrdinaryDaysWorked { get; set; } = 0;

        //Night Shift Differential Pay
        public uint OrdinaryNightHoursWorked { get; set; } = 0;
        public uint OrdinaryNightOTHoursWorked { get; set; } = 0;

        //OvertimePay
        public uint OrdinaryOTHoursWorked { get; set; } = 0;

        //Holiday Pay
        public uint ApprovedHolidayNoWorkPay { get; set; } = 0;
        public uint HolidayHoursWorked { get; set; } = 0;
        public uint HolidayOTHoursWorked { get; set; } = 0;
        public uint HolidayNightHoursWorked { get; set; } = 0;
        public uint HolidayOTNightHoursWorked { get; set; } = 0;
        public uint SpecialHolidayHoursWorked { get; set; } = 0;
        public uint SpecialHolidayOTHoursWorked { get; set; } = 0;
        public uint SpecialHolidayNightHoursWorked { get; set; } = 0;
        public uint SpecialHolidayOTNightHoursWorked { get; set; } = 0;

        //Paid Leaves
        public uint ApprovedPaidLeaves { get; set; } = 0;

        //Deductions due to infractions
        public uint OrdinaryLateMinutes { get; set; } = 0;
        public uint OrdinaryUTHours { get; set; } = 0;

        /*-------------------------------------PUBLIC METHODS-------------------------------------*/
        public void CalculatePaySlip(IEnumerable<HolidayModel> regularHolidaysWithinPeriod)
        {
            var validAttendances = Employee.EmployeeAttendances
                .Where(e => IsDateBetween(e.Date, PeriodStart, PeriodEnd) && e.Status == FormStatus.Approved)
                .ToList();
            var dailyRate = Employee.BasicMonthlyRate * 12 / _annualWorkDays;
            var hourlyRate = dailyRate / _workHoursPerDay;

            //TO DO - Fill in Time Calculations

            AnalyzeAttendanceLog(validAttendances);
            ApprovedHolidayNoWorkPay = CalculateApprovedHolidayNoWorkPay(regularHolidaysWithinPeriod, validAttendances);

            //TO DO - Fill in Money Calculations

            //Basic Pay
            BasicPay = OrdinaryDaysWorked * dailyRate;

            //Overtime Pay
            OvertimePay = OrdinaryOTHoursWorked * hourlyRate * _ordinaryOTPremium;

            //Night Shift Differential Pay
            var night = OrdinaryNightHoursWorked * hourlyRate * _nightDifferential;
            var nightOT = OrdinaryNightOTHoursWorked * hourlyRate * _nightDifferential * _ordinaryOTPremium;
            NightShiftDifferentialPay = night + nightOT;

            //Holiday Pay *madugo to hahahahahahah

            //Paid Leaves

            //Allowance

            //Contribution

            //Deductions
            var ordinaryLate = OrdinaryLateMinutes * hourlyRate / 60;
            var ordinaryUT = OrdinaryUTHours * hourlyRate;
            Deduction = ordinaryLate + ordinaryUT;

            //Gross Pay
            GrossPay = BasicPay + NightShiftDifferentialPay + HolidayPay + PaidLeaves + Allowance;

            //Net Pay
            NetPay = GrossPay - Contribution - Deduction;
        }

        /*---------------------------------INTERNAL METHODS-----------------s----------------*/
        private bool IsDateBetween(DateOnly date, DateOnly start, DateOnly end)
        {
            return date >= start && date <= end;
        }
        private void AnalyzeAttendanceLog(IEnumerable<EmployeeAttendanceModel> validAttendances)
        {
            //TO DO - Reset all time values to zero
            OrdinaryDaysWorked = 0;
            OrdinaryLateMinutes = 0;
            OrdinaryOTHoursWorked = 0;
            OrdinaryUTHours = 0;

            OrdinaryNightHoursWorked = 0;
            OrdinaryNightOTHoursWorked = 0;

            foreach (var attendance in validAttendances)
            {
                if (attendance.HolidayStatus != HolidayType.Regular && attendance.HolidayStatus != HolidayType.SpecialNonWorking)
                {
                    //TO DO - Fill in ordinary/special working time readings
                    if (!attendance.IsNight)
                    {
                        //This goes to Basic Pay.
                        OrdinaryDaysWorked++;

                        //This goes to Deductions
                        OrdinaryLateMinutes += attendance.LateMinutes;
                        OrdinaryOTHoursWorked += attendance.OTHours;
                        OrdinaryUTHours += attendance.UTHours;
                    }
                    else
                    {
                        //This goes to Night Shift Differential Pay
                        OrdinaryNightHoursWorked += attendance.PayableHours;
                        OrdinaryNightOTHoursWorked += attendance.OTHours;
                    }
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

        private uint CalculateApprovedHolidayNoWorkPay(IEnumerable<HolidayModel> validRegularHolidays, IEnumerable<EmployeeAttendanceModel> validAttendances)
        {
            uint result = 0;
            //TO DO - Fill in holiday analysis
            foreach (var holiday in validRegularHolidays)
            {
                if (holiday.Date.DayOfWeek == DayOfWeek.Sunday)
                {

                }
                else if (holiday.Date.DayOfWeek == DayOfWeek.Monday)
                {

                }
                else
                {
                    //If holiday falls within Tuesday - Saturday
                    var dateBefore = validAttendances
                        .Where(e => e.Date == holiday.Date.AddDays(-1))
                        .FirstOrDefault();
                    var workOnHoliday = validAttendances
                        .Where(e => e.Date == holiday.Date)
                        .FirstOrDefault();

                    if (dateBefore != null && workOnHoliday == null)
                        result++;
                }
            }
            return result;
        }

        //TO DO - Create method for analyzing leaves to count all paid leaves
    }
}
