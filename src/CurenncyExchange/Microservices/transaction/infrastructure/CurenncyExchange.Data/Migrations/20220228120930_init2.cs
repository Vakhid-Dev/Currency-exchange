using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurenncyExchange.Transaction.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyType",
                table: "CurrencyDetails",
                type: "nvarchar(30)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyType",
                table: "CurrencyDetails");
        }
    }
}
