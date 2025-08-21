using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInUserTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReshuffled",
                table: "UserTeams",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ReshuffledStatus",
                table: "UserTeams",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 21, 6, 59, 6, 174, DateTimeKind.Utc).AddTicks(9126), "$2a$11$F1cTRdMQVk7gjjgQyCn.Wud72iv2tAnFWZmFmh21McbCCjSVFpW0y" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReshuffled",
                table: "UserTeams");

            migrationBuilder.DropColumn(
                name: "ReshuffledStatus",
                table: "UserTeams");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 21, 5, 46, 54, 219, DateTimeKind.Utc).AddTicks(345), "$2a$11$SpvxLVRdEl/5NTmFKBRsoeognHdhVRlHoln/3DT5Wm9s1sUixes9G" });
        }
    }
}
