using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddBidsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Auctions_AuctionId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Players_PlayerId",
                table: "Bid");

            migrationBuilder.DropForeignKey(
                name: "FK_Bid_Users_UserId",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bid",
                table: "Bid");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionPlayer",
                table: "AuctionPlayer");

            migrationBuilder.RenameTable(
                name: "Bid",
                newName: "Bids");

            migrationBuilder.RenameTable(
                name: "AuctionPlayer",
                newName: "AuctionPlayers");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_UserId",
                table: "Bids",
                newName: "IX_Bids_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_PlayerId",
                table: "Bids",
                newName: "IX_Bids_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bid_AuctionId",
                table: "Bids",
                newName: "IX_Bids_AuctionId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bids",
                table: "Bids",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuctionPlayers",
                table: "AuctionPlayers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 7, 5, 4, 33, 940, DateTimeKind.Utc).AddTicks(1972), "$2a$11$Ebx00gvpCeu5PKIJQ1J6s.a4svHnoLy3BkFUCLB3wEdDzAWLPMaZu" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Players_PlayerId",
                table: "Bids",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Auctions_AuctionId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Players_PlayerId",
                table: "Bids");

            migrationBuilder.DropForeignKey(
                name: "FK_Bids_Users_UserId",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bids",
                table: "Bids");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuctionPlayers",
                table: "AuctionPlayers");

            migrationBuilder.RenameTable(
                name: "Bids",
                newName: "Bid");

            migrationBuilder.RenameTable(
                name: "AuctionPlayers",
                newName: "AuctionPlayer");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_UserId",
                table: "Bid",
                newName: "IX_Bid_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_PlayerId",
                table: "Bid",
                newName: "IX_Bid_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bids_AuctionId",
                table: "Bid",
                newName: "IX_Bid_AuctionId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bid",
                table: "Bid",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Auctions_AuctionId",
                table: "Bid",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Players_PlayerId",
                table: "Bid",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bid_Users_UserId",
                table: "Bid",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
