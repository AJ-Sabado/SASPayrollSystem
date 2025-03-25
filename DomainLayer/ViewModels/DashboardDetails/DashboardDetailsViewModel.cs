using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModels.DashboardDetails
{
    public class DashboardDetailsViewModel : IDashboardDetailsViewModel
    {
        public string UpcomingDate { get; set; } = string.Empty;
        public string TotalHours { get; set; } = string.Empty;
        public string TotalSalary { get; set; } = string.Empty;
        public string Allowance { get; set; } = string.Empty;
        public string Bonuses { get; set; } = string.Empty;
        public string Deductions { get; set; } = string.Empty;
    }
}
