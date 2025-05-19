using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Enums.EmployeeEvaluatedAttendance;
using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeeEvaluatedAttendance
{
    public class EmployeeEvaluatedAttendanceModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }

        [Column(TypeName = "tinyint")]
        public EvaluatedAttendanceDayStatus DayStatus { get; set; } = EvaluatedAttendanceDayStatus.Present;

        public decimal ExpectedWorkHours { get; set; } = 0;
        public decimal ActualWorkHours { get; set; } = 0;

        [Column(TypeName = "datetime")]
        public DateTime EvaluationTimeStamp { get; set; }

        //Log Sources
        public Guid? TimeInReference { get; set; }
        public Guid? BreakTimeInReference { get; set; }
        public Guid? BreakTimeOutReference { get; set; }
        public Guid? TimeOutReference { get; set; }

    }
}
