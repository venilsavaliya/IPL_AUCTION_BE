using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTeam_Auctions_AuctionId",
                table: "UserTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeam_Players_PlayerId",
                table: "UserTeam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeam_Users_UserId",
                table: "UserTeam");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeam",
                table: "UserTeam");

            migrationBuilder.RenameTable(
                name: "UserTeam",
                newName: "UserTeams");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeam_UserId",
                table: "UserTeams",
                newName: "IX_UserTeams_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeam_PlayerId",
                table: "UserTeams",
                newName: "IX_UserTeams_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeam_AuctionId",
                table: "UserTeams",
                newName: "IX_UserTeams_AuctionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeams",
                table: "UserTeams",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 7, 11, 24, 15, 905, DateTimeKind.Utc).AddTicks(9190), "$2a$11$Qnm/x2OFeWRRRYbNVtJdneoazEsEOrDJ6zNPL2AsTOC2R7LZVNSgi" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Auctions_AuctionId",
                table: "UserTeams",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Players_PlayerId",
                table: "UserTeams",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeams_Users_UserId",
                table: "UserTeams",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Auctions_AuctionId",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Players_PlayerId",
                table: "UserTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTeams_Users_UserId",
                table: "UserTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTeams",
                table: "UserTeams");

            migrationBuilder.RenameTable(
                name: "UserTeams",
                newName: "UserTeam");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeams_UserId",
                table: "UserTeam",
                newName: "IX_UserTeam_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeams_PlayerId",
                table: "UserTeam",
                newName: "IX_UserTeam_PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTeams_AuctionId",
                table: "UserTeam",
                newName: "IX_UserTeam_AuctionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTeam",
                table: "UserTeam",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 7, 5, 4, 33, 940, DateTimeKind.Utc).AddTicks(1972), "$2a$11$Ebx00gvpCeu5PKIJQ1J6s.a4svHnoLy3BkFUCLB3wEdDzAWLPMaZu" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeam_Auctions_AuctionId",
                table: "UserTeam",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeam_Players_PlayerId",
                table: "UserTeam",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTeam_Users_UserId",
                table: "UserTeam",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
