using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendance;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.EmployeePayslip
{
    public class EmployeePayslipModel
    {
        [Key]
        public Guid EmployeePayslipId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        [Column(TypeName = "date")]
        public DateOnly PeriodStart { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PeriodEnd { get; set; }
        
        //CALCULATED MONEY VALUES
        [Column(TypeName = "money")]
        public decimal BasicPay { get; set; }
        [Column(TypeName = "money")]
        public decimal OverTimePay { get; set; }
        [Column(TypeName = "money")]
        public decimal Allowance { get; set; }
        [Column(TypeName = "money")]
        public decimal GrossPay { get; set; }
        [Column(TypeName = "money")]
        public decimal Deduction { get; set; }

        [Column(TypeName = "money")]
        public decimal NetPay { get; set; }
    }
}
