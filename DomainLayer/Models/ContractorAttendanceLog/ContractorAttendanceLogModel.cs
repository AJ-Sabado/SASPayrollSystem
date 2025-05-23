using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Enums;
using DomainLayer.Models.Contractor;

namespace DomainLayer.Models.ContractorAttendanceLog
{
    public class ContractorAttendanceLogModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(ContractorId))]
        public required Guid ContractorId { get; set; }
        public required ContractorModel Contractor { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? TimeIn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeOut { get; set; }

        [NotMapped]
        public decimal Duration
        {
            get
            {
                if (TimeIn.HasValue && TimeOut.HasValue)
                {
                    var span = TimeIn.Value - TimeOut.Value;
                    var result = Math.Abs(span.TotalHours);
                    return result > 0 ? (decimal)result : 0;
                }
                else
                    return 0;
            }
        }
    }
}
