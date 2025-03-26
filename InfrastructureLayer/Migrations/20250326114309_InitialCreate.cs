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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForgotPasswordRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    DateOfRequest = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeOfRequest = table.Column<TimeOnly>(type: "time", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgotPasswordRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewUserRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    DateOfRequest = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeOfRequest = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewUserRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrollDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CutOffStart = table.Column<DateOnly>(type: "date", nullable: false),
                    CutOffEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalWorkingDays = table.Column<byte>(type: "tinyint", nullable: false),
                    TotalRegularHolidays = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    PayrollModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_Payrolls_PayrollModelId",
                        column: x => x.PayrollModelId,
                        principalTable: "Payrolls",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Salt = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "binary(32)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    BirthDay = table.Column<DateOnly>(type: "date", nullable: false),
                    EmploymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BasicSemiMonthlyRate = table.Column<decimal>(type: "money", nullable: false),
                    LeaveCredits = table.Column<byte>(type: "tinyint", nullable: false),
                    WorkShiftStart = table.Column<TimeOnly>(type: "time", nullable: false),
                    WorkShiftEnd = table.Column<TimeOnly>(type: "time", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeIn = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeOut = table.Column<TimeOnly>(type: "time", nullable: false),
                    TotalHours = table.Column<byte>(type: "tinyint", nullable: false),
                    OTHours = table.Column<byte>(type: "tinyint", nullable: false),
                    LateMinutes = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contributions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SSSAmount = table.Column<decimal>(type: "money", nullable: false),
                    PagIbigAmount = table.Column<decimal>(type: "money", nullable: false),
                    PhilHealthAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalContributions = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contributions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfFiling = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfAbsence = table.Column<DateOnly>(type: "date", nullable: false),
                    Duration = table.Column<short>(type: "smallint", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leaves_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayrollId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DaysWorked = table.Column<byte>(type: "tinyint", nullable: false),
                    DaysAmount = table.Column<decimal>(type: "money", nullable: false),
                    NightsWorked = table.Column<byte>(type: "tinyint", nullable: false),
                    NightsAmount = table.Column<decimal>(type: "money", nullable: false),
                    RegularHolidaysWorked = table.Column<byte>(type: "tinyint", nullable: false),
                    RegularHolidaysAmount = table.Column<decimal>(type: "money", nullable: false),
                    RegularHolidayNightsWorked = table.Column<byte>(type: "tinyint", nullable: false),
                    RegularHolidayNightsAmount = table.Column<decimal>(type: "money", nullable: false),
                    SpecialHolidaysWorked = table.Column<byte>(type: "tinyint", nullable: false),
                    SpecialHolidaysAmount = table.Column<decimal>(type: "money", nullable: false),
                    SpecialHolidayNightsWorked = table.Column<byte>(type: "tinyint", nullable: false),
                    SpecialHolidayNightsAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalBasic = table.Column<decimal>(type: "money", nullable: false),
                    RegularOT = table.Column<byte>(type: "tinyint", nullable: false),
                    RegularOTAmount = table.Column<decimal>(type: "money", nullable: false),
                    NightsOT = table.Column<byte>(type: "tinyint", nullable: false),
                    NightsOTAmount = table.Column<decimal>(type: "money", nullable: false),
                    RegularHolidayOT = table.Column<byte>(type: "tinyint", nullable: false),
                    RegularHolidayOTAmount = table.Column<decimal>(type: "money", nullable: false),
                    RegularHolidayNightOT = table.Column<byte>(type: "tinyint", nullable: false),
                    RegularHolidayOTNightAmount = table.Column<decimal>(type: "money", nullable: false),
                    SpecialHolidayOT = table.Column<byte>(type: "tinyint", nullable: false),
                    SpecialHolidayOTAmount = table.Column<decimal>(type: "money", nullable: false),
                    SpecialHolidayNightOT = table.Column<byte>(type: "tinyint", nullable: false),
                    SpecialHolidayNightOTAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalOT = table.Column<decimal>(type: "money", nullable: false),
                    LatesMinutes = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    LatesDeductionAmount = table.Column<decimal>(type: "money", nullable: false),
                    AllowancesAmount = table.Column<decimal>(type: "money", nullable: false),
                    BonusesAmount = table.Column<decimal>(type: "money", nullable: false),
                    TaxableIncome = table.Column<decimal>(type: "money", nullable: false),
                    TaxWithholdings = table.Column<decimal>(type: "money", nullable: false),
                    IncomeNetOfTax = table.Column<decimal>(type: "money", nullable: false),
                    SSSLoanAmount = table.Column<decimal>(type: "money", nullable: false),
                    PagIbigLoanAmount = table.Column<decimal>(type: "money", nullable: false),
                    DisposableIncome = table.Column<decimal>(type: "money", nullable: false),
                    CashAdvance = table.Column<decimal>(type: "money", nullable: false),
                    Vale = table.Column<decimal>(type: "money", nullable: false),
                    TotalCompanyLoans = table.Column<decimal>(type: "money", nullable: false),
                    AddAdjustments = table.Column<decimal>(type: "money", nullable: false),
                    SubtractAdjustments = table.Column<decimal>(type: "money", nullable: false),
                    NetPay = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Salaries_Payrolls_PayrollId",
                        column: x => x.PayrollId,
                        principalTable: "Payrolls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId",
                table: "Attendances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_EmployeeId",
                table: "Contributions",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_PayrollModelId",
                table: "Holidays",
                column: "PayrollModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_EmployeeId",
                table: "Leaves",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_PayrollId",
                table: "Salaries",
                column: "PayrollId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Contributions");

            migrationBuilder.DropTable(
                name: "ForgotPasswordRequests");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.DropTable(
                name: "NewUserRequests");

            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
