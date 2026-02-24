using Microsoft.EntityFrameworkCore.Migrations;

namespace A4.Empower.Migrations
{
    public partial class AddNewPropertyToBlogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                schema: "dbo",
                table: "Blog",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                schema: "dbo",
                table: "Blog");
        }
    }
}
