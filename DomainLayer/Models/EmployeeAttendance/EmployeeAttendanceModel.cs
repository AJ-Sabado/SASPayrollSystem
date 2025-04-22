using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeeAttendance
{
    public class EmployeeAttendanceModel
    {
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
                LateMinutes = CalculateMinutesLate(TimeIn, Employee.WorkShiftStart);
                OTMinutes = CalculateOTMinutes(TimeOut, Employee.WorkShiftEnd);
                UTMinutes = CalculateUTMinutes(TimeOut, Employee.WorkShiftEnd);

                TotalPayableHours = CalculateTotalPayableHours(TimeIn, TimeOut, Employee.BreakTimeStart, Employee.BreakTimeEnd);
            }
        }

        //DERIVED VALUES
        [Column(TypeName = "smallint")]
        public uint TotalPayableHours { get; set; }

        [Column(TypeName = "smallint")]
        public uint LateMinutes { get; private set; } = 0;

        [Column(TypeName = "smallint")]
        public uint UTMinutes { get; private set; } = 0;

        [Column(TypeName = "smallint")]
        public uint OTMinutes { get; private set; } = 0;

        [Column(TypeName = "tinyint")]
        public HolidayType HolidayStatus { get; set; } = HolidayType.No;

        [Column(TypeName = "tinyint")]
        public FormStatus Status { get; set; } = FormStatus.Pending;

        //INNER TIME CALCULATIONS
        private uint CalculateMinutesLate(TimeOnly timeIn, TimeOnly workShiftStart)
        {
            if (timeIn <= workShiftStart)
                return 0;
            var span = workShiftStart - timeIn;
            return (uint)Math.Floor(span.TotalMinutes);
        }

        private uint CalculateOTMinutes(TimeOnly timeOut, TimeOnly workShiftEnd)
        {
            if (timeOut <= workShiftEnd)
                return 0;
            var span = workShiftEnd - timeOut;
            return (uint)Math.Floor(span.TotalMinutes);
        }

        private uint CalculateUTMinutes(TimeOnly timeOut, TimeOnly workShiftEnd)
        {
            if (timeOut >= workShiftEnd)
                return 0;
            var span = timeOut - workShiftEnd;
            return (uint)Math.Floor(span.TotalMinutes);
        }

        private uint CalculateTotalPayableHours(TimeOnly timeIn, TimeOnly timeOut, TimeOnly breakTimeStart, TimeOnly breakTimeEnd)
        {
            var span = timeIn - timeOut;
            var breakSpan = breakTimeStart - breakTimeEnd;
            return (uint)Math.Floor(span.TotalHours - breakSpan.TotalHours);
        }
    }
}
