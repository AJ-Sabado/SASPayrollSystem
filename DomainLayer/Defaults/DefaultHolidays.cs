using DomainLayer.Enums;
using DomainLayer.Models.Holiday;

namespace DomainLayer.Defaults
{
    public class DefaultHolidays : IDefaultHolidays
    {
        public HolidayModel[] DefaultHolidaysList { get; private set; } = 
        {
            new HolidayModel() { Date = new DateOnly(2025, 1, 1), Type = HolidayType.Regular, Description="New Year"},
            new HolidayModel() { Date = new DateOnly(2025, 4, 9), Type = HolidayType.Regular, Description="Araw ng Kagitingan"},
            new HolidayModel() { Date = new DateOnly(2025, 4, 17), Type = HolidayType.Regular, Description="Maundy Thursday"},
            new HolidayModel() { Date = new DateOnly(2025, 4, 18), Type = HolidayType.Regular, Description="Good Friday"},
            new HolidayModel() { Date = new DateOnly(2025, 5, 1), Type = HolidayType.Regular, Description="Labor Day"},
            new HolidayModel() { Date = new DateOnly(2025, 6, 12), Type = HolidayType.Regular, Description="Independence Day"},
            new HolidayModel() { Date = new DateOnly(2025, 8, 25), Type = HolidayType.Regular, Description="National Heroes Day"},
            new HolidayModel() { Date = new DateOnly(2025, 11, 30), Type = HolidayType.Regular, Description="Bonifacio Day"},
            new HolidayModel() { Date = new DateOnly(2025, 12, 25), Type = HolidayType.Regular, Description="Christmas"},
            new HolidayModel() { Date = new DateOnly(2025, 12, 30), Type = HolidayType.Regular, Description="Rizal Day"},

            new HolidayModel() {Date = new DateOnly(2025, 8, 21), Type = HolidayType.SpecialNonWorking, Description="Ninoy Aquino Day"},
            new HolidayModel() {Date = new DateOnly(2025, 11, 1), Type = HolidayType.SpecialNonWorking, Description="All Saint's Day"},
            new HolidayModel() {Date = new DateOnly(2025, 12, 8), Type = HolidayType.SpecialNonWorking, Description="Immaculate Conception"},
            new HolidayModel() {Date = new DateOnly(2025, 12, 31), Type = HolidayType.SpecialNonWorking, Description="Last Day of the Year"},

            new HolidayModel() {Date = new DateOnly(2025, 2, 25), Type = HolidayType.SpecialWorking, Description="EDSA"},

            new HolidayModel() {Date = new DateOnly(2025, 1, 29), Type = HolidayType.SpecialNonWorking, Description="Chinese New Year"},
            new HolidayModel() {Date = new DateOnly(2025, 4, 19), Type = HolidayType.SpecialNonWorking, Description="Black Saturday"},
            new HolidayModel() {Date = new DateOnly(2025, 12, 24), Type = HolidayType.SpecialNonWorking, Description="Christmas Eve"},
            new HolidayModel() {Date = new DateOnly(2025, 10, 31), Type = HolidayType.SpecialNonWorking, Description="All Saints' Day Eve"}
        };
    }
}
