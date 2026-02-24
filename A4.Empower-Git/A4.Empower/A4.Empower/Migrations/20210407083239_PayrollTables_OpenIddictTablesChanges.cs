using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace A4.Empower.Migrations
{
    public partial class PayrollTables_OpenIddictTablesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictTokens",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "OpenIddictTokens",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OpenIddictTokens",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceId",
                table: "OpenIddictTokens",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ExpirationDate",
                table: "OpenIddictTokens",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "OpenIddictTokens",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictTokens",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RedemptionDate",
                table: "OpenIddictTokens",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OpenIddictScopes",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictScopes",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descriptions",
                table: "OpenIddictScopes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayNames",
                table: "OpenIddictScopes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictAuthorizations",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "OpenIddictAuthorizations",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OpenIddictAuthorizations",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictAuthorizations",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "OpenIddictAuthorizations",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictApplications",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ConsentType",
                table: "OpenIddictApplications",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictApplications",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "OpenIddictApplications",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DisplayNames",
                table: "OpenIddictApplications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Requirements",
                table: "OpenIddictApplications",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeCode",
                table: "Employee",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "AttendenceSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    DaysWorked = table.Column<decimal>(nullable: false),
                    LeaveTaken = table.Column<int>(nullable: false),
                    OnDuty = table.Column<int>(nullable: false),
                    WeeklyOff = table.Column<int>(nullable: false),
                    Holidays = table.Column<int>(nullable: false),
                    Unpaid = table.Column<int>(nullable: false),
                    PaidDays = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendenceSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendenceSummary_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttendence",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PunchIn = table.Column<string>(nullable: true),
                    PunchOut = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    LeaveType = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendence_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeByLevel",
                columns: table => new
                {
                    EmpId = table.Column<Guid>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    EmpLevel = table.Column<int>(nullable: false),
                    Root = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FunctionalDesignation = table.Column<string>(nullable: true),
                    FunctionalGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "EmployeeCtc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CTC = table.Column<decimal>(nullable: false),
                    BasicSalary = table.Column<decimal>(nullable: false),
                    DA = table.Column<decimal>(nullable: false),
                    HRA = table.Column<decimal>(nullable: false),
                    Conveyance = table.Column<decimal>(nullable: false),
                    MedicalExpenses = table.Column<decimal>(nullable: false),
                    Special = table.Column<decimal>(nullable: false),
                    Bonus = table.Column<decimal>(nullable: false),
                    ContributionToPf = table.Column<decimal>(nullable: false),
                    ProfessionTax = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeCtc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeCtc_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpPerformanceGoalMeasure",
                columns: table => new
                {
                    QuadID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ExpenseApproveByLevel",
                columns: table => new
                {
                    EmpID = table.Column<Guid>(nullable: false),
                    ManagerID = table.Column<Guid>(nullable: false),
                    EmpLevel = table.Column<int>(nullable: false),
                    TitleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SalaryComponent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsEarnings = table.Column<bool>(nullable: false),
                    IsMonthly = table.Column<bool>(nullable: false),
                    IsAllYearly = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskList",
                columns: table => new
                {
                    EmpId = table.Column<string>(nullable: true),
                    Task = table.Column<string>(nullable: true),
                    RedirectTo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    TotalDaysOfMonth = table.Column<decimal>(nullable: false),
                    AllowedLeave = table.Column<decimal>(nullable: false),
                    LeaveTaken = table.Column<decimal>(nullable: false),
                    WorkedDays = table.Column<decimal>(nullable: false),
                    BasicSalary = table.Column<decimal>(nullable: false),
                    DA = table.Column<decimal>(nullable: false),
                    HRA = table.Column<decimal>(nullable: false),
                    Conveyance = table.Column<decimal>(nullable: false),
                    MedicalExpenses = table.Column<decimal>(nullable: false),
                    Special = table.Column<decimal>(nullable: false),
                    Bonus = table.Column<decimal>(nullable: false),
                    TA = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    ContributionToPf = table.Column<decimal>(nullable: false),
                    ProfessionTax = table.Column<decimal>(nullable: false),
                    TDS = table.Column<decimal>(nullable: false),
                    SalaryAdvance = table.Column<decimal>(nullable: false),
                    SalaryTotal = table.Column<decimal>(nullable: false),
                    NetPayable = table.Column<decimal>(nullable: false),
                    MedicalBillAmount = table.Column<decimal>(nullable: false),
                    UnpaidDays = table.Column<decimal>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    EmployeeCtcId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeSalary_EmployeeCtc_EmployeeCtcId",
                        column: x => x.EmployeeCtcId,
                        principalTable: "EmployeeCtc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalary_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CtcOtherComponent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    EmployeeCtcId = table.Column<int>(nullable: false),
                    SalaryComponentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CtcOtherComponent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CtcOtherComponent_EmployeeCtc_EmployeeCtcId",
                        column: x => x.EmployeeCtcId,
                        principalTable: "EmployeeCtc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CtcOtherComponent_SalaryComponent_SalaryComponentId",
                        column: x => x.SalaryComponentId,
                        principalTable: "SalaryComponent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaryPart",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    EmployeeSalaryId = table.Column<int>(nullable: false),
                    CtcOtherComponentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryPart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryPart_CtcOtherComponent_CtcOtherComponentId",
                        column: x => x.CtcOtherComponentId,
                        principalTable: "CtcOtherComponent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaryPart_EmployeeSalary_EmployeeSalaryId",
                        column: x => x.EmployeeSalaryId,
                        principalTable: "EmployeeSalary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations",
                columns: new[] { "ApplicationId", "Status", "Subject", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true,
                filter: "[ClientId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttendenceSummary_EmployeeId",
                table: "AttendenceSummary",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CtcOtherComponent_EmployeeCtcId",
                table: "CtcOtherComponent",
                column: "EmployeeCtcId");

            migrationBuilder.CreateIndex(
                name: "IX_CtcOtherComponent_SalaryComponentId",
                table: "CtcOtherComponent",
                column: "SalaryComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendence_EmployeeId",
                table: "EmployeeAttendence",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeCtc_EmployeeId",
                table: "EmployeeCtc",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalary_EmployeeCtcId",
                table: "EmployeeSalary",
                column: "EmployeeCtcId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalary_EmployeeId",
                table: "EmployeeSalary",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPart_CtcOtherComponentId",
                table: "SalaryPart",
                column: "CtcOtherComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryPart_EmployeeSalaryId",
                table: "SalaryPart",
                column: "EmployeeSalaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendenceSummary");

            migrationBuilder.DropTable(
                name: "EmployeeAttendence");

            migrationBuilder.DropTable(
                name: "EmployeeByLevel");

            migrationBuilder.DropTable(
                name: "EmpPerformanceGoalMeasure");

            migrationBuilder.DropTable(
                name: "ExpenseApproveByLevel");

            migrationBuilder.DropTable(
                name: "SalaryPart");

            migrationBuilder.DropTable(
                name: "TaskList");

            migrationBuilder.DropTable(
                name: "CtcOtherComponent");

            migrationBuilder.DropTable(
                name: "EmployeeSalary");

            migrationBuilder.DropTable(
                name: "SalaryComponent");

            migrationBuilder.DropTable(
                name: "EmployeeCtc");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type",
                table: "OpenIddictTokens");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications");

            migrationBuilder.DropColumn(
                name: "RedemptionDate",
                table: "OpenIddictTokens");

            migrationBuilder.DropColumn(
                name: "Descriptions",
                table: "OpenIddictScopes");

            migrationBuilder.DropColumn(
                name: "DisplayNames",
                table: "OpenIddictScopes");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "OpenIddictAuthorizations");

            migrationBuilder.DropColumn(
                name: "DisplayNames",
                table: "OpenIddictApplications");

            migrationBuilder.DropColumn(
                name: "Requirements",
                table: "OpenIddictApplications");

            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "OpenIddictTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OpenIddictTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceId",
                table: "OpenIddictTokens",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "ExpirationDate",
                table: "OpenIddictTokens",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreationDate",
                table: "OpenIddictTokens",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictTokens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OpenIddictScopes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictScopes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictAuthorizations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "OpenIddictAuthorizations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "OpenIddictAuthorizations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictAuthorizations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "OpenIddictApplications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConsentType",
                table: "OpenIddictApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ConcurrencyToken",
                table: "OpenIddictApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "OpenIddictApplications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                table: "OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                table: "OpenIddictScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true);
        }
    }
}
