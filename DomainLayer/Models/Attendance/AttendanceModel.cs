﻿using DomainLayer.Enums;
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
            }
        }

        [Required]
        [Column(TypeName = "tinyint")]
        public uint TotalHours
        { get; private set; } = 0;

        [Required]
        [Column(TypeName = "tinyint")]
        public FormStatus Status { get; set; } = FormStatus.Pending;

        [ForeignKey(nameof(EmployeeId))]
        public Guid EmployeeId { get; set; }
        public required EmployeeModel Employee { get; set; }

        private uint CalculateTotalHours(TimeOnly timeIn, TimeOnly timeOut)
        {
            var totalHoursSpan = timeIn - timeOut;
            return TotalHours = (uint)Math.Round(totalHoursSpan.TotalHours);
        }
    }
}
