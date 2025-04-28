using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendance;
using DomainLayer.Models.EmployeeLeave;
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
        private const decimal _minimumSSSMonthlyCompRange = 5250;
        private const decimal _maximumSSSMonthlyCompRange = 34750;
        private const decimal _minimumSSSMonthlyContribution = 250;
        private const decimal _maximumSSSMonthlyContribution = 1750;
        private const decimal _minimumPhilHealthMonthlySalaryCap = 10000;
        private const decimal _maximumPhilHealthMonthlySalaryCap = 100000;
        private const decimal _minimumPhilHealthMonthlyContribution = 500;
        private const decimal _maximumPhilHealthMonthlyContribution = 5000;
        private const decimal _currentPhilHealthMonthlyRate = 0.05m;
        private const decimal _minimumPagIbigMonthlyComp = 1500;

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
        public decimal BasicPay { get; set; } = 0;  //Absences already deducted as No work no pay is followed
        [Column(TypeName = "money")]
        public decimal BonusPay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal OvertimePay { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal NightShiftDifferentialPay { get; set; } = 0; //All ordinary night shiftts
        [Column(TypeName = "money")]
        public decimal HolidayPay { get; set; } = 0;    //Paid holidays and holidays worked
        [Column(TypeName = "money")]
        public decimal PaidLeaves { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal Allowance { get; set; } = 0;     //Value of allowance manually entered for now
        [Column(TypeName = "money")]
        public decimal GrossPay { get; set; } = 0;  //Total pay before tax, contribution, and deduction
        [Column(TypeName = "money")]
        public decimal SalaryTax { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal GovtContribution { get; set; } = 0;  //SSS, Pag Ibig, and PhilHealth as one
        [Column(TypeName = "money")]
        public decimal LoanDeduction { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal LateUTDeduction { get; set; } = 0; //Total deduction from lates and undertimes
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

        //Contribution
        

        [Column(TypeName = "money")]
        public decimal SSSContributionAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal PagIbigContributionAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal PhilHealthContributionAmount { get; set; } = 0;

        //Loan Deductions
        [Column(TypeName = "money")]
        public decimal CompanyLoansAmount { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal GovtLoansAmount { get; set; } = 0;

        //Deductions due to infractions
        public uint OrdinaryLateMinutes { get; set; } = 0;
        public uint OrdinaryUTHours { get; set; } = 0;

        //TO DO - Add other deductables like govt and company loans, allowances, etc.

        //TO DO - Add 13 Month Bonus

        /*-------------------------------------PUBLIC METHODS-------------------------------------*/
        public void CalculatePaySlip(IEnumerable<HolidayModel> regularHolidaysWithinPeriod)
        {
            var validAttendances = Employee.EmployeeAttendances
                .Where(e => IsDateBetween(e.Date, PeriodStart, PeriodEnd) && e.Status == FormStatus.Approved)
                .ToList();
            var dailyRate = Employee.BasicDailyRate;
            var hourlyRate = dailyRate / 8m;

            //TO DO - Fill in Time Calculations

            AnalyzeAttendanceLog(validAttendances);

            //TO DO - Fill in Money Calculations

            //Basic Pay
            BasicPay = OrdinaryDaysWorked * dailyRate;

            //Overtime Pay
            OvertimePay = OrdinaryOTHoursWorked * hourlyRate * _ordinaryOTPremium;

            //Night Shift Differential Pay
            var night = OrdinaryNightHoursWorked * hourlyRate * _nightDifferential;
            var nightOT = OrdinaryNightOTHoursWorked * hourlyRate * _nightDifferential * _ordinaryOTPremium;
            NightShiftDifferentialPay = night + nightOT;

            //Holiday Pay
            ApprovedHolidayNoWorkPay = CalculateApprovedHolidayNoWorkPay(regularHolidaysWithinPeriod, validAttendances);
            var reg = HolidayHoursWorked * hourlyRate * 2;
            var regOT = HolidayOTHoursWorked * hourlyRate * 2 * _holidayOTPremium;

            var nightReg = HolidayNightHoursWorked * hourlyRate * 2 * _nightDifferential;
            var nightRegOT = HolidayOTNightHoursWorked * hourlyRate * 2 * _nightDifferential * _holidayOTPremium;

            var spec = SpecialHolidayHoursWorked * hourlyRate * _specialNonWorkingOrRestDayPremium;
            var specOT = SpecialHolidayOTHoursWorked * hourlyRate * _specialNonWorkingOrRestDayPremium * _holidayOTPremium;
            
            var specNight = SpecialHolidayNightHoursWorked * hourlyRate * _specialNonWorkingOrRestDayPremium * _nightDifferential;
            var specNightOT = SpecialHolidayOTNightHoursWorked * hourlyRate * _specialNonWorkingOrRestDayPremium * _holidayOTPremium * _holidayOTPremium;

            HolidayPay = (ApprovedHolidayNoWorkPay * dailyRate) + reg + regOT + nightReg + nightRegOT + spec + specOT + specNight + specNightOT;

            //Paid Leaves
            var validLeaves = Employee.EmployeeLeaves
                .Where(e => IsDateBetween(e.DateOfAbsenceStart, PeriodStart, PeriodEnd) && e.Status == FormStatus.Approved)
                .ToList();
            ApprovedPaidLeaves = CalculatePaidLeaves(validLeaves);
            PaidLeaves = ApprovedPaidLeaves * dailyRate;

            //Allowance

            //Loan Deductions
            LoanDeduction = CompanyLoansAmount + GovtLoansAmount;

            //Deductions
            var ordinaryLate = OrdinaryLateMinutes * hourlyRate / 60;
            var ordinaryUT = OrdinaryUTHours * hourlyRate;
            LateUTDeduction = ordinaryLate + ordinaryUT;

            //Gross Pay
            GrossPay = BasicPay + NightShiftDifferentialPay + HolidayPay + PaidLeaves + Allowance;

            //Contribution
            SSSContributionAmount = CalculateSSSAmount(Employee.BasicMonthlyRate);
            PagIbigContributionAmount = CalculatePagIbigAmount(Employee.BasicMonthlyRate);
            PhilHealthContributionAmount = CalculatePhilHealthAmount(Employee.BasicMonthlyRate);
            GovtContribution = SSSContributionAmount + PagIbigContributionAmount + PhilHealthContributionAmount;
            var taxableIncome = GrossPay - GovtContribution;
            SalaryTax = CalculateSalaryTax(taxableIncome);


            //Net Pay
            NetPay = GrossPay - GovtContribution - LateUTDeduction;
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

            HolidayHoursWorked = 0;
            HolidayOTHoursWorked = 0;

            HolidayNightHoursWorked = 0;
            HolidayOTNightHoursWorked = 0;

            SpecialHolidayHoursWorked = 0;
            SpecialHolidayOTHoursWorked = 0;

            SpecialHolidayNightHoursWorked = 0;
            SpecialHolidayOTNightHoursWorked = 0;

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
                    if (!attendance.IsNight)
                    {
                        HolidayHoursWorked += attendance.PayableHours;
                        HolidayOTHoursWorked += attendance.OTHours;
                    }
                    else
                    {
                        HolidayNightHoursWorked += attendance.PayableHours;
                        HolidayOTNightHoursWorked += attendance.OTHours;
                    }  
                }
                else if (attendance.HolidayStatus == HolidayType.SpecialNonWorking)
                {
                    //TO DO - Fill in special non working time readings
                    if (!attendance.IsNight)
                    {
                        SpecialHolidayHoursWorked += attendance.PayableHours;
                        SpecialHolidayOTHoursWorked += attendance.OTHours;
                    }
                    else
                    {
                        SpecialHolidayNightHoursWorked += attendance.PayableHours;
                        SpecialHolidayOTNightHoursWorked += attendance.OTHours;
                    }
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
                    var lastAttendance = validAttendances.FirstOrDefault(e => e.Date == holiday.Date.AddDays(-2));

                    if (lastAttendance != null)
                        result++;
                }
                else if (holiday.Date.DayOfWeek == DayOfWeek.Monday)
                {
                    var lastAttendance = validAttendances.FirstOrDefault(e => e.Date == holiday.Date.AddDays(-3));

                    var workOnHoliday = validAttendances
                        .Where(e => e.Date == holiday.Date)
                        .FirstOrDefault();

                    if (lastAttendance != null && workOnHoliday == null)
                        result++;
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
        private uint CalculatePaidLeaves(IEnumerable<EmployeeLeaveModel> validLeaves)
        {
            uint result = 0;
            foreach (var leave in validLeaves)
            {
                result += leave.Duration;
                var nday = leave.DateOfAbsenceEnd;
                while (nday > PeriodEnd)
                {
                    result -= 1;
                    nday = nday.AddDays(-1);
                }
            }
            return result;
        }

        //TO DO - Create methods to calculate contributions
        private decimal CalculateSSSAmount(decimal basicMonthlySalary)
        {
            decimal finalMonthlyAmount = 0;
            if (basicMonthlySalary < _minimumSSSMonthlyCompRange)
                finalMonthlyAmount = _minimumSSSMonthlyContribution;
            else if (basicMonthlySalary >= _maximumSSSMonthlyCompRange)
                finalMonthlyAmount = _maximumSSSMonthlyContribution;
            else
            {
                decimal baseCompensation = basicMonthlySalary - _minimumSSSMonthlyCompRange;
                finalMonthlyAmount = 25 * (Math.Floor(baseCompensation / 500) + 1) + _minimumSSSMonthlyContribution;
            }
            return finalMonthlyAmount / 2;
        }

        private decimal CalculatePagIbigAmount(decimal basicMonthlySalary)
        {
            if (basicMonthlySalary <= _minimumPagIbigMonthlyComp)
                return basicMonthlySalary * 0.03m / 2;
            return basicMonthlySalary * 0.04m / 2;
        }
        private decimal CalculatePhilHealthAmount(decimal basicMonthlySalary)
        {
            if (basicMonthlySalary <= _minimumPhilHealthMonthlySalaryCap)
                return _minimumPhilHealthMonthlyContribution / 4;
            else if (basicMonthlySalary >= _maximumPhilHealthMonthlySalaryCap)
                return _maximumPhilHealthMonthlyContribution / 4;
            return basicMonthlySalary * _currentPhilHealthMonthlyRate / 4;
        }
        private decimal CalculateSalaryTax(decimal taxableIncome)
        {
            decimal finalAmount = 0;
            if (taxableIncome <= 10417)
                finalAmount = 0;
            else if (taxableIncome > 10417 && taxableIncome <= 16667)
                finalAmount = 0.15m * (taxableIncome - 10416.67m);
            else if (taxableIncome > 16667 && taxableIncome <= 33333)
                finalAmount = 0.20m * (taxableIncome - 16666.67m) + 937.5m;
            else if (taxableIncome > 33333 && taxableIncome <= 83333)
                finalAmount = 0.25m * (taxableIncome - 33333.33m) + 4270.83m;
            else if (taxableIncome > 83333 && taxableIncome <= 333333)
                finalAmount = 0.3m * (taxableIncome - 83333.33m) + 16770.83m;
            else
                finalAmount = 0.35m * (taxableIncome - 333333.33m) + 91770.83m;

            return finalAmount;
        }
    }
}
