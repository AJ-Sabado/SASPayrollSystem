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
        public DateOnly RequestDate { get; set; }

        [Column(TypeName = "date")]
        public DateOnly AttendanceDate { get; set; }

        [Column(TypeName = "time")]
        public TimeOnly TimeIn { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly TimeOut { get; set; }

        [Column(TypeName = "time")]
        public TimeOnly? BreakStart { get; set; } = null;
        [Column(TypeName = "time")]
        public TimeOnly? BreakEnd { get; set; } = null;

        public string Reason { get; set; } = string.Empty;

        [Column(TypeName = "tinyint")]
        public FormStatus Status { get; set; } = FormStatus.Pending;

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
                    if (BreakStart.HasValue && BreakEnd.HasValue)
                    {
                        var breakSpan = BreakEnd.Value - BreakStart.Value;
                        if (breakSpan < TimeSpan.Zero)
                        {
                            return 0;
                        }
                        else
                        {
                            //Accounts for 1 hour unpaid break
                            return (uint)Math.Floor(span.TotalHours - breakSpan.TotalHours);
                        }
                    }

                    //No break time
                    return (uint)Math.Floor(span.TotalHours);
                }
            }
        }
    }
}
