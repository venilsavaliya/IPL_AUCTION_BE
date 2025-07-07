using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddBidEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionPlayers_Auctions_AuctionId",
                table: "AuctionPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionPlayers_Players_PlayerId",
                table: "AuctionPlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionPlayers_Users_CurrentBidUserId",
                table: "AuctionPlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionPlayers",
                table: "AuctionPlayers");

            migrationBuilder.RenameTable(
                name: "AuctionPlayers",
                newName: "AuctionPlayer");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionPlayers_PlayerId",
                table: "AuctionPlayer",
                newName: "IX_AuctionPlayer_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionPlayers_CurrentBidUserId",
                table: "AuctionPlayer",
                newName: "IX_AuctionPlayer_CurrentBidUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionPlayers_AuctionId",
                table: "AuctionPlayer",
                newName: "IX_AuctionPlayer_AuctionId");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentPlayerId",
                table: "Auctions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionPlayer",
                table: "AuctionPlayer",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 7, 4, 48, 30, 487, DateTimeKind.Utc).AddTicks(4099), "$2a$11$bHT/PblYLMRHaTBXaLnwlu2tMb8J/xXxd6QIQxPP4MiIv.sPiG/mu" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionPlayer_Auctions_AuctionId",
                table: "AuctionPlayer",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionPlayer_Players_PlayerId",
                table: "AuctionPlayer",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionPlayer_Users_CurrentBidUserId",
                table: "AuctionPlayer",
                column: "CurrentBidUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionPlayer_Auctions_AuctionId",
                table: "AuctionPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionPlayer_Players_PlayerId",
                table: "AuctionPlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionPlayer_Users_CurrentBidUserId",
                table: "AuctionPlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionPlayer",
                table: "AuctionPlayer");

            migrationBuilder.RenameTable(
                name: "AuctionPlayer",
                newName: "AuctionPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionPlayer_PlayerId",
                table: "AuctionPlayers",
                newName: "IX_AuctionPlayers_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionPlayer_CurrentBidUserId",
                table: "AuctionPlayers",
                newName: "IX_AuctionPlayers_CurrentBidUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionPlayer_AuctionId",
                table: "AuctionPlayers",
                newName: "IX_AuctionPlayers_AuctionId");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentPlayerId",
                table: "Auctions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionPlayers",
                table: "AuctionPlayers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 4, 9, 2, 32, 645, DateTimeKind.Utc).AddTicks(2177), "$2a$11$PabXM/.VHjkpq33WH5/ILuWMsNSfY9V52iwsrFshwRb29EUUG1fWq" });

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionPlayers_Auctions_AuctionId",
                table: "AuctionPlayers",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionPlayers_Players_PlayerId",
                table: "AuctionPlayers",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionPlayers_Users_CurrentBidUserId",
                table: "AuctionPlayers",
                column: "CurrentBidUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
