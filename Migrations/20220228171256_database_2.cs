using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenPersonalBudget.API.Migrations
{
    public partial class database_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Money",
                table: "AccountBalances",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "AccountBalances",
                newName: "Money");
        }
    }
}
