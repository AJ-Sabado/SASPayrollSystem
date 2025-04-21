using DomainLayer.Models.User;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models.Contractor
{
    public class ContractorModel
    {
        [Key]
        public Guid ContractorId { get; set; }


        public required Guid UserId { get; set; }
        public required UserModel User { get; set; }
    }
}
