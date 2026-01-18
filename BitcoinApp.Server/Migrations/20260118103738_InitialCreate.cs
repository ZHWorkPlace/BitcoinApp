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
                    RetrievedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    BitcoinValue = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitcoinValueRecords", x => x.RetrievedAt);
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
