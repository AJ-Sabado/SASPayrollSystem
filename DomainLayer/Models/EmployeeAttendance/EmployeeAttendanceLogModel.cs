using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Enums;
using DomainLayer.Enums.EmployeeAttendanceLog;
using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeeAttendanceLog
{
    public class EmployeeAttendanceLogModel
    {
        [Key]
        public Guid EmployeeAttendanceId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly TimeStamp { get; set; }

        [Column(TypeName = "tinyint")]
        public AttendanceLogEventType EventType { get; set; } = AttendanceLogEventType.TimeIn;

        //[Column(TypeName = "time")]
        //public TimeOnly? BreakTimeIn { get; set; }
        //[Column(TypeName = "time")]
        //public TimeOnly? BreakTimeOut { get; set; }
        //[Column(TypeName = "time")]
        //public TimeOnly? TimeOut { get; set; }
        //[Column(TypeName = "tinyint")]
        //public AttendanceStatus Status { get; set; } = AttendanceStatus.Present;
        //[Column(TypeName = "tinyint")]
        //public FormStatus OTStatus { get; set; } = FormStatus.Pending;

        //Derived values
        private uint _payableHours = 0;        
        [NotMapped]
        public uint PayableHours
        {
            get
            {
                return _payableHours;
                //if (TimeIn == TimeOut || TimeIn == BreakTimeOut || TimeOut == BreakTimeIn)
                //{
                //    Status = AttendanceStatus.Absent;
                //    return 0;
                //}

                //var validStart = TimeIn > Employee.WorkShiftStart ? TimeIn : Employee.WorkShiftStart;
                //var validEnd = TimeOut < Employee.WorkShiftEnd ? TimeOut : Employee.WorkShiftEnd;
                //var totalWorked = validStart - validEnd;
                //var breakTime = BreakTimeOut - BreakTimeIn;
                //totalWorked -= breakTime;
                //return (uint)Math.Floor(totalWorked.TotalHours);
            }
        }

        //Temporary
        private uint _otHours = 0;
        [NotMapped]
        public uint OTHours
        {
            get
            {
                return _otHours;
                //if (OTStatus == FormStatus.Approved)
                //{
                //    var span = TimeOut - Employee.WorkShiftEnd;
                //    if (span > TimeSpan.Zero)
                //    {
                //        return (uint)Math.Floor(span.TotalHours);
                //    }
                //    else
                //    {
                //        return 0;
                //    }
                //}
                //else
                //{
                //    return 0;
                //}
            }
        }
    }
}
