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

        [Column(TypeName = "tinyint")]
        public HolidayType HolidayType { get; set; } = HolidayType.Not;

        [Column(TypeName = "time")]
        public TimeOnly TimeIn { get; set; }

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
                TotalHours = CalculateTotalHours(TimeIn, TimeOut, Employee.BreakTimeStart, Employee.BreakTimeEnd);
                if (TotalHours > 8)
                {
                    OTMinutes = CalculateOTMinutes(TimeOut, Employee.WorkShiftEnd);
                    TotalHours = 8;
                }
                LateMinutes = CalculateMinutesLate(TimeIn, Employee.WorkShiftStart);
                Status = FormStatus.Approved;
            }
        }

        [Column(TypeName = "tinyint")]
        public uint TotalHours
        { get; private set; } = 0;

        [Column(TypeName = "smallint")]
        public uint OTMinutes { get; private set; } = 0;

        [Column(TypeName = "smallint")]
        public uint LateMinutes { get; private set; } = 0;

        [Column(TypeName = "tinyint")]
        public FormStatus Status { get; set; } = FormStatus.Pending;


        private uint CalculateTotalHours(TimeOnly timeIn, TimeOnly timeOut, TimeOnly breakTimeStart, TimeOnly breakTimeEnd)
        {
            var totalHoursSpan = (timeIn - timeOut) - (breakTimeStart - breakTimeEnd);
            return TotalHours = (uint)Math.Floor(totalHoursSpan.TotalHours);
        }

        private uint CalculateOTMinutes(TimeOnly timeOut, TimeOnly workShiftEnd)
        {
            var span = workShiftEnd - timeOut;
            return (uint)Math.Floor(span.TotalMinutes);
        }

        private uint CalculateMinutesLate(TimeOnly timeIn, TimeOnly workShiftStart)
        {
            if (timeIn <= workShiftStart)
            {
                return 0;
            }
            var span = workShiftStart - timeIn;
            return (uint)Math.Floor(span.TotalMinutes);
        }
    }
}
