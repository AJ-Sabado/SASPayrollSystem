using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Models.Contractor;

namespace DomainLayer.Models.ContractorPayslip
{
    public class ContractorPayslipModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(ContractorId))]
        public required Guid ContractorId { get; set; }
        public required ContractorModel Contractor { get; set; }

        [Column(TypeName = "date")]
        public DateOnly PeriodStart { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PeriodEnd { get; set; }
        [Column(TypeName = "date")]
        public DateOnly PayDate { get; set; }

        //For historical accuracy

        [Column(TypeName = "money")]
        public decimal AppliedHourlyRate { get; set; }
        
        public decimal TotalHoursRendered { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal NetPay { get; set; } = 0;
    }
}
