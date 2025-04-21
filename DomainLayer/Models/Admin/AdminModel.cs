using DomainLayer.Models.User;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.Models.Admin
{
    public class AdminModel
    {
        [Key]
        public Guid AdminId { get; set; }

        public required Guid UserId { get; set; }
        public required UserModel User { get; set; }
    }
}
