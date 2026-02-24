using Microsoft.EntityFrameworkCore.Migrations;

namespace A4.Empower.Migrations
{
    public partial class ExcelCandidateDataTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelCandidateData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateName = table.Column<string>(nullable: true),
                    JobName = table.Column<string>(nullable: true),
                    JobTitle = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    Level1ManagerId = table.Column<int>(nullable: true),
                    Level1Result = table.Column<string>(nullable: true),
                    Level2ManagerId = table.Column<int>(nullable: true),
                    Level2Result = table.Column<string>(nullable: true),
                    Level3ManagerId = table.Column<int>(nullable: true),
                    Level3Result = table.Column<string>(nullable: true),
                    Level4ManagerId = table.Column<int>(nullable: true),
                    Level4Result = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelCandidateData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelCandidateData");
        }
    }
}
