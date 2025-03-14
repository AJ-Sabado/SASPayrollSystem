using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Holiday
{
    public class HolidayModel : IHolidayModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "date")]
        public required DateOnly Date { get; set; }

        [Column(TypeName = "tinyint")]
        public HolidayType Type { get; set; } = HolidayType.Regular;
    }
}
