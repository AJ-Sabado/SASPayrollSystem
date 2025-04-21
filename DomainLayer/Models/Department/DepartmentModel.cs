using DomainLayer.Common;
using DomainLayer.Models.Employee;
using DomainLayer.Models.User;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models.Department
{
    public class DepartmentModel : IDepartmentModel
    {
        private string _name = string.Empty;

        [Key]
        public Guid DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(50, ErrorMessage = "Must not exceed 50 characters")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                var formatter = new Formatter();
                _name = formatter.ToProperCase(value.Trim().ToLowerInvariant());
                NormalizedName = Name.ToUpperInvariant();
            }
        }

        [Required]
        [StringLength(50)]
        public string NormalizedName { get; private set; } = null!;

        public virtual ICollection<UserModel> Users { get; } = [];
    }
}
