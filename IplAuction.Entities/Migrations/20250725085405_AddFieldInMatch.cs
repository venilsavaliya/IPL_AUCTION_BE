using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InningNumber",
                table: "Matches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 25, 8, 54, 3, 461, DateTimeKind.Utc).AddTicks(5048), "$2a$11$5X3Ayrst87kKJgh8KltpuuN3blcVjcXhMtxFLdZ1JAGIVWuCprBqu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InningNumber",
                table: "Matches");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 24, 10, 0, 6, 585, DateTimeKind.Utc).AddTicks(9866), "$2a$11$ZIGWxw/9EnJ20njRpJ2HUuki9cLBkM3isQrJmJRZEG2YkPiLCtPJO" });
        }
    }
}
