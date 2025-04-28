using DomainLayer.Models.EmployeeAccountInfo;
using DomainLayer.Models.EmployeeAttendance;
using DomainLayer.Models.EmployeeLeave;
using DomainLayer.Models.EmployeePayslip;
using DomainLayer.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Employee
{
    public class EmployeeModel
    {
        [Key]
        public Guid EmployeeId { get; set; }

        [ForeignKey(nameof(UserId))]
        public required Guid UserId { get; set; }
        public required UserModel User { get; set; } = null!;

        //WORK INFORMATION FOR SALARY CALCULATION
        [Column(TypeName = "money")]
        public decimal BasicMonthlyRate { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal BasicDailyRate { get; set; } = 0;

        [Column(TypeName = "time")]
        public TimeOnly WorkShiftStart { get; set; } = new TimeOnly(8, 0, 0);

        [Column(TypeName = "time")]
        public TimeOnly WorkShiftEnd { get; set; } = new TimeOnly(17, 0, 0);

        [Column(TypeName = "time")]
        public TimeOnly BreakTimeStart { get; set; } = new TimeOnly(12, 0, 0);

        [Column(TypeName = "time")]
        public TimeOnly BreakTimeEnd { get; set; } = new TimeOnly(13, 0, 0);

        //Navigation
        public EmployeeAccountInfoModel? EmployeeAccountInfo { get; set; }
        public ICollection<EmployeeAttendanceModel> EmployeeAttendances { get; } = [];
        public ICollection<EmployeeLeaveModel> EmployeeLeaves { get; } = [];
        public ICollection<EmployeePayslipModel> EmployeePayslips { get; } = [];
    }
}
