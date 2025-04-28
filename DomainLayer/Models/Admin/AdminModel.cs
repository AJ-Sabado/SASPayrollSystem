using DomainLayer.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Admin
{
    public class AdminModel
    {
        [Key]
        public Guid AdminId { get; set; }

        [ForeignKey(nameof(UserId))]
        public required Guid UserId { get; set; }
        public required UserModel User { get; set; } = null!;
    }
}
