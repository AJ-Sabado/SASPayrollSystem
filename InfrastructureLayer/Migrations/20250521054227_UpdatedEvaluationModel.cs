using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEvaluationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NightDifferentialHours",
                table: "EmployeeEvaluatedAttendanceModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OvertimeHours",
                table: "EmployeeEvaluatedAttendanceModel",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NightDifferentialHours",
                table: "EmployeeEvaluatedAttendanceModel");

            migrationBuilder.DropColumn(
                name: "OvertimeHours",
                table: "EmployeeEvaluatedAttendanceModel");
        }
    }
}
