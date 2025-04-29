namespace DomainLayer.Helpers
{
    public class SalaryConverter
    {
        private const int _factor = 261;

        public static decimal ConvertMonthlyToDaily(decimal monthlySalary)
        {
            return monthlySalary * 12 / _factor;
        }

        public static decimal ConvertDailyToHourly(decimal dailySalary, uint hoursPerDay)
        {
            return dailySalary / hoursPerDay;
        }
    }
}
