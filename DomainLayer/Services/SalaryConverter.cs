namespace DomainLayer.Services
{
    public class SalaryConverter : ISalaryConverter
    {
        private const int _factor = 261;

        public static decimal ConvertMonthlyToDaily(decimal monthlySalary)
        {
            return monthlySalary * 12 / _factor;
        }

        public static decimal ConvertDailyToHourly(decimal dailySalary, decimal hoursPerDay)
        {
            return dailySalary / hoursPerDay;
        }
    }
}
