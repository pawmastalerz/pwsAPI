using Microsoft.EntityFrameworkCore.Migrations;

namespace pwsAPI.Migrations
{
    public partial class PosterVisibility : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Visible",
                table: "Posters",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Posters");
        }
    }
}
