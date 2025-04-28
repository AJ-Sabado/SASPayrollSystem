using DomainLayer.Models.Admin;
using DomainLayer.Models.Contractor;
using DomainLayer.Models.Department;
using DomainLayer.Models.Employee;
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStringHome);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AdminModel> Admins { get; set; }
        public DbSet<ContractorModel> Contractors { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<HolidayModel> Holidays { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
