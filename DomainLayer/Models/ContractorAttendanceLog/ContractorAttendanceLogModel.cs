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

        [Column(TypeName = "time")]
        public TimeOnly? TimeIn { get; set; }
        [Column(TypeName = "time")]
        public TimeOnly? TimeOut { get; set; }

        public FormStatus ReviewStatus { get; set; } = FormStatus.Pending;
    }
}
