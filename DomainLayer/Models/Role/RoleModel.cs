using DomainLayer.Helpers;
using DomainLayer.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Role
{
    public class RoleModel : IRoleModel
    {
        private string _name = string.Empty;

        [Key]
        public Guid RoleId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Must be between 2 - 20 characters only")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                var formatter = new Formatter();
                _name = formatter.ToProperCase(value);
                NormalizedName = NormalizeString(Name);
            }
        }

        [Required]
        [StringLength(20)]
        public string NormalizedName { get; private set; } = null!;

        public ICollection<UserModel> Users { get; } = [];

        private string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return input.ToUpperInvariant().Trim();
        }
    }
}
