using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models.EmployeeAttendanceRequest
{
    public class EmployeeAttendanceRequestModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}
