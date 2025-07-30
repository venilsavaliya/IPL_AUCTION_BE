using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerMatchState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerMatchStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    MatchId = table.Column<int>(type: "integer", nullable: false),
                    Fours = table.Column<int>(type: "integer", nullable: false),
                    Sixes = table.Column<int>(type: "integer", nullable: false),
                    Runs = table.Column<int>(type: "integer", nullable: false),
                    Wickets = table.Column<int>(type: "integer", nullable: false),
                    MaidenOvers = table.Column<int>(type: "integer", nullable: false),
                    Catches = table.Column<int>(type: "integer", nullable: false),
                    Stumpings = table.Column<int>(type: "integer", nullable: false),
                    RunOuts = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerMatchStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerMatchStates_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerMatchStates_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 30, 8, 16, 33, 499, DateTimeKind.Utc).AddTicks(1227), "$2a$11$x4PbUy6SdRVvkYLSnyFvAur3D6hYXdYCla3yesmg04/7rVxjrW9Iy" });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStates_MatchId",
                table: "PlayerMatchStates",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerMatchStates_PlayerId",
                table: "PlayerMatchStates",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerMatchStates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 28, 4, 35, 22, 394, DateTimeKind.Utc).AddTicks(3626), "$2a$11$sfhMmJOfPybhyN6HBhrA2u78jXii8JC4qZ3iRMuWmpSi8S1v2Yjjm" });
        }
    }
}
