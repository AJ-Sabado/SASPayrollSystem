using DomainLayer.Common;
using DomainLayer.Models.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Department
{
    public class DepartmentModel : IDepartmentModel
    {
        private string _name = string.Empty;
        private Formatter formatter = new Formatter();

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [StringLength(50, ErrorMessage = "Must not exceed 20 characters")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = formatter.ToProperCase(value.Trim().ToLowerInvariant());
                NormalizedName = Name.ToUpperInvariant();
            }
        }

        [Required]
        [StringLength(50)]
        public string NormalizedName { get; private set; } = null!;

        public virtual ICollection<EmployeeModel> Employees { get; set; } = [];
    }
}
