using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddSeasonTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "Matches",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeasonId",
                table: "Auctions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 31, 13, 23, 14, 429, DateTimeKind.Utc).AddTicks(7188), "$2a$11$jnvjBWdcfBvXM5rVFx6zP.sGbZHpRBMk3OglrlhIDCQVfoj944Jiq" });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SeasonId",
                table: "Matches",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_SeasonId",
                table: "Auctions",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Seasons_SeasonId",
                table: "Auctions",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Seasons_SeasonId",
                table: "Matches",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Seasons_SeasonId",
                table: "Auctions");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Seasons_SeasonId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropIndex(
                name: "IX_Matches_SeasonId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_SeasonId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "SeasonId",
                table: "Auctions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 30, 8, 52, 21, 588, DateTimeKind.Utc).AddTicks(2152), "$2a$11$B3DxruKtSLeCF2bQrroCi.1.hs6kI0Sd5BxL/845duW5gNHlqZayu" });
        }
    }
}
