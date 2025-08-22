using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInSeason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSeasonStarted",
                table: "Seasons",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 22, 12, 58, 18, 188, DateTimeKind.Utc).AddTicks(2257), "$2a$11$IrSdJdXrwv4eeKCU67rO8.K34MO9c5ApxjwA4aa3pdTDmWqjjP2T6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSeasonStarted",
                table: "Seasons");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 21, 6, 59, 6, 174, DateTimeKind.Utc).AddTicks(9126), "$2a$11$F1cTRdMQVk7gjjgQyCn.Wud72iv2tAnFWZmFmh21McbCCjSVFpW0y" });
        }
    }
}
