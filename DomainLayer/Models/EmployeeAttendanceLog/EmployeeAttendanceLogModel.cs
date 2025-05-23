using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Enums.EmployeeAttendanceLog;
using DomainLayer.Models.Employee;
using DomainLayer.Services;

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

        public string BusinessId { get; set; } = BusinessIdGenerator.GenerateEmployeeAttendanceLogId();
    }
}
