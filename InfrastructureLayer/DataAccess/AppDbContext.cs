using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;
using DomainLayer.Models.ContractorPayslip;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
using DomainLayer.Models.EmployeeAttendanceLog;
using DomainLayer.Models.EmployeeAttendanceRequest;
using DomainLayer.Models.EmployeeEvaluatedAttendance;
using DomainLayer.Models.EmployeeLeave;
using DomainLayer.Models.EmployeePayslip;
using DomainLayer.Models.Holiday;
using DomainLayer.Models.Role;
using DomainLayer.Models.User;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.DataAccess
{
    public class AppDbContext : DbContext
    {
        private const string connectionStringHome = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SASPayrollDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private const string connectionStringLab = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SASPayrollDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private const string connectionStringTim = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SASPayrollDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private const string connectionStringAFA = "Data Source=(localdb)\\ProjectModels;Initial Catalog=SASPayrollDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private const string connectionStringNoreen = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SASPayrollDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStringNoreen);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //DbSets for quick access
        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<ContractorAttendanceLogModel> ContractorAttendanceLogs { get; set; }
        public DbSet<ContractorPayslipModel> ContractorPayslips { get; set; }
        public DbSet<ContractorModel> Contractors { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<EmployeeAttendanceLogModel> EmployeeAttendanceLogs { get; set; }
        public DbSet<EmployeeAttendanceRequestModel> EmployeeAttendanceRequests { get; set; }
        public DbSet<EmployeeEvaluatedAttendanceModel> EmployeeEvaluatedAttendances { get; set; }
        public DbSet<EmployeeLeaveModel> EmployeeLeaves { get; set; }
        public DbSet<EmployeePayslipModel> EmployeePayslips { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<HolidayModel> Holidays { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
