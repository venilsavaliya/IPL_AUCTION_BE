using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddAuctionMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuctionMode",
                table: "Auctions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 2, 9, 24, 59, 320, DateTimeKind.Utc).AddTicks(7077), "$2a$11$asgmQom5VefU6P7VSyRqUuzHFUTU0e4HQtJW/b5ENq.GIPTl799z." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuctionMode",
                table: "Auctions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 2, 5, 44, 0, 751, DateTimeKind.Utc).AddTicks(9915), "$2a$11$.lMZEMn363YJj1cFwf089OTEmUy.9VACyhmk/4bgBA5y.c.Qogtoa" });
        }
    }
}
