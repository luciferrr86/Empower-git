using Microsoft.EntityFrameworkCore.Migrations;

namespace A4.Empower.Migrations
{
    public partial class AddMoreFieldsToPersonalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<string>(
                name: "CurrentCity",
                table: "EmployeePersonalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentCountry",
                table: "EmployeePersonalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentState",
                table: "EmployeePersonalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentZipCode",
                table: "EmployeePersonalDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentCity",
                table: "EmployeePersonalDetail");

            migrationBuilder.DropColumn(
                name: "CurrentCountry",
                table: "EmployeePersonalDetail");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "EmployeePersonalDetail");

            migrationBuilder.DropColumn(
                name: "CurrentZipCode",
                table: "EmployeePersonalDetail");

        }
    }
}
