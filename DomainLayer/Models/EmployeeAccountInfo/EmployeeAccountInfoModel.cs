using DomainLayer.Common;
using DomainLayer.Enums.EmployeePersonalInfo;
using DomainLayer.Models.Employee;
using DomainLayer.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeeAccountInfo
{
    public class EmployeeAccountInfoModel
    {
        private string _fullName = string.Empty;

        [Key]
        public Guid EmployeeAccountInfoId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        //PERSONAL INFORMATION

        [StringLength(70, ErrorMessage = "Full name must not exceed 70 characters")]
        public string FullName { get => _fullName; set => _fullName = SetName(value.Trim()); }

        [Column(TypeName = "tinyint")]
        public GenderEnum Gender { get; set; }

        [Column(TypeName = "date")]
        public DateOnly BirthDate { get; set; }

        [Column(TypeName = "tinyint")]
        public CivilStatusEnum CivilStatus { get; set; }

        [StringLength(70, ErrorMessage = "Home address must not exceed 70 characters")]
        public string HomeAddress { get; set; } = string.Empty;

        //EMPLOYMENT INFORMATION
        [StringLength(20)]
        public string CompanyId { get; set; } = string.Empty;

        [StringLength(30, ErrorMessage = "Job title must not exceed 30 characters")]
        public string JobTitle { get; set; } = string.Empty;

        [NotMapped]
        public string Department { get => Employee.User.Department.Name; }

        [Column(TypeName = "date")]
        public DateOnly DateHired { get; set; }

        [NotMapped]
        public uint YearsEmployed { get => GetYearsEmployed(DateHired); }

        //CONTACT INFORMATION
        [RegularExpression(@"^\+639\d{9}", ErrorMessage = "Invalid phone number format!")]
        [StringLength(13, ErrorMessage = "Must be exactly 13 characters")]
        public string PhoneNumber { get; set; } = "+639000000000";

        [RegularExpression(@"^\+639\d{9}", ErrorMessage = "Invalid phone number format!")]
        [StringLength(13, ErrorMessage = "Must be exactly 13 characters")]
        public string PhoneNumberAlt { get; set; } = "+639000000000";

        //Regex to be added
        public string Telephone { get; set; } = string.Empty;

        [NotMapped]
        public string Email { get => GetEmail(Employee.User); }

        [StringLength(140, ErrorMessage = "Mailing address must not exceed 70 characters")]
        public string MailingAddress { get; set; } = string.Empty;

        //FINANCIAL INFORMATION
        //Regex to be added
        public string TaxIdNumber { get; set; } = string.Empty;
        public string PhilHealthIdNumber { get; set; } = string.Empty;
        public string SSSIdNumber { get; set; } = string.Empty;
        public string PagIbigIdNumber { get; set; } = string.Empty;
        public string BankingDetails { get; set; } = string.Empty;

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
