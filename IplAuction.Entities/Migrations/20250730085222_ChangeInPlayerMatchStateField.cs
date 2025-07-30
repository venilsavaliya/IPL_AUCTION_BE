using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class ChangeInPlayerMatchStateField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "PlayerMatchStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 30, 8, 52, 21, 588, DateTimeKind.Utc).AddTicks(2152), "$2a$11$B3DxruKtSLeCF2bQrroCi.1.hs6kI0Sd5BxL/845duW5gNHlqZayu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "PlayerMatchStates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 30, 8, 16, 33, 499, DateTimeKind.Utc).AddTicks(1227), "$2a$11$x4PbUy6SdRVvkYLSnyFvAur3D6hYXdYCla3yesmg04/7rVxjrW9Iy" });
        }
    }
}
