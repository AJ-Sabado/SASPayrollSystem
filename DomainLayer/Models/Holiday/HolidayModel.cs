using DomainLayer.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models.Holiday
{
    public class HolidayModel : IHolidayModel
    {
        [Key]
        public int HolidayId { get; set; }

        [Column(TypeName = "date")]
        public required DateOnly Date { get; set; }

        [Column(TypeName = "tinyint")]
        public HolidayType Type { get; set; } = HolidayType.Regular;
    }
}
