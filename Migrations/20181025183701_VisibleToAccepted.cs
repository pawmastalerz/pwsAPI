using Microsoft.EntityFrameworkCore.Migrations;

namespace pwsAPI.Migrations
{
    public partial class VisibleToAccepted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Visible",
                table: "Thoughts",
                newName: "Accepted");

            migrationBuilder.RenameColumn(
                name: "Visible",
                table: "Posters",
                newName: "Accepted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Accepted",
                table: "Thoughts",
                newName: "Visible");

            migrationBuilder.RenameColumn(
                name: "Accepted",
                table: "Posters",
                newName: "Visible");
        }
    }
}
