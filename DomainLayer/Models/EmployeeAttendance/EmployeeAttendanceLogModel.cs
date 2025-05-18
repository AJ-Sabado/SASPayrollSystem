using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Enums;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeeAttendance
{
    public class EmployeeAttendanceLogModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly? TimeIn { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly? BreakTimeIn { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly? BreakTimeOut { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly? TimeOut { get; set; }

        [Column(TypeName = "tinyint")]
        public AttendanceLogEvaluationStatus EvaluationStatus { get; set; } = AttendanceLogEvaluationStatus.Pending;
    }
}
