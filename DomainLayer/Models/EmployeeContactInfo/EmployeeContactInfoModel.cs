using DomainLayer.Models.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.EmployeeContactInfo
{
    public class EmployeeContactInfoModel : IEmployeeContactInfoModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [RegularExpression(@"^\+639\d{9}", ErrorMessage = "Invalid phone number format!")]
        [StringLength(13, MinimumLength = 12, ErrorMessage = "Must be exactly 12 characters")]
        public required string PhoneNumber { get; set; }

        [RegularExpression(@"^\+639\d{9}", ErrorMessage = "Invalid phone number format!")]
        [StringLength(13, ErrorMessage = "Must be exactly 12 characters")]
        public string? PhoneNumberAlt { get; set; }

        //Regex to be added
        public string? Telephone { get; set; }

        [NotMapped]
        public string Email { get => GetEmail(Employee); }

        [StringLength(70, ErrorMessage = "Mailing address must not exceed 70 characters")]
        public string MailingAddress { get; set; } = string.Empty;

        private string GetEmail(IEmployeeModel employee)
        {
            var email = employee.User.Email;
            if (email == null)
                return string.Empty;
            return email;
        }
    }
}
