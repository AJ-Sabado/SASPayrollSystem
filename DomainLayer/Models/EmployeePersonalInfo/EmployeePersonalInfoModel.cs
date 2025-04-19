using DomainLayer.Common;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Employee;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeePersonalInfo
{
    public class EmployeePersonalInfoModel : IEmployeePersonalInfoModel
    {
        private string _fullName = string.Empty;

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [StringLength(70, ErrorMessage = "Full name must not exceed 70 characters")]
        public required string FullName { get => _fullName; set => _fullName = SetName(value.Trim()); }

        [Column(TypeName = "tinyint")]
        public GenderEnum Gender { get; set; }

        [Column(TypeName = "date")]
        public DateOnly BirthDate { get; set; }

        [Column(TypeName = "tinyint")]
        public CivilStatusEnum CivilStatus { get; set; }

        [StringLength(70, ErrorMessage = "Home address must not exceed 70 characters")]
        public string HomeAddress { get; set; } = string.Empty;

        private string SetName(string input)
        {
            var formatter = new Formatter();
            return formatter.ToProperCase(input);
        }
    }
}
