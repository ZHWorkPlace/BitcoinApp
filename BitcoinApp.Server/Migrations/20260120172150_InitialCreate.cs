using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BitcoinApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BitcoinValueRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RetrievedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ValueEur = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ValueCzk = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    ExchangeRate = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitcoinValueRecords", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BitcoinValueRecords");
        }
    }
}
