using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedBreakStartBreakEndAttendanceRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "BreakEnd",
                table: "EmployeeAttendanceRequestModel",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "BreakStart",
                table: "EmployeeAttendanceRequestModel",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreakEnd",
                table: "EmployeeAttendanceRequestModel");

            migrationBuilder.DropColumn(
                name: "BreakStart",
                table: "EmployeeAttendanceRequestModel");
        }
    }
}
