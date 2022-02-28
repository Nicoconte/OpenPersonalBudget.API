using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenPersonalBudget.API.Migrations
{
    public partial class database_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concept",
                table: "Operations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Concept",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
