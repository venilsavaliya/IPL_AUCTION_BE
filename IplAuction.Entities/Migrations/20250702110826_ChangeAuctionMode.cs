using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAuctionMode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuctionMode",
                table: "Auctions");

            migrationBuilder.AddColumn<bool>(
                name: "ModeOfAuction",
                table: "Auctions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 2, 11, 8, 25, 358, DateTimeKind.Utc).AddTicks(7925), "$2a$11$1ECSpWm1fCXfNDGDTrswIOxYXtKCgu906CVRKyXMzeLoOukpH94mO" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModeOfAuction",
                table: "Auctions");

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
    }
}
