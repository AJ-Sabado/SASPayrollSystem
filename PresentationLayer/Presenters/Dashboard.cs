using DomainLayer.Models.User;
using DomainLayer.ViewModels.AttendanceLog;
using DomainLayer.ViewModels.DashboardDetails;
using DomainLayer.ViewModels.JobDeskDetails;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters
{
    public class Dashboard : IDashboardPresenter
    {
        private readonly IUnitOfWork _unitOfWork;

        public Dashboard(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public DashboardDetailsViewModel GetDashboardDetails(IUserModel user)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(user.EmployeeId); // diri
            var salaryInfo = _unitOfWork.SalaryRepository.GetByEmployee(user.EmployeeId); // diri

            return new DashboardDetailsViewModel
            {
                NetSalary = salaryInfo.NetSalary, // dirii
                Allowance = salaryInfo.Allowance,
                Bonuses = salaryInfo.Bonuses,
                Deductions = salaryInfo.Deductions,
                Absences = employee.Absences //dirii
            };
        }
        public IEnumerable<AttendanceLogViewModel> GetAttendanceLogs(IUserModel user)
        {
            return _unitOfWork.AttendanceRepository
            .GetForEmployee(user.EmployeeId) // dunno unsa error
            .Select(a => new AttendanceLogViewModel
            {
                Date = a.Date,
                TimeIn = a.TimeIn,
                TimeOut = a.TimeOut,
                Status = a.Status
            });
        }
        public JobDeskDetailsViewModel GetJobDeskDetails(IUserModel user)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(user.EmployeeId);

            return new JobDeskDetailsViewModel
            {
                Department = employee.Department,
                BaseSalary = employee.BaseSalary,
                WorkSlot = employee.WorkSlot,// paedit ko diri jay
                EmploymentStatus = employee.EmploymentStatus,
                EmploymentDate = employee.EmploymentDate,
                Contacts = new ContactsViewModel // ug diri
                {
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Website = employee.Website
                },
                Payslip = new PayslipViewModel // diri saddd
                {
                    BaseSalary = employee.BaseSalary,
                    Bonuses = employee.Bonuses,
                    Deductions = employee.Deductions,
                    Total = employee.NetSalary,
                    PayrollDate = employee.LastPayrollDate,
                    Status = "Completed"
                }
            };
        }
    }
}
