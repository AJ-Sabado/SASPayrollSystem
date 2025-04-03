using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeePersonalInfo
{
    public class EmployeePersonalInfoModel : IEmployeePersonalInfoModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [StringLength(70, MinimumLength = 2, ErrorMessage = "Full name must be between 2 - 70 characters")]
        public string FullName { get; set; } = string.Empty;

        [Column(TypeName = "tinyint")]
        public GenderEnum Gender { get; set; }

        [Column(TypeName = "date")]
        public DateOnly BirthDate { get; set; }

        [Column(TypeName = "tinyint")]
        public CivilStatusEnum CivilStatus { get; set; }

        [StringLength(70)]
        public string HomeAddress { get; set; } = string.Empty;
    }
}
