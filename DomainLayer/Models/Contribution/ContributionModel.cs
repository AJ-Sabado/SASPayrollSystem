using DomainLayer.Models.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Contribution
{
    public class ContributionModel : IContributionModel
    {
        private const decimal _minimumSSSMonthlyCompRange = 5250;
        private const decimal _maximumSSSMonthlyCompRange = 34750;
        private const decimal _minimumSSSMonthlyContribution = 250;
        private const decimal _maximumSSSMonthlyContribution = 1750;

        private const decimal _minimumPhilHealthMonthlySalaryCap = 10000;
        private const decimal _maximumPhilHealthMonthlySalaryCap = 100000;
        private const decimal _minimumPhilHealthMonthlyContribution = 500;
        private const decimal _maximumPhilHealthMonthlyContribution = 5000;
        private const decimal _currentPhilHealthMonthlyRate = 0.05m;

        private const decimal _minimumPagIbigMonthlyComp = 1500;

        private EmployeeModel _employee = null!;

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Guid EmployeeId { get; set; }
        public EmployeeModel Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                CalculateContributions(Employee.BasicSemiMonthlyRate * 2);
            }
        }

        [Column(TypeName = "money")]
        public decimal SSSAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal PagIbigAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal PhilHealthAmount { get; set; } = 0;

        [Column(TypeName = "money")]
        public decimal TotalContributions { get; set; } = 0;

        private void CalculateContributions(decimal basicMonthlySalary)
        {
            SSSAmount = CalculateSSSAmount(basicMonthlySalary);
            PagIbigAmount = CalculatePagIbigAmount(basicMonthlySalary);
            PhilHealthAmount = CalculatePhilHealthAmount(basicMonthlySalary);
        }
        private decimal CalculateSSSAmount(decimal basicMonthlySalary)
        {
            decimal finalMonthlyAmount = 0;
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
        private decimal CalculatePagIbigAmount(decimal basicMonthlySalary)
        {
            if (basicMonthlySalary <= _minimumPagIbigMonthlyComp)
                return basicMonthlySalary * 0.03m / 2;
            return basicMonthlySalary * 0.04m / 2;
        }
        private decimal CalculatePhilHealthAmount(decimal basicMonthlySalary)
        {
            if (basicMonthlySalary <= _minimumPhilHealthMonthlySalaryCap)
                return _minimumPhilHealthMonthlyContribution / 4;
            else if (basicMonthlySalary >= _maximumPhilHealthMonthlySalaryCap)
                return _maximumPhilHealthMonthlyContribution / 4;
            return basicMonthlySalary * _currentPhilHealthMonthlyRate / 4;
        }
        private decimal CalculateTaxWithholdings(decimal taxableIncome)
        {
            decimal finalAmount = 0;
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
    }
}
