using DomainLayer.Helpers;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Employee;
using DomainLayer.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeeAccountInfo
{
    public class EmployeeAccountInfoModel
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _middleInitial = string.Empty;

        [Key]
        public Guid EmployeeAccountInfoId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        //BASIC INFORMATION
        [StringLength(20)]
        public string FirstName 
        { 
            get => _firstName; 
            set
            {
                var formatter = new Formatter();
                _firstName = formatter.ToProperCase(value.Trim());
            }
        }

        [StringLength(20)]
        public string LastName
        {
            get => _lastName;
            set
            {
                var formatter = new Formatter();
                _lastName = formatter.ToProperCase(value.Trim());
            }
        }

        [StringLength(2)]
        [RegularExpression(@"^[A-Z]\.?$")]
        public string MiddleInitial
        {
            get => _middleInitial;
            set
            {
                var formatter = new Formatter();
                _middleInitial = formatter.ToProperCase(value.Trim());
            }
        }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + MiddleInitial + " " + LastName;
            }
        }

        [Column(TypeName = "tinyint")]
        public Gender Gender { get; set; }

        [Column(TypeName = "date")]
        public DateOnly DateOfBirth { get; set; }

        [Column(TypeName = "tinyint")]
        public Nationality Nationality { get; set; }

        //CONTACT INFORMATION
        [RegularExpression(@"^\+639\d{9}", ErrorMessage = "Invalid phone number format!")]
        [StringLength(13, ErrorMessage = "Must be exactly 13 characters")]
        public string PrimaryPhoneNumber { get; set; } = "+639000000000";

        [RegularExpression(@"^\+639\d{9}", ErrorMessage = "Invalid phone number format!")]
        [StringLength(13, ErrorMessage = "Must be exactly 13 characters")]
        public string SecondaryPhoneNumber { get; set; } = "+639000000000";

        //Regex to be added
        public string Telephone { get; set; } = string.Empty;

        [NotMapped]
        public string PrimaryEmail { get => GetEmail(Employee.User); }

        [EmailAddress(ErrorMessage = "Must be a valid email address")]
        public string SecondaryEmail { get; set; } = string.Empty;

        [StringLength(140, ErrorMessage = "Mailing address must not exceed 70 characters")]
        public string MailingAddress { get; set; } = string.Empty;

        [Url]
        public string FacebookUrl { get; set; } = string.Empty;

        [Url]
        public string LinkedInUrl { get; set; } = string.Empty;

        [Url]
        public string WebsiteUrl { get; set; } = string.Empty;

        //FINANCIAL INFORMATION
        //Regex to be added
        public string TaxIdNumber { get; set; } = string.Empty;
        public string SSSIdNumber { get; set; } = string.Empty;
        public string PhilHealthIdNumber { get; set; } = string.Empty;
        public string PagIbigIdNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string BankAccountName { get; set; } = string.Empty;
        public string BankAccountId { get; set; } = string.Empty;

        //EMPLOYMENT INFORMATION
        [StringLength(20)]
        public string CompanyId { get; set; } = string.Empty;

        [StringLength(30, ErrorMessage = "Role must not exceed 30 characters")]
        public string Role { get; set; } = string.Empty;

        [NotMapped]
        public string Department { get => Employee.User.Department.Name; }

        [Column(TypeName = "tinyint")]
        public EmploymentType EmploymentType { get; set; }

        [Column(TypeName = "date")]
        public DateOnly DateHired { get; set; }

        [NotMapped]
        public uint YearsEmployed { get => GetYearsEmployed(DateHired); }

        

        //INTERNAL METHODS
        private string GetEmail(IUserModel user)
        {
            var email = user.Email;
            if (email == null)
                return string.Empty;
            return email;
        }
        private uint GetYearsEmployed(DateOnly dateHired)
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            return (uint)(now.Year - dateHired.Year);
        }
        private string SetName(string input)
        {
            var formatter = new Formatter();
            return formatter.ToProperCase(input);
        }
    }
}
