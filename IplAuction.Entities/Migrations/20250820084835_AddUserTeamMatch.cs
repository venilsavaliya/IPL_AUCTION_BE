using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTeamMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ScoringRules",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.CreateTable(
                name: "UserTeamMatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    AuctionId = table.Column<int>(type: "integer", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTeamMatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTeamMatches_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeamMatches_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeamMatches_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTeamMatches_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 20, 8, 48, 33, 664, DateTimeKind.Utc).AddTicks(7080), "$2a$11$2NMZKk86ggnUl68t74i15OXwSEOcmgqwA3xWyQOEt.Iv4pOGq2cAG" });

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamMatches_AuctionId",
                table: "UserTeamMatches",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamMatches_MatchId",
                table: "UserTeamMatches",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamMatches_PlayerId",
                table: "UserTeamMatches",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTeamMatches_UserId",
                table: "UserTeamMatches",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTeamMatches");

            migrationBuilder.InsertData(
                table: "ScoringRules",
                columns: new[] { "Id", "EventType", "Points" },
                values: new object[,]
                {
                    { 4, "HalfCentury", 4 },
                    { 5, "Century", 8 },
                    { 6, "Duck", -2 },
                    { 8, "ThreeCatchHaul", 4 },
                    { 10, "DirectRunOut", 12 },
                    { 11, "AssistedRunOut", 6 },
                    { 13, "ThreeWicketHaul", 4 },
                    { 14, "FourWicketHaul", 8 },
                    { 15, "FiveWicketHaul", 16 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 18, 9, 15, 27, 237, DateTimeKind.Utc).AddTicks(7793), "$2a$11$y9UL.6rJnwRpysqSCrPckubR7iIbKUVX9qtLpJs1NHc8uSCrF7eca" });
        }
    }
}
