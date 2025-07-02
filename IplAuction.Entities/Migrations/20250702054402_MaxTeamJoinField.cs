using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class MaxTeamJoinField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaximumTeamsCanJoin",
                table: "Auctions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 2, 5, 44, 0, 751, DateTimeKind.Utc).AddTicks(9915), "$2a$11$.lMZEMn363YJj1cFwf089OTEmUy.9VACyhmk/4bgBA5y.c.Qogtoa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumTeamsCanJoin",
                table: "Auctions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 6, 26, 8, 14, 14, 390, DateTimeKind.Utc).AddTicks(7340), "$2a$11$xBOPKNJxs33PXCnUXw.S1O.xIwJKJfwwdTFuYpJftN/HPP8Y5UYsS" });
        }
    }
}
