using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInInningstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BattingTeamId",
                table: "InningStates",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BowlingTeamId",
                table: "InningStates",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 28, 4, 35, 22, 394, DateTimeKind.Utc).AddTicks(3626), "$2a$11$sfhMmJOfPybhyN6HBhrA2u78jXii8JC4qZ3iRMuWmpSi8S1v2Yjjm" });

            migrationBuilder.CreateIndex(
                name: "IX_InningStates_BattingTeamId",
                table: "InningStates",
                column: "BattingTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_InningStates_BowlingTeamId",
                table: "InningStates",
                column: "BowlingTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_InningStates_Teams_BattingTeamId",
                table: "InningStates",
                column: "BattingTeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InningStates_Teams_BowlingTeamId",
                table: "InningStates",
                column: "BowlingTeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InningStates_Teams_BattingTeamId",
                table: "InningStates");

            migrationBuilder.DropForeignKey(
                name: "FK_InningStates_Teams_BowlingTeamId",
                table: "InningStates");

            migrationBuilder.DropIndex(
                name: "IX_InningStates_BattingTeamId",
                table: "InningStates");

            migrationBuilder.DropIndex(
                name: "IX_InningStates_BowlingTeamId",
                table: "InningStates");

            migrationBuilder.DropColumn(
                name: "BattingTeamId",
                table: "InningStates");

            migrationBuilder.DropColumn(
                name: "BowlingTeamId",
                table: "InningStates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 25, 13, 20, 26, 745, DateTimeKind.Utc).AddTicks(6041), "$2a$11$TH3/4NBv0jI3clSPKtdHt.ieQJxxQiYLN4MEuKD8E75wdUct35E9y" });
        }
    }
}
