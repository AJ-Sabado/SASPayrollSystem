using DomainLayer.Enums;
using DomainLayer.Models.Attendance;
using DomainLayer.Models.Employee;
using DomainLayer.Models.Leave;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Payroll
{
    public class PayrollModel : IPayrollModel
    {
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

        private uint _days = 0;
        private uint _regularOT = 0;
        private uint _nights = 0;
        private uint _nightsOT = 0;
        private decimal _latesMinutes = 0;
        private uint _totalDays = 0;

        private readonly TimeOnly DayStart = new TimeOnly(6, 0, 0);
        private readonly TimeOnly DayEnd = new TimeOnly(22, 0, 0);

        private readonly TimeOnly DayWorkShiftStart = new TimeOnly(8, 0, 0);
        private readonly TimeOnly DayWorkShiftEnd = new TimeOnly(17, 0, 0);

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateOnly PayrollDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateOnly CutOffStart { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateOnly CutOffEnd { get; set; }

        [Column(TypeName = "tinyint")]
        public bool SkipAttendancesAndLeaves { get; set; } = false;

        [Column(TypeName = "tinyint")]
        public uint Days
        {
            get
            {
                return _days;
            }
            set
            {
                _days = value;
                DaysAmount = Days * Employee.BasicSemiMonthlyRate / _totalDays;
            }
        }

        [Column(TypeName = "money")]
        public decimal DaysAmount { get; set; } = 0;

        [Column(TypeName = "tinyint")]
        public uint RegularOT
        {
            get
            {
                return _regularOT;
            }
            set
            {
                _regularOT = value;
                RegularOTAmount = Employee.BasicSemiMonthlyRate / _totalDays / 8 * 1.25m * RegularOT;
            }
        }

        [Column(TypeName = "money")]
        public decimal RegularOTAmount { get; set; } = 0;

        [Column(TypeName = "tinyint")]
        public uint Nights
        {
            get
            {
                return _nights;
            }
            set
            {
                _nights = value;
                NightsAmount = Nights * Employee.BasicSemiMonthlyRate / _totalDays;
            }
        }
        [Column(TypeName = "money")]
        public decimal NightsAmount { get; set; } = 0;

        [Column(TypeName = "tinyint")]
        public uint NightsOT
        {
            get
            {
                return _nightsOT;
            }
            set
            {
                _nightsOT = value;
                NightsOTAmount = Employee.BasicSemiMonthlyRate / _totalDays / 8 * 1.25m * 1.10m * NightsOT;
            }
        }
        [Column(TypeName = "money")]
        public decimal NightsOTAmount { get; set; } = 0;

        [Column(TypeName = "decimal")]
        public decimal LatesMinutes
        {
            get
            {
                return _latesMinutes;
            }
            set
            {
                _latesMinutes = value;
                LatesDeductionAmount = Employee.BasicSemiMonthlyRate / _totalDays / 480 * LatesMinutes;
            }
        }
        [Column(TypeName = "money")]
        public decimal LatesDeductionAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal TotalBasic { get; private set; } = 0;

        [Column(TypeName = "money")]
        public decimal TotalOT { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal SSSAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal PagIbigAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal PhilHealthAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal TotalContributions { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal TaxableIncome { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal TaxWithholdings { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal IncomeNetOfTax { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal SSSLoanAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal PagIbigLoanAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal DisposableIncome { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal CashAdvance { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal Vale { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal TotalCompanyLoans { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal AddAdjustments { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal SubtractAdjustments { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal NetPay { get; set; } = 0;


        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        public void UpdatePayroll(bool skipAttendancesAndLeaves = false)
        {
            SkipAttendancesAndLeaves = skipAttendancesAndLeaves;
            _totalDays = CountWeekDays(CutOffStart, CutOffEnd);
            if (!SkipAttendancesAndLeaves)
                ApplyAttendancesAndLeaves(Employee.Attendances, Employee.Leaves);
            TotalBasic = DaysAmount + NightsAmount - LatesDeductionAmount;
            TotalOT = RegularOTAmount + NightsOTAmount;
            CalculateContributions(Employee.BasicSemiMonthlyRate * 2);
            TotalContributions = SSSAmount + PagIbigAmount + PhilHealthAmount;
            TaxableIncome = TotalBasic + TotalOT - TotalContributions;
            TaxWithholdings = CalculateTaxWithholdings(TaxableIncome);
            IncomeNetOfTax = TaxableIncome - TaxWithholdings;
            DisposableIncome = IncomeNetOfTax - SSSLoanAmount - PagIbigLoanAmount;
            TotalCompanyLoans = CashAdvance + Vale;
            NetPay = DisposableIncome - TotalCompanyLoans + AddAdjustments - SubtractAdjustments;
        }

        private void ApplyAttendancesAndLeaves(IEnumerable<AttendanceModel> attendances, IEnumerable<LeaveModel> leaves)
        {
            uint totalDays = 0;
            uint totalNights = 0;
            decimal totalMinutesLate = 0;

            uint totalRegularOT = 0;
            uint totalNightOT = 0;

            foreach (var attendance in attendances)
            {
                if (IsDateBetween(attendance.Date, CutOffStart, CutOffEnd) && attendance.Status == FormStatus.Approved)
                {
                    if (attendance.TimeIn.IsBetween(DayStart, DayEnd))
                    {
                        totalDays++;
                        if (attendance.TotalHours > 8)
                        {
                            totalRegularOT += (attendance.TotalHours - 8);
                        }
                        if (attendance.TimeIn > DayWorkShiftStart)
                        {
                            var span = attendance.TimeIn - DayWorkShiftStart;
                            totalMinutesLate += (decimal)span.TotalMinutes;
                        }
                    }
                    else
                    {
                        totalNights++;
                        if (attendance.TotalHours > 8)
                        {
                            totalNightOT += (attendance.TotalHours - 8);
                        }
                    }
                }
            }

            foreach (var leave in leaves)
            {
                if (IsDateBetween(leave.DateOfAbsence, CutOffStart, CutOffEnd) && leave.Status == FormStatus.Approved)
                    totalDays += leave.Duration;
            }

            Days = totalDays;
            RegularOT = totalRegularOT;
            Nights = totalNights;
            NightsOT = totalNightOT;
            LatesMinutes = totalMinutesLate;
        }

        private bool IsDateBetween(DateOnly date, DateOnly start, DateOnly end)
        {
            return date >= start && date <= end;
        }

        private uint CountWeekDays(DateOnly start, DateOnly end)
        {
            uint result = 0;
            var nDays = start.AddDays(0);
            while (nDays <= end)
            {
                if (nDays.DayOfWeek != DayOfWeek.Saturday && nDays.DayOfWeek != DayOfWeek.Sunday)
                    result++;
                nDays = nDays.AddDays(1);
            }
            return result;
        }

        private void CalculateContributions(decimal basicMonthlySalary)
        {
            SSSAmount = CalculateSSSAmount(basicMonthlySalary);
            PagIbigAmount = CalculatePagIbigAmount(basicMonthlySalary);
            PhilHealthAmount = CalculatePhilHealthAmount(basicMonthlySalary);
        }

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

        private decimal CalculateTaxWithholdings(decimal taxableIncome)
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
