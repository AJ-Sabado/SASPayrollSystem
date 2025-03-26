using DomainLayer.Enums;
using DomainLayer.Models.Employee;
using DomainLayer.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Attendance
{
    public class AttendanceModel : IAttendanceModel
    {
        private TimeOnly _timeOut;

        private readonly TimeOnly _breakTimeStart = new TimeOnly(12, 0, 0);
        private readonly TimeOnly _breakTimeEnd = new TimeOnly(13, 0, 0);

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Column(TypeName = "date")]
        public DateOnly Date { get; set; }

        [Required(ErrorMessage = "Time in is required")]
        [Column(TypeName = "time")]
        public TimeOnly TimeIn { get; set; }

        [Column(TypeName = "time")]
        public TimeOnly TimeOut
        {
            get
            {
                return _timeOut;
            }
            set
            {
                _timeOut = value;
                TotalHours = CalculateTotalHours(TimeIn, TimeOut);
                if (TotalHours > 8)
                {
                    OTHours = TotalHours - 8;
                    TotalHours = 8;
                }
                LateMinutes = CalculateMinutesLate(TimeIn, Employee.WorkShiftStart);
            }
        }

        [Required]
        [Column(TypeName = "tinyint")]
        public uint TotalHours
        { get; private set; } = 0;

        [Column(TypeName = "tinyint")]
        public uint OTHours { get; private set; } = 0;

        [Column(TypeName = "decimal")]
        public decimal LateMinutes { get; private set; } = 0;

        [Required]
        [Column(TypeName = "tinyint")]
        public FormStatus Status { get; set; } = FormStatus.Pending;

        [ForeignKey(nameof(EmployeeId))]
        public required Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        private uint CalculateTotalHours(TimeOnly timeIn, TimeOnly timeOut)
        {
            var totalHoursSpan = (timeIn - timeOut) - (_breakTimeStart - _breakTimeEnd);
            return TotalHours = (uint)Math.Floor(totalHoursSpan.TotalHours);
        }

        private decimal CalculateMinutesLate(TimeOnly timeIn, TimeOnly workShiftIn)
        {
            if (timeIn <= workShiftIn)
            {
                return 0;
            }
            var span = workShiftIn - timeIn;
            return (decimal)Math.Floor(span.TotalMinutes);
        }
    }
}
