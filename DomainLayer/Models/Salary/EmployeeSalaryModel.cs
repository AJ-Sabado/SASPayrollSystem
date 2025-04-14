using DomainLayer.Enums;
using DomainLayer.Models.Attendance;
using DomainLayer.Models.Employee;
using DomainLayer.Models.Leave;
using DomainLayer.Models.Payroll;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Salary
{
    public class EmployeeSalaryModel : IEmployeeSalaryModel
    {
        private uint _days = 0;
        private uint _regularOT = 0;
        private uint _nights = 0;
        private uint _nightsOT = 0;
        private decimal _latesMinutes = 0;

        private readonly TimeOnly DayStart = new TimeOnly(6, 0, 0);
        private readonly TimeOnly DayEnd = new TimeOnly(22, 0, 0);

        public EmployeeSalaryModel()
        {

        }

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(PayrollId))]
        public required Guid PayrollId { get; set; }
        public required PayrollModel Payroll { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        //Basic
        //Normal Days and Nights Worked
        [Column(TypeName = "tinyint")]
        public uint DaysWorked
        {
            get
            {
                return _days;
            }
            set
            {
                _days = value;
                DaysAmount = GetAmount(DaysWorked, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays);
            }
        }
        [Column(TypeName = "money")]
        public decimal DaysAmount { get; set; } = 0;
        [Column(TypeName = "tinyint")]
        public uint NightsWorked
        {
            get
            {
                return _nights;
            }
            set
            {
                _nights = value;
                NightsAmount = GetAmount(NightsWorked, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, false);
            }
        }
        [Column(TypeName = "money")]
        public decimal NightsAmount { get; set; } = 0;

        //Regular Holidays Days and Nights Worked
        private uint _regularHolidaysWorked = 0;
        [Column(TypeName = "tinyint")]
        public uint RegularHolidaysWorked
        {
            get
            {
                return _regularHolidaysWorked;
            }
            set
            {
                _regularHolidaysWorked = value;
                RegularHolidaysAmount
                    = GetAmount(RegularHolidaysWorked, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, true, HolidayType.Regular);
            }
        }
        [Column(TypeName = "money")]
        public decimal RegularHolidaysAmount { get; set; } = 0;

        private uint _regularHolidayNightsWorked = 0;
        [Column(TypeName = "tinyint")]
        public uint RegularHolidayNightsWorked
        {
            get
            {
                return _regularHolidayNightsWorked;
            }
            set
            {
                _regularHolidayNightsWorked = value;
                RegularHolidayNightsAmount
                    = GetAmount(RegularHolidayNightsWorked, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, false, HolidayType.Regular);
            }
        }
        [Column(TypeName = "money")]
        public decimal RegularHolidayNightsAmount { get; set; } = 0;

        //Special Non Working Holidays Worked Days and Nights
        private uint _specialDays = 0;
        [Column(TypeName = "tinyint")]
        public uint SpecialHolidaysWorked
        {
            get
            {
                return _specialDays;
            }
            set
            {
                _specialDays = value;
                SpecialHolidaysAmount
                    = GetAmount(SpecialHolidaysWorked, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, true, HolidayType.SpecialNonWorking);
            }
        }
        [Column(TypeName = "money")]
        public decimal SpecialHolidaysAmount { get; set; } = 0;
        private uint _specialNights = 0;
        [Column(TypeName = "tinyint")]
        public uint SpecialHolidayNightsWorked
        {
            get
            {
                return _specialNights;
            }
            set
            {
                _specialNights = value;
                SpecialHolidayNightsAmount
                    = GetAmount(SpecialHolidayNightsWorked, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, false, HolidayType.SpecialNonWorking);
            }
        }
        [Column(TypeName = "money")]
        public decimal SpecialHolidayNightsAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalBasic { get; private set; } = 0;

        //OTs
        //Working Day OTs
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
                RegularOTAmount = GetOTAmount(RegularOT, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays);
            }
        }
        [Column(TypeName = "money")]
        public decimal RegularOTAmount { get; set; } = 0;

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
                NightsOTAmount = GetOTAmount(NightsOT, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, false);
            }
        }
        [Column(TypeName = "money")]
        public decimal NightsOTAmount { get; set; } = 0;
        //Regular Holiday OTs
        private uint _regularHolidayOT = 0;
        [Column(TypeName = "tinyint")]
        public uint RegularHolidayOT
        {
            get
            {
                return _regularHolidayOT;
            }
            set
            {
                _regularHolidayOT = value;
                RegularHolidayOTAmount = GetOTAmount(RegularHolidayOT, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, true, HolidayType.Regular);
            }
        }
        [Column(TypeName = "money")]
        public decimal RegularHolidayOTAmount { get; set; } = 0;
        private uint _regularHolidayNightOT = 0;
        [Column(TypeName = "tinyint")]
        public uint RegularHolidayNightOT
        {
            get
            {
                return _regularHolidayNightOT;
            }
            set
            {
                _regularHolidayNightOT = value;
                RegularHolidayOTNightAmount = GetOTAmount(RegularHolidayOT, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, false, HolidayType.Regular);
            }
        }
        [Column(TypeName = "money")]
        public decimal RegularHolidayOTNightAmount { get; set; } = 0;
        //Special Holiday OTs
        private uint _specialHolidayOT = 0;
        [Column(TypeName = "tinyint")]
        public uint SpecialHolidayOT
        {
            get
            {
                return _specialHolidayOT;
            }
            set
            {
                _specialHolidayOT = value;
                SpecialHolidayOTAmount = GetOTAmount(RegularHolidayOT, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, true, HolidayType.SpecialNonWorking);
            }
        }
        [Column(TypeName = "money")]
        public decimal SpecialHolidayOTAmount { get; set; } = 0;
        private uint _specialHolidayNightOT = 0;
        [Column(TypeName = "tinyint")]
        public uint SpecialHolidayNightOT
        {
            get
            {
                return _specialHolidayNightOT;
            }
            set
            {
                _specialHolidayNightOT = value;
                SpecialHolidayNightOTAmount = GetOTAmount(RegularHolidayOT, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays, false, HolidayType.SpecialNonWorking);
            }
        }
        [Column(TypeName = "money")]
        public decimal SpecialHolidayNightOTAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal TotalOT { get; set; } = 0;

        //Deductions
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
                LatesDeductionAmount = GetLatesDeductionAmount(LatesMinutes, Employee.BasicSemiMonthlyRate, Payroll.TotalWorkingDays);
            }
        }

        [Column(TypeName = "money")]
        public decimal LatesDeductionAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal AllowancesAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal BonusesAmount { get; set; } = 0;

        //Income Tax
        [Column(TypeName = "money")]
        public decimal TaxableIncome { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal TaxWithholdings { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal IncomeNetOfTax { get; set; } = 0;

        //Loans
        [Column(TypeName = "money")]
        public decimal SSSLoanAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal PagIbigLoanAmount { get; set; } = 0;


        [Column(TypeName = "money")]
        public decimal DisposableIncome { get; set; } = 0;


        //Company Loans
        [Column(TypeName = "money")]
        public decimal CashAdvance { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal Vale { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal TotalCompanyLoans { get; set; } = 0;

        //Adjustments
        [Column(TypeName = "money")]
        public decimal AddAdjustments { get; set; } = 0;
        [Column(TypeName = "money")]
        public decimal SubtractAdjustments { get; set; } = 0;


        [Column(TypeName = "money")]
        public decimal NetPay { get; set; } = 0;


        //Atomic Operation
        public void UpdateSalary(bool skipAttendances = false, bool skipLeaves = false)
        {
            if (!skipAttendances)
                ApplyAttendances(Employee.Attendances);
            if (!skipLeaves)
                ApplyLeaves(Employee.Leaves);
            TotalBasic = DaysAmount + NightsAmount + RegularHolidaysAmount + RegularHolidayNightsAmount
                + SpecialHolidaysAmount + SpecialHolidayNightsAmount;
            TotalOT = RegularOTAmount + NightsOTAmount;
            TaxableIncome = TotalBasic + TotalOT - Employee.Contribution.TotalContributions;
            TaxWithholdings = CalculateTaxWithholdings(TaxableIncome);
            IncomeNetOfTax = TaxableIncome - TaxWithholdings;
            DisposableIncome = IncomeNetOfTax - SSSLoanAmount - PagIbigLoanAmount;
            TotalCompanyLoans = CashAdvance + Vale;
            NetPay = DisposableIncome - TotalCompanyLoans + AddAdjustments - SubtractAdjustments;
        }

        //Money Operations
        //Basics
        private decimal GetAmount(uint presents, decimal basicSemiMonthlyRate, uint totalDays, bool day = true, HolidayType holidayType = HolidayType.Not)
        {
            var normal = presents * basicSemiMonthlyRate / totalDays;
            if (!day)   //Night
                normal = normal * 1.1m;
            if (holidayType == HolidayType.Regular) //Regular Holiday
                normal = normal * 2;
            if (holidayType == HolidayType.SpecialNonWorking)  //Special NonWorking
                normal = normal * 1.3m;
            return normal;
        }

        //OTs
        private decimal GetOTAmount(uint OT, decimal basicSemiMonthlyRate, uint totalDays, bool day = true, HolidayType holidayType = HolidayType.Not)
        {
            var normal = OT * basicSemiMonthlyRate / totalDays / 8 * 1.3m;
            if (!day)
                normal = normal * 1.1m;
            if (holidayType == HolidayType.Regular)
                normal = normal * 2;
            if (holidayType == HolidayType.SpecialNonWorking)
                normal = normal * 1.3m;
            return normal;
        }

        //Deductions
        private decimal GetLatesDeductionAmount(decimal latesMinutes, decimal basicSemiMonthly, uint totalDays)
        {
            return latesMinutes * basicSemiMonthly / totalDays / 480;
        }

        //Contributions
        //private void CalculateContributions(decimal basicMonthlySalary)
        //{
        //    SSSAmount = CalculateSSSAmount(basicMonthlySalary);
        //    PagIbigAmount = CalculatePagIbigAmount(basicMonthlySalary);
        //    PhilHealthAmount = CalculatePhilHealthAmount(basicMonthlySalary);
        //}
        //private decimal CalculateSSSAmount(decimal basicMonthlySalary)
        //{
        //    decimal finalMonthlyAmount = 0;
        //    if (basicMonthlySalary < _minimumSSSMonthlyCompRange)
        //        finalMonthlyAmount = _minimumSSSMonthlyContribution;
        //    else if (basicMonthlySalary >= _maximumSSSMonthlyCompRange)
        //        finalMonthlyAmount = _maximumSSSMonthlyContribution;
        //    else
        //    {
        //        decimal baseCompensation = basicMonthlySalary - _minimumSSSMonthlyCompRange;
        //        finalMonthlyAmount = 25 * (Math.Floor(baseCompensation / 500) + 1) + _minimumSSSMonthlyContribution;
        //    }
        //    return finalMonthlyAmount / 2;
        //}
        //private decimal CalculatePagIbigAmount(decimal basicMonthlySalary)
        //{
        //    if (basicMonthlySalary <= _minimumPagIbigMonthlyComp)
        //        return basicMonthlySalary * 0.03m / 2;
        //    return basicMonthlySalary * 0.04m / 2;
        //}
        //private decimal CalculatePhilHealthAmount(decimal basicMonthlySalary)
        //{
        //    if (basicMonthlySalary <= _minimumPhilHealthMonthlySalaryCap)
        //        return _minimumPhilHealthMonthlyContribution / 4;
        //    else if (basicMonthlySalary >= _maximumPhilHealthMonthlySalaryCap)
        //        return _maximumPhilHealthMonthlyContribution / 4;
        //    return basicMonthlySalary * _currentPhilHealthMonthlyRate / 4;
        //}
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


        //private void ApplyAttendancesAndLeaves(IEnumerable<AttendanceModel> attendances, IEnumerable<LeaveModel> leaves)
        //{
        //    uint totalDays = 0;
        //    uint totalNights = 0;
        //    decimal totalMinutesLate = 0;

        //    uint totalRegularOT = 0;
        //    uint totalNightOT = 0;

        //    foreach (var attendance in attendances)
        //    {
        //        if (IsDateBetween(attendance.Date, Payroll.CutOffStart, Payroll.CutOffEnd) && attendance.Status == FormStatus.Approved)
        //        {
        //            if (attendance.TimeIn.IsBetween(DayStart, DayEnd))
        //            {
        //                totalDays++;
        //                if (attendance.TotalHours > 8)
        //                {
        //                    totalRegularOT += (attendance.TotalHours - 8);
        //                }
        //                if (attendance.TimeIn > DayWorkShiftStart)
        //                {
        //                    var span = attendance.TimeIn - DayWorkShiftStart;
        //                    totalMinutesLate += (decimal)span.TotalMinutes;
        //                }
        //            }
        //            else
        //            {
        //                totalNights++;
        //                if (attendance.TotalHours > 8)
        //                {
        //                    totalNightOT += (attendance.TotalHours - 8);
        //                }
        //            }
        //        }
        //    }

        //    foreach (var leave in leaves)
        //    {
        //        if (IsDateBetween(leave.DateOfAbsence, Payroll.CutOffStart, Payroll.CutOffEnd) && leave.Status == FormStatus.Approved)
        //            totalDays += leave.Duration;
        //    }

        //    Days = totalDays;
        //    RegularOT = totalRegularOT;
        //    Nights = totalNights;
        //    NightsOT = totalNightOT;
        //    LatesMinutes = totalMinutesLate;
        //}

        //Time Operations
        //private void ApplyAttendances(IEnumerable<AttendanceModel> attendances) 
        //{
        //    uint totalWorkingDays = 0;
        //    uint totalWorkingNights = 0;

        //    uint totalRegularHolidayDays = 0;
        //    uint totalRegularHolidayNights = 0;

        //    uint totalSpecialDays = 0;
        //    uint totalSpecialNights = 0;

        //    uint totalRegularOT = 0;
        //    uint totalNightOT = 0;
        //    decimal totalMinutesLate = 0;
        //    foreach (var attendance in attendances)
        //    {
        //        var holiday = Payroll.Holidays.Where(h => h.Date == attendance.Date).FirstOrDefault();
        //        if (IsDateBetween(attendance.Date, Payroll.CutOffStart, Payroll.CutOffEnd) && attendance.Status == FormStatus.Approved)
        //        {
        //            if (attendance.TimeIn.IsBetween(DayStart, DayEnd))
        //            {
        //                if (holiday != null)
        //                {
        //                    if (holiday.Type == HolidayType.Regular)
        //                        totalRegularHolidayDays++;
        //                    else if (holiday.Type == HolidayType.Regular)
        //                        totalSpecialDays++;
        //                }
        //                else
        //                    totalWorkingDays++;
        //                if (attendance.TotalHours > 8)
        //                {
        //                    totalRegularOT += (attendance.TotalHours - 8);
        //                }
        //                if (attendance.TimeIn > DayWorkShiftStart)
        //                {
        //                    var span = attendance.TimeIn - DayWorkShiftStart;
        //                    totalMinutesLate += (decimal)span.TotalMinutes;
        //                }
        //            }
        //            else
        //            {
        //                totalWorkingNights++;
        //                if (attendance.TotalHours > 8)
        //                {
        //                    totalNightOT += (attendance.TotalHours - 8);
        //                }
        //                if (attendance.TimeIn > DayWorkShiftEnd)
        //                {
        //                    var span = attendance.TimeIn - DayWorkShiftStart;
        //                    totalMinutesLate += (decimal)span.TotalMinutes;
        //                }
        //            }
        //        }
        //    }

        //    DaysWorked = totalWorkingDays;
        //    RegularOT = totalRegularOT;
        //    NightsWorked = totalWorkingNights;
        //    NightsOT = totalNightOT;
        //    LatesMinutes = totalMinutesLate;
        //}

        private void ApplyAttendances(IEnumerable<AttendanceModel> attendances)
        {
            var normalDays = attendances
                .Where(a => IsDateBetween(a.Date, Payroll.CutOffStart, Payroll.CutOffEnd) && a.Status == FormStatus.Approved)
                .Where(a => a.TimeIn.IsBetween(DayStart, DayEnd))
                .Where(a => Payroll.Holidays.Where(h => h.Date == a.Date).FirstOrDefault() == null)
                .ToList();

            DaysWorked = (uint)normalDays.Count();
            var nDOTs = normalDays.Where(a => a.TimeOut > DayEnd).ToList();
            uint nDOT = 0;
            foreach (var day in nDOTs)
            {
                var span = day.TimeOut - DayEnd;
                nDOT += (uint)Math.Floor(span.TotalHours);
            }
            RegularOT = nDOT;

            var regHDays = attendances
                .Where(a => IsDateBetween(a.Date, Payroll.CutOffStart, Payroll.CutOffEnd) && a.Status == FormStatus.Approved)
                .Where(a => a.TimeIn.IsBetween(DayStart, DayEnd))
                .Where(a => Payroll.Holidays.Where(h => h.Date == a.Date && h.Type == HolidayType.Regular).FirstOrDefault() != null)
                .ToList();

            RegularHolidaysWorked = (uint)regHDays.Count();

            var specHDays = attendances
                .Where(a => IsDateBetween(a.Date, Payroll.CutOffStart, Payroll.CutOffEnd) && a.Status == FormStatus.Approved)
                .Where(a => a.TimeIn.IsBetween(DayStart, DayEnd))
                .Where(a => Payroll.Holidays.Where(h => h.Date == a.Date && h.Type == HolidayType.SpecialNonWorking).FirstOrDefault() != null)
                .ToList();

            SpecialHolidaysWorked = (uint)specHDays.Count();

            var normalNights = attendances
                .Where(a => IsDateBetween(a.Date, Payroll.CutOffStart, Payroll.CutOffEnd) && a.Status == FormStatus.Approved)
                .Where(a => !a.TimeIn.IsBetween(DayStart, DayEnd))
                .Where(a => Payroll.Holidays.Where(h => h.Date == a.Date).FirstOrDefault() == null)
                .ToList();

            NightsWorked = (uint)normalNights.Count();

            var regHNights = attendances
                .Where(a => IsDateBetween(a.Date, Payroll.CutOffStart, Payroll.CutOffEnd) && a.Status == FormStatus.Approved)
                .Where(a => !a.TimeIn.IsBetween(DayStart, DayEnd))
                .Where(a => Payroll.Holidays.Where(h => h.Date == a.Date && h.Type == HolidayType.Regular).FirstOrDefault() != null)
                .ToList();

            RegularHolidayNightsWorked = (uint)regHNights.Count();

            var specHNights = attendances
                .Where(a => IsDateBetween(a.Date, Payroll.CutOffStart, Payroll.CutOffEnd) && a.Status == FormStatus.Approved)
                .Where(a => !a.TimeIn.IsBetween(DayStart, DayEnd))
                .Where(a => Payroll.Holidays.Where(h => h.Date == a.Date && h.Type == HolidayType.SpecialNonWorking).FirstOrDefault() != null)
                .ToList();

            SpecialHolidayNightsWorked = (uint)specHNights.Count();

            var lates = attendances
                .Where(a => a.TimeIn > Employee.WorkShiftStart)
                .Where(a => Payroll.Holidays.Where(h => h.Date == a.Date).FirstOrDefault() == null)
                .ToList();

            decimal totalMinutesLate = 0;
            foreach (var late in lates)
            {
                var span = late.TimeIn - DayStart;
                totalMinutesLate += (decimal)span.TotalMinutes;
            }
            LatesMinutes = totalMinutesLate;
        }

        private void ApplyLeaves(IEnumerable<LeaveModel> leaves)
        {
            uint totalLeaves = 0;
            var validLeaves = leaves.Where(l => IsDateBetween(l.DateOfAbsence, Payroll.CutOffStart, Payroll.CutOffEnd)).ToList();
            foreach (var leave in validLeaves)
            {
                if (IsDateBetween(leave.DateOfAbsence, Payroll.CutOffStart, Payroll.CutOffEnd) && leave.Status == FormStatus.Approved)
                    totalLeaves += leave.Duration;
            }
            DaysWorked += totalLeaves;
        }
        private bool IsDateBetween(DateOnly date, DateOnly start, DateOnly end)
        {
            return date >= start && date <= end;
        }
    }
}
