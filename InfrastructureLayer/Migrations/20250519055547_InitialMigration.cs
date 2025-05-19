using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasicHourlyRate = table.Column<decimal>(type: "money", nullable: false),
                    MaximumWeeklyHours = table.Column<byte>(type: "tinyint", nullable: false)
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
                    DefaultWorkShiftStart = table.Column<TimeOnly>(type: "time", nullable: false),
                    DefaultWorkShiftEnd = table.Column<TimeOnly>(type: "time", nullable: false),
                    DefaultBreakTimeStart = table.Column<TimeOnly>(type: "time", nullable: false),
                    DefaultBreakTimeEnd = table.Column<TimeOnly>(type: "time", nullable: false),
                    LeaveCredits = table.Column<byte>(type: "tinyint", nullable: false),
                    Absences = table.Column<byte>(type: "tinyint", nullable: false)
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
                name: "ContractorAccountInformationModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorAccountInformationModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorAccountInformationModel_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "ContractorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorAttendanceLogModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeIn = table.Column<TimeOnly>(type: "time", nullable: true),
                    TimeOut = table.Column<TimeOnly>(type: "time", nullable: true),
                    ReviewStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorAttendanceLogModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorAttendanceLogModel_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "ContractorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractorPayslipModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContractorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PeriodStart = table.Column<DateOnly>(type: "date", nullable: false),
                    PeriodEnd = table.Column<DateOnly>(type: "date", nullable: false),
                    PayDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AppliedHourlyRate = table.Column<decimal>(type: "money", nullable: false),
                    TotalHoursWorked = table.Column<byte>(type: "tinyint", nullable: false),
                    NetPay = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorPayslipModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorPayslipModel_Contractors_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractors",
                        principalColumn: "ContractorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAccountInfoModel",
                columns: table => new
                {
                    EmployeeAccountInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MiddleInitial = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Nationality = table.Column<byte>(type: "tinyint", nullable: false),
                    PrimaryPhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    SecondaryPhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MailingAddress = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    FacebookUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSSIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhilHealthIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PagIbigIdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    EmploymentType = table.Column<byte>(type: "tinyint", nullable: false),
                    DateHired = table.Column<DateOnly>(type: "date", nullable: false)
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
                name: "EmployeeAttendanceLogModel",
                columns: table => new
                {
                    EmployeeAttendanceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeStamp = table.Column<TimeOnly>(type: "time", nullable: false),
                    EventType = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendanceLogModel", x => x.EmployeeAttendanceId);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendanceLogModel_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttendanceRequestModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AttendanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeIn = table.Column<TimeOnly>(type: "time", nullable: false),
                    TimeOut = table.Column<TimeOnly>(type: "time", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendanceRequestModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendanceRequestModel_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEvaluatedAttendanceModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DayStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    ExpectedWorkHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualWorkHours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EvaluationTimeStamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeInReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BreakTimeInReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BreakTimeOutReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeOutReference = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvaluatedAttendanceModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluatedAttendanceModel_Employees_EmployeeId",
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
                    PayDate = table.Column<DateOnly>(type: "date", nullable: false),
                    AppliedHourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppliedNDRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppliedLegalHolidayRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AppliedOvertimeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HoursWorkedRegular = table.Column<short>(type: "smallint", nullable: false),
                    LegalHolidaysHours = table.Column<short>(type: "smallint", nullable: false),
                    NDOnWorkingDayHours = table.Column<short>(type: "smallint", nullable: false),
                    OTHoursWorkedRegular = table.Column<short>(type: "smallint", nullable: false),
                    UtilityAllowance = table.Column<decimal>(type: "money", nullable: false),
                    MealAllowance = table.Column<decimal>(type: "money", nullable: false),
                    LoadAllowance = table.Column<decimal>(type: "money", nullable: false),
                    TotalGrossPay = table.Column<decimal>(type: "money", nullable: false),
                    PHIC = table.Column<decimal>(type: "money", nullable: false),
                    HDMF = table.Column<decimal>(type: "money", nullable: false),
                    DecemberSSS = table.Column<decimal>(type: "money", nullable: false),
                    DecemberHDMF = table.Column<decimal>(type: "money", nullable: false),
                    WithholdingTax = table.Column<decimal>(type: "money", nullable: false),
                    TotalDeductions = table.Column<decimal>(type: "money", nullable: false),
                    NetPay = table.Column<decimal>(type: "money", nullable: false)
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
                name: "IX_ContractorAccountInformationModel_ContractorId",
                table: "ContractorAccountInformationModel",
                column: "ContractorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorAttendanceLogModel_ContractorId",
                table: "ContractorAttendanceLogModel",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorPayslipModel_ContractorId",
                table: "ContractorPayslipModel",
                column: "ContractorId");

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
                name: "IX_EmployeeAttendanceLogModel_EmployeeId",
                table: "EmployeeAttendanceLogModel",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendanceRequestModel_EmployeeId",
                table: "EmployeeAttendanceRequestModel",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluatedAttendanceModel_EmployeeId",
                table: "EmployeeEvaluatedAttendanceModel",
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
                name: "ContractorAccountInformationModel");

            migrationBuilder.DropTable(
                name: "ContractorAttendanceLogModel");

            migrationBuilder.DropTable(
                name: "ContractorPayslipModel");

            migrationBuilder.DropTable(
                name: "EmployeeAccountInfoModel");

            migrationBuilder.DropTable(
                name: "EmployeeAttendanceLogModel");

            migrationBuilder.DropTable(
                name: "EmployeeAttendanceRequestModel");

            migrationBuilder.DropTable(
                name: "EmployeeEvaluatedAttendanceModel");

            migrationBuilder.DropTable(
                name: "EmployeeLeaveModel");

            migrationBuilder.DropTable(
                name: "EmployeePayslipModel");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Contractors");

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
