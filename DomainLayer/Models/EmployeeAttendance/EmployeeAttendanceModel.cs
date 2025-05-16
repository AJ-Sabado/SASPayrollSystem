using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Enums;
using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeeAttendance
{
    public class EmployeeAttendanceModel
    {
        [Key]
        public Guid EmployeeAttendanceId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly TimeIn { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly BreakTimeIn { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly BreakTimeOut { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly TimeOut { get; set; }
        [Column(TypeName = "tinyint")]
        public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
        [Column(TypeName = "tinyint")]
        public FormStatus OTStatus { get; set; } = FormStatus.Pending;

        //Derived values
        [NotMapped]
        public uint PayableHours
        {
            get
            {
                if (TimeIn == TimeOut || TimeIn == BreakTimeOut || TimeOut == BreakTimeIn)
                {
                    Status = AttendanceStatus.Absent;
                    return 0;
                }

                var validStart = TimeIn > Employee.WorkShiftStart ? TimeIn : Employee.WorkShiftStart;
                var validEnd = TimeOut < Employee.WorkShiftEnd ? TimeOut : Employee.WorkShiftEnd;
                var totalWorked = validStart - validEnd;
                var breakTime = BreakTimeOut - BreakTimeIn;
                totalWorked -= breakTime;
                return (uint)Math.Floor(totalWorked.TotalHours);
            }
        }

        [NotMapped]
        public uint OTHours
        {
            get
            {
                if (OTStatus == FormStatus.Approved)
                {
                    var span = TimeOut - Employee.WorkShiftEnd;
                    if (span > TimeSpan.Zero)
                    {
                        return (uint)Math.Floor(span.TotalHours);
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
