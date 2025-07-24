using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddInningStateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InningStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MatchId = table.Column<int>(type: "integer", nullable: false),
                    InningNumber = table.Column<int>(type: "integer", nullable: false),
                    StrikerId = table.Column<int>(type: "integer", nullable: false),
                    NonStrikerId = table.Column<int>(type: "integer", nullable: false),
                    BowlerId = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InningStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InningStates_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InningStates_Players_BowlerId",
                        column: x => x.BowlerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InningStates_Players_NonStrikerId",
                        column: x => x.NonStrikerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InningStates_Players_StrikerId",
                        column: x => x.StrikerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 24, 10, 0, 6, 585, DateTimeKind.Utc).AddTicks(9866), "$2a$11$ZIGWxw/9EnJ20njRpJ2HUuki9cLBkM3isQrJmJRZEG2YkPiLCtPJO" });

            migrationBuilder.CreateIndex(
                name: "IX_InningStates_BowlerId",
                table: "InningStates",
                column: "BowlerId");

            migrationBuilder.CreateIndex(
                name: "IX_InningStates_MatchId",
                table: "InningStates",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_InningStates_NonStrikerId",
                table: "InningStates",
                column: "NonStrikerId");

            migrationBuilder.CreateIndex(
                name: "IX_InningStates_StrikerId",
                table: "InningStates",
                column: "StrikerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InningStates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 24, 9, 46, 57, 264, DateTimeKind.Utc).AddTicks(431), "$2a$11$q39wMHPLlP2vA4zFj.ZU/uBn7jhi/h4zIksI/l33P3WjfHRIgA9iy" });
        }
    }
}
