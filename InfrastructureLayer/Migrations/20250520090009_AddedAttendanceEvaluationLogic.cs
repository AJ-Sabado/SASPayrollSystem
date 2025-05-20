using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedAttendanceEvaluationLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfAbsenceEnd",
                table: "EmployeeLeaveModel",
                newName: "DateOfReturn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfReturn",
                table: "EmployeeLeaveModel",
                newName: "DateOfAbsenceEnd");
        }
    }
}
