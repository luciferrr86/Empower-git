using Microsoft.EntityFrameworkCore.Migrations;

namespace A4.Empower.Migrations
{
    public partial class AddColumnsToEmployeeAndPersonalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AadhaarNumber",
                table: "EmployeePersonalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PanNumber",
                table: "EmployeePersonalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpCode",
                table: "Employee",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AadhaarNumber",
                table: "EmployeePersonalDetail");

            migrationBuilder.DropColumn(
                name: "PanNumber",
                table: "EmployeePersonalDetail");

            migrationBuilder.DropColumn(
                name: "EmpCode",
                table: "Employee");
        }
    }
}
