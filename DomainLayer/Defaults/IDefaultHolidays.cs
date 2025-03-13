using DomainLayer.Models.Holiday;

namespace DomainLayer.Defaults
{
    public interface IDefaultHolidays
    {
        HolidayModel[] DefaultHolidaysList { get; }
    }
}