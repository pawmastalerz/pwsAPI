using Microsoft.EntityFrameworkCore.Migrations;

namespace pwsAPI.Migrations
{
    public partial class ThoughtWithoutOrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Thoughts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Order",
                table: "Thoughts",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
