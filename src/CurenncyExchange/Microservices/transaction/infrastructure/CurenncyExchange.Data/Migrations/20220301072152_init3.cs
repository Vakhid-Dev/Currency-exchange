using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurenncyExchange.Transaction.Data.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ammount",
                table: "CurrencyDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ammount",
                table: "CurrencyDetails");
        }
    }
}
