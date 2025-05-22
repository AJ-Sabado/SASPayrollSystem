using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPayslipAndAttendanceContractor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHoursRendered",
                table: "Contractors");

            migrationBuilder.DropColumn(
                name: "TotalHoursWorked",
                table: "ContractorPayslipModel");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalHoursRendered",
                table: "ContractorPayslipModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHoursRendered",
                table: "ContractorPayslipModel");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalHoursRendered",
                table: "Contractors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "TotalHoursWorked",
                table: "ContractorPayslipModel",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
