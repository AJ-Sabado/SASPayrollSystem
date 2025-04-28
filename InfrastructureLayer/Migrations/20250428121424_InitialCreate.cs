using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    HolidayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.HolidayId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Salt = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.ContractorId);
                    table.ForeignKey(
                        name: "FK_Contractors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasicMonthlyRate = table.Column<decimal>(type: "money", nullable: false),
                    BasicDailyRate = table.Column<decimal>(type: "money", nullable: false),
                    WorkShiftStart = table.Column<TimeOnly>(type: "time", nullable: false),
                    WorkShiftEnd = table.Column<TimeOnly>(type: "time", nullable: false),
                    BreakTimeStart = table.Column<TimeOnly>(type: "time", nullable: false),
                    BreakTimeEnd = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAccountInfoModel",
                columns: table => new
                {
                    EmployeeAccountInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CivilStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    HomeAddress = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateHired = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    PhoneNumberAlt = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailingAddress = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhilHealthIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSSIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PagIbigIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankingDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAccountInfoModel", x => x.EmployeeAccountInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeAccountInfoModel_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttendanceModel",
                columns: table => new
                {
                    EmployeeAttendanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeIn = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeOut = table.Column<TimeOnly>(type: "time", nullable: false),
                    HolidayStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    BreakIn = table.Column<TimeOnly>(type: "time", nullable: false),
                    BreakOut = table.Column<TimeOnly>(type: "time", nullable: false),
                    PayableHours = table.Column<short>(type: "smallint", nullable: false),
                    LateMinutes = table.Column<short>(type: "smallint", nullable: false),
                    UTHours = table.Column<short>(type: "smallint", nullable: false),
                    OTHours = table.Column<short>(type: "smallint", nullable: false),
                    IsNight = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendanceModel", x => x.EmployeeAttendanceId);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendanceModel_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLeaveModel",
                columns: table => new
                {
                    EmployeeLeaveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfFiling = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfAbsenceStart = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfAbsenceEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    Duration = table.Column<short>(type: "smallint", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLeaveModel", x => x.EmployeeLeaveId);
                    table.ForeignKey(
                        name: "FK_EmployeeLeaveModel_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayslipModel",
                columns: table => new
                {
                    EmployeePayslipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    BasicPay = table.Column<decimal>(type: "money", nullable: false),
                    BonusPay = table.Column<decimal>(type: "money", nullable: false),
                    OvertimePay = table.Column<decimal>(type: "money", nullable: false),
                    NightShiftDifferentialPay = table.Column<decimal>(type: "money", nullable: false),
                    HolidayPay = table.Column<decimal>(type: "money", nullable: false),
                    PaidLeaves = table.Column<decimal>(type: "money", nullable: false),
                    Allowance = table.Column<decimal>(type: "money", nullable: false),
                    GrossPay = table.Column<decimal>(type: "money", nullable: false),
                    SalaryTax = table.Column<decimal>(type: "money", nullable: false),
                    GovtContribution = table.Column<decimal>(type: "money", nullable: false),
                    LoanDeduction = table.Column<decimal>(type: "money", nullable: false),
                    LateUTDeduction = table.Column<decimal>(type: "money", nullable: false),
                    NetPay = table.Column<decimal>(type: "money", nullable: false),
                    OrdinaryDaysWorked = table.Column<long>(type: "bigint", nullable: false),
                    OrdinaryNightHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    OrdinaryNightOTHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    OrdinaryOTHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedHolidayNoWorkPay = table.Column<long>(type: "bigint", nullable: false),
                    HolidayHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    HolidayOTHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    HolidayNightHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    HolidayOTNightHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    SpecialHolidayHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    SpecialHolidayOTHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    SpecialHolidayNightHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    SpecialHolidayOTNightHoursWorked = table.Column<long>(type: "bigint", nullable: false),
                    ApprovedPaidLeaves = table.Column<long>(type: "bigint", nullable: false),
                    SSSContributionAmount = table.Column<decimal>(type: "money", nullable: false),
                    PagIbigContributionAmount = table.Column<decimal>(type: "money", nullable: false),
                    PhilHealthContributionAmount = table.Column<decimal>(type: "money", nullable: false),
                    CompanyLoansAmount = table.Column<decimal>(type: "money", nullable: false),
                    GovtLoansAmount = table.Column<decimal>(type: "money", nullable: false),
                    OrdinaryLateMinutes = table.Column<long>(type: "bigint", nullable: false),
                    OrdinaryUTHours = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayslipModel", x => x.EmployeePayslipId);
                    table.ForeignKey(
                        name: "FK_EmployeePayslipModel_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_UserId",
                table: "Contractors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAccountInfoModel_EmployeeId",
                table: "EmployeeAccountInfoModel",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendanceModel_EmployeeId",
                table: "EmployeeAttendanceModel",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLeaveModel_EmployeeId",
                table: "EmployeeLeaveModel",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayslipModel_EmployeeId",
                table: "EmployeePayslipModel",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropTable(
                name: "EmployeeAccountInfoModel");

            migrationBuilder.DropTable(
                name: "EmployeeAttendanceModel");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveModel");

            migrationBuilder.DropTable(
                name: "EmployeePayslipModel");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
