using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurenncyExchange.Data.Migrations
{
    public partial class initialise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ammount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CurrencyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDetails_AccountDetails_AccountDetailsId",
                        column: x => x.AccountDetailsId,
                        principalTable: "AccountDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_AccountDetailsId",
                table: "TransactionDetails",
                column: "AccountDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDetails");

            migrationBuilder.DropTable(
                name: "AccountDetails");
        }
    }
}
