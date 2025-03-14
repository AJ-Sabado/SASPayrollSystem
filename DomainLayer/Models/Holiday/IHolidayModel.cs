using DomainLayer.Enums;

namespace DomainLayer.Models.Holiday
{
    public interface IHolidayModel
    {
        DateOnly Date { get; set; }
        int Id { get; set; }
        HolidayType Type { get; set; }
    }
}