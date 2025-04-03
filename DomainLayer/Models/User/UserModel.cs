using DomainLayer.Common;
using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using DomainLayer.Models.ForgotPasswordRequest;
using DomainLayer.Models.NewUserRequest;
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

        public UserModel(INewUserRequestModel requestModel, IRoleModel roleModel)
        {
            UserName = requestModel.UserName;
            Salt = requestModel.Salt;
            PasswordHash = requestModel.PasswordHash;
            Email = requestModel.Email;
            RoleId = roleModel.Id;
            Role = (RoleModel)roleModel;
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(20, MinimumLength = 2, ErrorMessage = "Username must be 2 - 20 characters only")]
        public string UserName { get; set; } = null!;

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
        public byte[] Salt { get; private set; } = [];

        [Column(TypeName = "binary(32)")]
        public byte[] PasswordHash { get; private set; } = [];

        [EmailAddress(ErrorMessage = "Must be a valid email address")]
        public string? Email { get; set; }

        //[RegularExpression(@"^\+639\d{9}")]
        //[StringLength(13, MinimumLength = 12, ErrorMessage = "Must be exactly 12 characters")]
        //public string? PhoneNumber { get; set; } = null!;

        [Url(ErrorMessage = "Must be a valid Url")]
        public string? Url { get; set; }

        [ForeignKey(nameof(RoleId))]
        public Guid RoleId { get; set; }

        public RoleModel Role { get; set; } = null!;
        public EmployeeModel? Employee { get; set; }

        public void ConfirmPasswordChange(IForgotPasswordRequestModel request)
        {
            if (request.Status == FormStatus.Approved && request.UserName == UserName && request.Email == Email)
            {
                Salt = request.Salt;
                PasswordHash = request.PasswordHash;
            }
        }
    }
}
