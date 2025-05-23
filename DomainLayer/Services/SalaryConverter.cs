namespace DomainLayer.Services
{
    public class SalaryConverter : ISalaryConverter
    {
        private const int _factor = 261;

        public static decimal ConvertMonthlyToDaily(decimal monthlySalary)
        {
            return Math.Floor(monthlySalary * 12 / _factor * 100) / 100;
        }

        public static decimal ConvertDailyToHourly(decimal dailySalary, decimal hoursPerDay)
        {
            return Math.Floor(dailySalary / hoursPerDay * 100) / 100;
        }
    }
}
