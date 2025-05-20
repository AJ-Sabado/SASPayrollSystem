using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Models.EmployeeAccountInfo;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
using DomainLayer.Models.EmployeeLeave;
using DomainLayer.Models.EmployeePayslip;
using DomainLayer.Models.User;

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
        public TimeOnly DefaultWorkShiftStart { get; set; } = new TimeOnly(8, 0, 0);

        [Column(TypeName = "time")]
        public TimeOnly DefaultWorkShiftEnd { get; set; } = new TimeOnly(17, 0, 0);

        [Column(TypeName = "time")]
        public TimeOnly DefaultBreakTimeStart { get; set; } = new TimeOnly(12, 0, 0);

        [Column(TypeName = "time")]
        public TimeOnly DefaultBreakTimeEnd { get; set; } = new TimeOnly(13, 0, 0);
        [Column(TypeName = "tinyint")]
        public uint LeaveCredits { get; set; } = 5;
        [Column(TypeName = "tinyint")]
        public uint Absences { get; set; } = 0;

        [NotMapped]
        public decimal ExpectedWorkHours
        {
            get 
            {
                var workSpan = DefaultWorkShiftEnd - DefaultWorkShiftStart;
                var breakSpan = DefaultBreakTimeEnd - DefaultBreakTimeStart;
                if (workSpan < TimeSpan.Zero || breakSpan < TimeSpan.Zero)
                {
                    return 0;
                }
                else
                {
                    //Accounts for 1 hour unpaid break
                    return (decimal)(workSpan.TotalHours) - (decimal)(breakSpan.TotalHours);
                }
            }
        }

        //Navigation
        public EmployeeAccountInfoModel? EmployeeAccountInfo { get; set; }
        public ICollection<EmployeeAttendanceLogModel> EmployeeAttendanceLogs { get; } = [];
        public ICollection<EmployeeAttendanceRequestModel> EmployeeAttendanceRequests { get; } = [];
        public ICollection<EmployeeEvaluatedAttendanceModel> EmployeeEvaluatedAttendances { get; } = [];
        public ICollection<EmployeeLeaveModel> EmployeeLeaveRequests { get; } = [];
        public ICollection<EmployeePayslipModel> EmployeePayslips { get; } = [];
    }
}
