using DomainLayer.Models.ContractorAccountInformation;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.ContractorPayslip;
using DomainLayer.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Contractor
{
    public class ContractorModel
    {
        [Key]
        public Guid ContractorId { get; set; }


        [ForeignKey(nameof(UserId))]
        public required Guid UserId { get; set; }
        public required UserModel User { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal BasicHourlyRate { get; set; } = 0;

        [Column(TypeName = "tinyint")]
        public uint MaximumWeeklyHours { get; set; } = 0;

        public ICollection<ContractorAttendanceLogModel> ContractorAttendanceLogs { get; } = [];
        public ICollection<ContractorPayslipModel> ContractorPayslips { get; } = [];
        public ContractorAccountInformationModel? ContractorAccountInformation { get; set; }

    }
}
