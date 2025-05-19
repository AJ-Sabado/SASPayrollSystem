using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewPayslipAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalGrossPay",
                table: "EmployeePayslipModel",
                newName: "PerfectAttendanceBonus");

            migrationBuilder.RenameColumn(
                name: "NetPay",
                table: "EmployeePayslipModel",
                newName: "NetSalary");

            migrationBuilder.RenameColumn(
                name: "LegalHolidaysHours",
                table: "EmployeePayslipModel",
                newName: "UTMinutes");

            migrationBuilder.AddColumn<decimal>(
                name: "CompanyLoans",
                table: "EmployeePayslipModel",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GovernmentLoans",
                table: "EmployeePayslipModel",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "GrossPay",
                table: "EmployeePayslipModel",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<short>(
                name: "HolidayHours",
                table: "EmployeePayslipModel",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<decimal>(
                name: "Legal13thMonthPay",
                table: "EmployeePayslipModel",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<short>(
                name: "PaidLeaveHours",
                table: "EmployeePayslipModel",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyLoans",
                table: "EmployeePayslipModel");

            migrationBuilder.DropColumn(
                name: "GovernmentLoans",
                table: "EmployeePayslipModel");

            migrationBuilder.DropColumn(
                name: "GrossPay",
                table: "EmployeePayslipModel");

            migrationBuilder.DropColumn(
                name: "HolidayHours",
                table: "EmployeePayslipModel");

            migrationBuilder.DropColumn(
                name: "Legal13thMonthPay",
                table: "EmployeePayslipModel");

            migrationBuilder.DropColumn(
                name: "PaidLeaveHours",
                table: "EmployeePayslipModel");

            migrationBuilder.RenameColumn(
                name: "UTMinutes",
                table: "EmployeePayslipModel",
                newName: "LegalHolidaysHours");

            migrationBuilder.RenameColumn(
                name: "PerfectAttendanceBonus",
                table: "EmployeePayslipModel",
                newName: "TotalGrossPay");

            migrationBuilder.RenameColumn(
                name: "NetSalary",
                table: "EmployeePayslipModel",
                newName: "NetPay");
        }
    }
}
