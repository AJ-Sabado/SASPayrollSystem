namespace DomainLayer.Services
{
    public class ContributionCalculator
    {
        private const decimal _minimumPhilHealthMonthlySalaryCap = 10000;
        private const decimal _maximumPhilHealthMonthlySalaryCap = 100000;
        private const decimal _minimumPhilHealthMonthlyContribution = 500;
        private const decimal _maximumPhilHealthMonthlyContribution = 5000;
        private const decimal _currentPhilHealthMonthlyRate = 0.05m;

        private const decimal _minimumPagIbigMonthlyComp = 1500;

        private const decimal _minimumSSSMonthlyCompRange = 5250;
        private const decimal _maximumSSSMonthlyCompRange = 34750;
        private const decimal _minimumSSSMonthlyContribution = 250;
        private const decimal _maximumSSSMonthlyContribution = 1750;

        public static decimal CalculateWithholdingTax(decimal taxableIncome)
        {
            decimal finalAmount;
            if (taxableIncome <= 10417)
                finalAmount = 0;
            else if (taxableIncome > 10417 && taxableIncome <= 16667)
                finalAmount = 0.15m * (taxableIncome - 10416.67m);
            else if (taxableIncome > 16667 && taxableIncome <= 33333)
                finalAmount = 0.20m * (taxableIncome - 16666.67m) + 937.5m;
            else if (taxableIncome > 33333 && taxableIncome <= 83333)
                finalAmount = 0.25m * (taxableIncome - 33333.33m) + 4270.83m;
            else if (taxableIncome > 83333 && taxableIncome <= 333333)
                finalAmount = 0.3m * (taxableIncome - 83333.33m) + 16770.83m;
            else
                finalAmount = 0.35m * (taxableIncome - 333333.33m) + 91770.83m;

            return finalAmount;
        }

        public static decimal CalculatePhilHealthAmount(decimal basicMonthlySalary)
        {
            if (basicMonthlySalary <= _minimumPhilHealthMonthlySalaryCap)
                return _minimumPhilHealthMonthlyContribution / 4;
            else if (basicMonthlySalary >= _maximumPhilHealthMonthlySalaryCap)
                return _maximumPhilHealthMonthlyContribution / 4;
            return basicMonthlySalary * _currentPhilHealthMonthlyRate / 4;
        }

        public static decimal CalculatePagIbigAmount(decimal basicMonthlySalary)
        {
            if (basicMonthlySalary <= _minimumPagIbigMonthlyComp)
                return basicMonthlySalary * 0.01m / 2;
            var result = basicMonthlySalary * 0.02m / 2;
            return result < 100 ? result : 100;
        }

        public static decimal CalculateSSSAmount(decimal basicMonthlySalary)
        {
            decimal finalMonthlyAmount;
            if (basicMonthlySalary < _minimumSSSMonthlyCompRange)
                finalMonthlyAmount = _minimumSSSMonthlyContribution;
            else if (basicMonthlySalary >= _maximumSSSMonthlyCompRange)
                finalMonthlyAmount = _maximumSSSMonthlyContribution;
            else
            {
                decimal baseCompensation = basicMonthlySalary - _minimumSSSMonthlyCompRange;
                finalMonthlyAmount = 25 * (Math.Floor(baseCompensation / 500) + 1) + _minimumSSSMonthlyContribution;
            }
            return finalMonthlyAmount / 2;
        }
    }
}
