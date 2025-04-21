using DomainLayer.Enums;

namespace DomainLayer.Models.Holiday
{
    public interface IHolidayModel
    {
        DateOnly Date { get; set; }
        int HolidayId { get; set; }
        HolidayType Type { get; set; }
    }
}