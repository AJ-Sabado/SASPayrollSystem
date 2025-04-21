using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeeLeave
{
    public class EmployeeLeaveModel
    {
        [Key]
        public Guid EmployeeLeaveId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [Column(TypeName = "date")]
        public DateOnly DateOfFiling { get; set; }

        [Column(TypeName = "date")]
        public DateOnly DateOfAbsence { get; set; }

        [Column(TypeName = "smallint")]
        public uint Duration { get; set; }

        [Column(TypeName = "tinyint")]
        public FormStatus Status { get; set; } = FormStatus.Pending;
    }
}
