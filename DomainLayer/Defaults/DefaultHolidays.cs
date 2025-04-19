using DomainLayer.Enums;
using DomainLayer.Models.Holiday;

namespace DomainLayer.Defaults
{
    public class DefaultHolidays : IDefaultHolidays
    {
        public HolidayModel[] DefaultHolidaysList { get; private set; } =
        {
            new HolidayModel() { Date = new DateOnly(2025, 1, 1), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 4, 9), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 4, 17), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 4, 18), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 5, 1), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 6, 12), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 8, 25), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 11, 30), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 12, 25), Type = HolidayType.Regular},
            new HolidayModel() { Date = new DateOnly(2025, 12, 30), Type = HolidayType.Regular},

            new HolidayModel() {Date = new DateOnly(2025, 8, 21), Type = HolidayType.SpecialNonWorking},
            new HolidayModel() {Date = new DateOnly(2025, 11, 1), Type = HolidayType.SpecialNonWorking},
            new HolidayModel() {Date = new DateOnly(2025, 12, 8), Type = HolidayType.SpecialNonWorking},
            new HolidayModel() {Date = new DateOnly(2025, 12, 31), Type = HolidayType.SpecialNonWorking},

            new HolidayModel() {Date = new DateOnly(2025, 2, 25), Type = HolidayType.SpecialWorking},

            new HolidayModel() {Date = new DateOnly(2025, 1, 29), Type = HolidayType.SpecialNonWorking},
            new HolidayModel() {Date = new DateOnly(2025, 4, 19), Type = HolidayType.SpecialNonWorking},
            new HolidayModel() {Date = new DateOnly(2025, 12, 24), Type = HolidayType.SpecialNonWorking},
            new HolidayModel() {Date = new DateOnly(2025, 10, 31), Type = HolidayType.SpecialNonWorking}
        };
    }
}
