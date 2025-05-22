using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedContratorModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "ContractorAttendanceLogModel");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaximumWeeklyHours",
                table: "Contractors",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalHoursRendered",
                table: "Contractors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeOut",
                table: "ContractorAttendanceLogModel",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeIn",
                table: "ContractorAttendanceLogModel",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHoursRendered",
                table: "Contractors");

            migrationBuilder.AlterColumn<byte>(
                name: "MaximumWeeklyHours",
                table: "Contractors",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "TimeOut",
                table: "ContractorAttendanceLogModel",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "TimeIn",
                table: "ContractorAttendanceLogModel",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewStatus",
                table: "ContractorAttendanceLogModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
