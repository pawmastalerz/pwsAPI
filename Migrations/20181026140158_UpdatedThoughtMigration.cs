using Microsoft.EntityFrameworkCore.Migrations;

namespace pwsAPI.Migrations
{
    public partial class UpdatedThoughtMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Thoughts",
                newName: "Quote");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Thoughts",
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "Order",
                table: "Thoughts",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Thoughts");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Thoughts");

            migrationBuilder.RenameColumn(
                name: "Quote",
                table: "Thoughts",
                newName: "Description");
        }
    }
}
