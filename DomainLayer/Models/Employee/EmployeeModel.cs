using DomainLayer.Common;
using DomainLayer.Models.Attendance;
using DomainLayer.Models.Contribution;
using DomainLayer.Models.Department;
using DomainLayer.Models.EmployeeContactInfo;
using DomainLayer.Models.EmployeeEmploymentInfo;
using DomainLayer.Models.EmployeeFinancialInfo;
using DomainLayer.Models.EmployeePersonalInfo;
using DomainLayer.Models.Leave;
using DomainLayer.Models.Salary;
using DomainLayer.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Employee
{
    public class EmployeeModel : IEmployeeModel
    {
        private string _fullName = string.Empty;
        private string _jobTitle = string.Empty;

        private Formatter formatter = new Formatter();

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Must be between 2 - 70 characters")]
        public required string FullName
        {
            get => _fullName;
            set => _fullName = formatter.ToProperCase(value);
        }

        [Required(ErrorMessage = "Birthday is required")]
        public required DateOnly BirthDay { get; set; }

        [Required(ErrorMessage = "Employment date is required")]
        public required DateOnly EmploymentDate { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Must be between 2 - 50 characters")]
        public required string JobTitle
        {
            get => _jobTitle;
            set => _jobTitle = formatter.ToProperCase(value);
        }

        [Required]
        [Column(TypeName = "money")]
        public required decimal BasicSemiMonthlyRate { get; set; }

        [Column(TypeName = "tinyint")]
        public required uint LeaveCredits { get; set; } = 0;

        [Column(TypeName = "time")]
        public required TimeOnly WorkShiftStart { get; set; }

        [Column(TypeName = "time")]
        public required TimeOnly WorkShiftEnd { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public required Guid DepartmentId { get; set; }
        public required DepartmentModel Department { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public required Guid UserId { get; set; }
        public required UserModel User { get; set; }

        public EmployeePersonalInfoModel? EmployeePersonalInfo { get; set; }

        public EmployeeEmploymentInfoModel? EmployeeEmploymentInfo { get; set; }

        public EmployeeContactInfoModel? EmployeeContactInfo { get; set; }

        public EmployeeFinancialInfoModel? EmployeeFinancialInfo { get; set; }

        public ContributionModel Contribution { get; set; } = null!;

        public ICollection<AttendanceModel> Attendances { get; } = [];

        public ICollection<LeaveModel> Leaves { get; } = [];

        public ICollection<SalaryModel> Salaries { get; } = [];
    }
}
