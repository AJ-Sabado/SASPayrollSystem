namespace DomainLayer.Services
{
    public interface ISalaryConverter
    {
        static abstract decimal ConvertDailyToHourly(decimal dailySalary, decimal hoursPerDay);
        static abstract decimal ConvertMonthlyToDaily(decimal monthlySalary);
    }
}