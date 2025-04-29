using DomainLayer.Helpers;
using DomainLayer.Enums;
using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.Role;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.User
{
    public class UserModel : IUserModel
    {
        private const int saltSize = 32;
        private string _password = string.Empty;

        public UserModel() { }

        [Key]
        public Guid UserId { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be 2 - 20 characters only")]
        public string Username { get; set; } = null!;

        [NotMapped]
        public string Password
        {
            private get
            {
                return _password;
            }
            set
            {
                var encryption = new Encryption();
                _password = value;
                Salt = encryption.GenerateSalt(saltSize);
                PasswordHash = encryption.GenerateHash(Password, Salt);
            }
        }

        [Column(TypeName = "binary(32)")]
        public byte[] Salt { get; set; } = [];

        [Column(TypeName = "binary(32)")]
        public byte[] PasswordHash { get; set; } = [];

        [EmailAddress(ErrorMessage = "Must be a valid email address")]
        public string? Email { get; set; }

        [Column(TypeName = "tinyint")]
        public FormStatus Status { get; set; } = FormStatus.Pending;

        [ForeignKey(nameof(RoleId))]
        public Guid RoleId { get; set; }
        public RoleModel Role { get; set; } = null!;

        [ForeignKey(nameof(DepartmentId))]
        public Guid DepartmentId { get; set; }
        public DepartmentModel Department { get; set; } = null!;


        //Data Access on Authentication
        public AdminModel? Admin { get; set; }
        public ContractorModel? Contractor { get; set; }
        public EmployeeModel? Employee { get; set; }
    }
}
