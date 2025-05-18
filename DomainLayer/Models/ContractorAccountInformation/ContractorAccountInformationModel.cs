using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DomainLayer.Models.Contractor;

namespace DomainLayer.Models.ContractorAccountInformation
{
    public class ContractorAccountInformationModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(ContractorId))]
        public required Guid ContractorId { get; set; }
        public required ContractorModel Contractor { get; set; }

        //Not sure what to put this depends on UI
    }

}
