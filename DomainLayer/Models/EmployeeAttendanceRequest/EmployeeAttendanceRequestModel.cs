using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Enums;
using DomainLayer.Models.Employee;

namespace DomainLayer.Models.EmployeeAttendanceRequest
{
    public class EmployeeAttendanceRequestModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }
        [Column(TypeName = "tinyint")]
        public LeaveType Reason { get; set; } = LeaveType.Sick;
        [Column(TypeName = "time")]
        public TimeOnly TimeIn { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly TimeOut { get; set; }

        [NotMapped]
        public uint TotalHours
        {
            get
            {
                var span = TimeOut - TimeIn;
                if (span < TimeSpan.Zero)
                {
                    return 0;
                }
                else
                {
                    //Accounts for 1 hour unpaid break
                    return (uint)Math.Floor(span.TotalHours) - 1;
                }
            }
        }
    }
}
