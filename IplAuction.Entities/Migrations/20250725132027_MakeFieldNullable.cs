using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class MakeFieldNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InningStates_Players_BowlerId",
                table: "InningStates");

            migrationBuilder.DropForeignKey(
                name: "FK_InningStates_Players_NonStrikerId",
                table: "InningStates");

            migrationBuilder.DropForeignKey(
                name: "FK_InningStates_Players_StrikerId",
                table: "InningStates");

            migrationBuilder.AlterColumn<int>(
                name: "StrikerId",
                table: "InningStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "NonStrikerId",
                table: "InningStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BowlerId",
                table: "InningStates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 25, 13, 20, 26, 745, DateTimeKind.Utc).AddTicks(6041), "$2a$11$TH3/4NBv0jI3clSPKtdHt.ieQJxxQiYLN4MEuKD8E75wdUct35E9y" });

            migrationBuilder.AddForeignKey(
                name: "FK_InningStates_Players_BowlerId",
                table: "InningStates",
                column: "BowlerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InningStates_Players_NonStrikerId",
                table: "InningStates",
                column: "NonStrikerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InningStates_Players_StrikerId",
                table: "InningStates",
                column: "StrikerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InningStates_Players_BowlerId",
                table: "InningStates");

            migrationBuilder.DropForeignKey(
                name: "FK_InningStates_Players_NonStrikerId",
                table: "InningStates");

            migrationBuilder.DropForeignKey(
                name: "FK_InningStates_Players_StrikerId",
                table: "InningStates");

            migrationBuilder.AlterColumn<int>(
                name: "StrikerId",
                table: "InningStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NonStrikerId",
                table: "InningStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BowlerId",
                table: "InningStates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 25, 8, 54, 3, 461, DateTimeKind.Utc).AddTicks(5048), "$2a$11$5X3Ayrst87kKJgh8KltpuuN3blcVjcXhMtxFLdZ1JAGIVWuCprBqu" });

            migrationBuilder.AddForeignKey(
                name: "FK_InningStates_Players_BowlerId",
                table: "InningStates",
                column: "BowlerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InningStates_Players_NonStrikerId",
                table: "InningStates",
                column: "NonStrikerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InningStates_Players_StrikerId",
                table: "InningStates",
                column: "StrikerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
