using Microsoft.EntityFrameworkCore.Migrations;

namespace A4.Empower.Migrations
{
    public partial class CandidateInterview_SelectedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "TaskList",
                newName: "TaskListModel");

            migrationBuilder.AddColumn<bool>(
                name: "IsCandidateSelected",
                table: "JobCandidateInterview",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCandidateSelected",
                table: "JobCandidateInterview");

            migrationBuilder.RenameTable(
                name: "TaskListModel",
                newName: "TaskList");
        }
    }
}
