using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedDbSetsForOtherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorAttendanceLogModel_Contractors_ContractorId",
                table: "ContractorAttendanceLogModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorPayslipModel_Contractors_ContractorId",
                table: "ContractorPayslipModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttendanceLogModel_Employees_EmployeeId",
                table: "EmployeeAttendanceLogModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttendanceRequestModel_Employees_EmployeeId",
                table: "EmployeeAttendanceRequestModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluatedAttendanceModel_Employees_EmployeeId",
                table: "EmployeeEvaluatedAttendanceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveModel_Employees_EmployeeId",
                table: "EmployeeLeaveModel");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayslipModel_Employees_EmployeeId",
                table: "EmployeePayslipModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeePayslipModel",
                table: "EmployeePayslipModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveModel",
                table: "EmployeeLeaveModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeEvaluatedAttendanceModel",
                table: "EmployeeEvaluatedAttendanceModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeAttendanceRequestModel",
                table: "EmployeeAttendanceRequestModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeAttendanceLogModel",
                table: "EmployeeAttendanceLogModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractorPayslipModel",
                table: "ContractorPayslipModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractorAttendanceLogModel",
                table: "ContractorAttendanceLogModel");

            migrationBuilder.RenameTable(
                name: "EmployeePayslipModel",
                newName: "EmployeePayslips");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveModel",
                newName: "EmployeeLeaves");

            migrationBuilder.RenameTable(
                name: "EmployeeEvaluatedAttendanceModel",
                newName: "EmployeeEvaluatedAttendances");

            migrationBuilder.RenameTable(
                name: "EmployeeAttendanceRequestModel",
                newName: "EmployeeAttendanceRequests");

            migrationBuilder.RenameTable(
                name: "EmployeeAttendanceLogModel",
                newName: "EmployeeAttendanceLogs");

            migrationBuilder.RenameTable(
                name: "ContractorPayslipModel",
                newName: "ContractorPayslips");

            migrationBuilder.RenameTable(
                name: "ContractorAttendanceLogModel",
                newName: "ContractorAttendanceLogs");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePayslipModel_EmployeeId",
                table: "EmployeePayslips",
                newName: "IX_EmployeePayslips_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveModel_EmployeeId",
                table: "EmployeeLeaves",
                newName: "IX_EmployeeLeaves_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeEvaluatedAttendanceModel_EmployeeId",
                table: "EmployeeEvaluatedAttendances",
                newName: "IX_EmployeeEvaluatedAttendances_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeAttendanceRequestModel_EmployeeId",
                table: "EmployeeAttendanceRequests",
                newName: "IX_EmployeeAttendanceRequests_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeAttendanceLogModel_EmployeeId",
                table: "EmployeeAttendanceLogs",
                newName: "IX_EmployeeAttendanceLogs_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorPayslipModel_ContractorId",
                table: "ContractorPayslips",
                newName: "IX_ContractorPayslips_ContractorId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorAttendanceLogModel_ContractorId",
                table: "ContractorAttendanceLogs",
                newName: "IX_ContractorAttendanceLogs_ContractorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeePayslips",
                table: "EmployeePayslips",
                column: "EmployeePayslipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaves",
                table: "EmployeeLeaves",
                column: "EmployeeLeaveId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeEvaluatedAttendances",
                table: "EmployeeEvaluatedAttendances",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeAttendanceRequests",
                table: "EmployeeAttendanceRequests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeAttendanceLogs",
                table: "EmployeeAttendanceLogs",
                column: "EmployeeAttendanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractorPayslips",
                table: "ContractorPayslips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractorAttendanceLogs",
                table: "ContractorAttendanceLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorAttendanceLogs_Contractors_ContractorId",
                table: "ContractorAttendanceLogs",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorPayslips_Contractors_ContractorId",
                table: "ContractorPayslips",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttendanceLogs_Employees_EmployeeId",
                table: "EmployeeAttendanceLogs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttendanceRequests_Employees_EmployeeId",
                table: "EmployeeAttendanceRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluatedAttendances_Employees_EmployeeId",
                table: "EmployeeEvaluatedAttendances",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaves_Employees_EmployeeId",
                table: "EmployeeLeaves",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayslips_Employees_EmployeeId",
                table: "EmployeePayslips",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorAttendanceLogs_Contractors_ContractorId",
                table: "ContractorAttendanceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorPayslips_Contractors_ContractorId",
                table: "ContractorPayslips");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttendanceLogs_Employees_EmployeeId",
                table: "EmployeeAttendanceLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttendanceRequests_Employees_EmployeeId",
                table: "EmployeeAttendanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEvaluatedAttendances_Employees_EmployeeId",
                table: "EmployeeEvaluatedAttendances");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaves_Employees_EmployeeId",
                table: "EmployeeLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayslips_Employees_EmployeeId",
                table: "EmployeePayslips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeePayslips",
                table: "EmployeePayslips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaves",
                table: "EmployeeLeaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeEvaluatedAttendances",
                table: "EmployeeEvaluatedAttendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeAttendanceRequests",
                table: "EmployeeAttendanceRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeAttendanceLogs",
                table: "EmployeeAttendanceLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractorPayslips",
                table: "ContractorPayslips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractorAttendanceLogs",
                table: "ContractorAttendanceLogs");

            migrationBuilder.RenameTable(
                name: "EmployeePayslips",
                newName: "EmployeePayslipModel");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaves",
                newName: "EmployeeLeaveModel");

            migrationBuilder.RenameTable(
                name: "EmployeeEvaluatedAttendances",
                newName: "EmployeeEvaluatedAttendanceModel");

            migrationBuilder.RenameTable(
                name: "EmployeeAttendanceRequests",
                newName: "EmployeeAttendanceRequestModel");

            migrationBuilder.RenameTable(
                name: "EmployeeAttendanceLogs",
                newName: "EmployeeAttendanceLogModel");

            migrationBuilder.RenameTable(
                name: "ContractorPayslips",
                newName: "ContractorPayslipModel");

            migrationBuilder.RenameTable(
                name: "ContractorAttendanceLogs",
                newName: "ContractorAttendanceLogModel");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeePayslips_EmployeeId",
                table: "EmployeePayslipModel",
                newName: "IX_EmployeePayslipModel_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaves_EmployeeId",
                table: "EmployeeLeaveModel",
                newName: "IX_EmployeeLeaveModel_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeEvaluatedAttendances_EmployeeId",
                table: "EmployeeEvaluatedAttendanceModel",
                newName: "IX_EmployeeEvaluatedAttendanceModel_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeAttendanceRequests_EmployeeId",
                table: "EmployeeAttendanceRequestModel",
                newName: "IX_EmployeeAttendanceRequestModel_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeAttendanceLogs_EmployeeId",
                table: "EmployeeAttendanceLogModel",
                newName: "IX_EmployeeAttendanceLogModel_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorPayslips_ContractorId",
                table: "ContractorPayslipModel",
                newName: "IX_ContractorPayslipModel_ContractorId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorAttendanceLogs_ContractorId",
                table: "ContractorAttendanceLogModel",
                newName: "IX_ContractorAttendanceLogModel_ContractorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeePayslipModel",
                table: "EmployeePayslipModel",
                column: "EmployeePayslipId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveModel",
                table: "EmployeeLeaveModel",
                column: "EmployeeLeaveId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeEvaluatedAttendanceModel",
                table: "EmployeeEvaluatedAttendanceModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeAttendanceRequestModel",
                table: "EmployeeAttendanceRequestModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeAttendanceLogModel",
                table: "EmployeeAttendanceLogModel",
                column: "EmployeeAttendanceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractorPayslipModel",
                table: "ContractorPayslipModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractorAttendanceLogModel",
                table: "ContractorAttendanceLogModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorAttendanceLogModel_Contractors_ContractorId",
                table: "ContractorAttendanceLogModel",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorPayslipModel_Contractors_ContractorId",
                table: "ContractorPayslipModel",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "ContractorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttendanceLogModel_Employees_EmployeeId",
                table: "EmployeeAttendanceLogModel",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttendanceRequestModel_Employees_EmployeeId",
                table: "EmployeeAttendanceRequestModel",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEvaluatedAttendanceModel_Employees_EmployeeId",
                table: "EmployeeEvaluatedAttendanceModel",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveModel_Employees_EmployeeId",
                table: "EmployeeLeaveModel",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayslipModel_Employees_EmployeeId",
                table: "EmployeePayslipModel",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
