namespace DomainLayer.Services
{
    public interface ISalaryConverter
    {
        static abstract decimal ConvertDailyToHourly(decimal dailySalary, uint hoursPerDay);
        static abstract decimal ConvertMonthlyToDaily(decimal monthlySalary);
    }
}