using DomainLayer.Models.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.EmployeeFinancialInfo
{
    public class EmployeeFinancialInfo : IEmployeeFinancialInfo
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        //REGEX FOR IDs TO BE ADDED
        public string TaxIdNumber { get; set; } = string.Empty;

        public string PhilHealthIdNumber { get; set; } = string.Empty;

        public string SSSIdNumber { get; set; } = string.Empty;

        public string PagIbigIdNumber { get; set; } = string.Empty;

        public string BankingDetails { get; set; } = string.Empty;
    }
}
