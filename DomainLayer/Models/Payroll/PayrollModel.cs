using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.EmployeeSalary;
using DomainLayer.Models.Holiday;
using DomainLayer.Enums;

namespace DomainLayer.Models.Payroll
{
    public class PayrollModel
    {

        [Key]
        public Guid Id { get; set; }

        public ICollection<HolidayModel> Holidays { get; } = [];

        [Required]
        [Column(TypeName = "date")]
        public required DateOnly PayrollDate { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public required DateOnly CutOffStart { get; set; }


        [Required]
        [Column(TypeName = "date")]
        public required DateOnly CutOffEnd { get; set; }

        [Column(TypeName = "tinyint")]
        public uint TotalWorkingDays { get; set; } = 0;

        [Column(TypeName = "tinyint")]
        public uint TotalRegularHolidays { get; set; } = 0;

        public ICollection<EmployeeSalaryModel> Salaries { get; } = [];

        public void CountWorkDaysAndHolidays()
        {
            uint totalWorkingDays = 0;
            uint totalRegularHolidays = 0;

            var nDays = CutOffStart.AddDays(0);

            while (nDays <= CutOffEnd)
            {
                if (nDays.DayOfWeek != DayOfWeek.Saturday && nDays.DayOfWeek != DayOfWeek.Sunday)
                {
                    var holiday = Holidays.Where(h => h.Date == nDays).FirstOrDefault();
                    if (holiday != null)
                    {
                        if (holiday.Type == HolidayType.Regular)
                            totalRegularHolidays++;
                        else
                            totalWorkingDays++;
                    }
                    else
                        totalWorkingDays++;
                }
                nDays = nDays.AddDays(1);
            }
            TotalWorkingDays = totalWorkingDays;
            TotalRegularHolidays = totalRegularHolidays;
        }
    }
}
