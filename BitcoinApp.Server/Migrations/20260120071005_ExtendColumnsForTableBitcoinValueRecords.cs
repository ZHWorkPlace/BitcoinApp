using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitcoinApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class ExtendColumnsForTableBitcoinValueRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BitcoinValue",
                table: "BitcoinValueRecords",
                newName: "ValueEur");

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRate",
                table: "BitcoinValueRecords",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueCzk",
                table: "BitcoinValueRecords",
                type: "decimal(8,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeRate",
                table: "BitcoinValueRecords");

            migrationBuilder.DropColumn(
                name: "ValueCzk",
                table: "BitcoinValueRecords");

            migrationBuilder.RenameColumn(
                name: "ValueEur",
                table: "BitcoinValueRecords",
                newName: "BitcoinValue");
        }
    }
}
