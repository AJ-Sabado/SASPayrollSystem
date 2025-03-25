using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ViewModels.JobDeskDetails
{
    public class JobDeskDetailsViewModel : IJobDeskDetailsViewModel
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string BaseSalary { get; set; } = string.Empty;
        public string WorkShift { get; set; } = string.Empty;
        public string EmploymentStatus { get; set; } = string.Empty;
        public string EmploymentDate { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
    }
}
