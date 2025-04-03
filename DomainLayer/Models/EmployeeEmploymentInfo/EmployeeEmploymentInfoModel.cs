using DomainLayer.Models.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.EmployeeEmploymentInfo
{
    public class EmployeeEmploymentInfoModel : IEmployeeEmploymentInfoModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        //Regex to be updated
        public required string CompanyId { get; set; } = string.Empty;

        [StringLength(30, ErrorMessage = "Job title must not exceed 30 characters")]
        public required string JobTitle { get; set; } = string.Empty;

        [NotMapped]
        public string Department { get => Employee.Department.Name; }

        [Column(TypeName = "date")]
        public required DateOnly DateHired { get; set; }

        [NotMapped]
        public uint YearsEmployed { get => GetYearsEmployed(DateHired); }

        private uint GetYearsEmployed(DateOnly dateHired)
        {
            var now = DateOnly.FromDateTime(DateTime.Now);
            return (uint)(now.Year - dateHired.Year);
        }
    }
}
