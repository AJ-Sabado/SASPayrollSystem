using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeeAttendance
{
    public class EmployeeAttendanceModel
    {
        private const int _nightStart = 22;
        private const int _nightEnd = 6;

        private TimeOnly _timeOut;

        [Key]
        public Guid EmployeeAttendanceId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }

        [Column(TypeName = "time")]
        public TimeOnly TimeIn { get; set; }

        //Automatically fills in other fields through calculations
        [Column(TypeName = "time")]
        public TimeOnly TimeOut
        {
            get
            {
                return _timeOut;
            }
            set
            {
                _timeOut = value;
                CalculateDerivedValues();
            }
        }

        //DERIVED VALUES
        [Column(TypeName = "smallint")]
        public uint PayableHours { get; set; } = 0;

        [Column(TypeName = "smallint")]
        public uint LateMinutes { get; private set; } = 0;

        [Column(TypeName = "smallint")]
        public uint UTHours { get; private set; } = 0;

        [Column(TypeName = "smallint")]
        public uint OTHours { get; private set; } = 0;

        [Column(TypeName = "boolean")]
        public bool IsNight { get; private set; } = false;

        [Column(TypeName = "tinyint")]
        public HolidayType HolidayStatus { get; set; } = HolidayType.No;

        [Column(TypeName = "tinyint")]
        public FormStatus Status { get; set; } = FormStatus.Pending;

        //INNER TIME CALCULATIONS
        public void CalculateDerivedValues()
        {
            LateMinutes = CalculateMinutesLate(TimeIn, Employee.WorkShiftStart);
            OTHours = CalculateOTHours(TimeOut, Employee.WorkShiftEnd);
            UTHours = CalculateUTHours(TimeOut, Employee.WorkShiftEnd);
            var validStart = TimeIn > Employee.WorkShiftStart ? TimeIn : Employee.WorkShiftStart;   //Disregards early time ins
            var validEnd = TimeOut < Employee.WorkShiftEnd ? TimeOut : Employee.WorkShiftEnd;   //Disregards overtime
            PayableHours = CalculateTotalPayableHours(validStart, validEnd, Employee.BreakTimeStart, Employee.BreakTimeEnd);
            IsNight = IsDayOrNight(Employee.WorkShiftStart, Employee.WorkShiftEnd);
            //For now
            Status = FormStatus.Approved;
        }

        private uint CalculateMinutesLate(TimeOnly timeIn, TimeOnly workShiftStart)
        {
            if (timeIn <= workShiftStart)
                return 0;
            var span = workShiftStart - timeIn;
            return (uint)Math.Floor(span.TotalMinutes);
        }

        private uint CalculateOTHours(TimeOnly timeOut, TimeOnly workShiftEnd)
        {
            if (timeOut <= workShiftEnd)
                return 0;
            var span = workShiftEnd - timeOut;
            return (uint)Math.Floor(span.TotalHours);
        }

        private uint CalculateUTHours(TimeOnly timeOut, TimeOnly workShiftEnd)
        {
            if (timeOut >= workShiftEnd)
                return 0;
            var span = timeOut - workShiftEnd;
            return (uint)Math.Floor(span.TotalHours);
        }

        private uint CalculateTotalPayableHours(TimeOnly start, TimeOnly end, TimeOnly breakTimeStart, TimeOnly breakTimeEnd)
        {
            var span = start - end;
            var breakSpan = breakTimeStart - breakTimeEnd;
            return (uint)Math.Floor(span.TotalHours - breakSpan.TotalHours);
        }

        private bool IsDayOrNight(TimeOnly workShiftStart, TimeOnly workShiftEnd)
        {
            return workShiftStart.Hour >= _nightStart && workShiftEnd.Hours <= _nightEnd;
        }
    }
}
