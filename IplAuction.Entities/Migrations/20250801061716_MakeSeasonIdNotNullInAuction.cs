using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class MakeSeasonIdNotNullInAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Seasons_SeasonId",
                table: "Auctions");

            migrationBuilder.AlterColumn<int>(
                name: "SeasonId",
                table: "Auctions",
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
                values: new object[] { new DateTime(2025, 8, 1, 6, 17, 13, 857, DateTimeKind.Utc).AddTicks(7503), "$2a$11$k7yI9KbIAjNDM8MWglhtgulppLGlo92an5WOzARbE8r78rZKuGJZO" });

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Seasons_SeasonId",
                table: "Auctions",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Seasons_SeasonId",
                table: "Auctions");

            migrationBuilder.AlterColumn<int>(
                name: "SeasonId",
                table: "Auctions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 31, 13, 23, 14, 429, DateTimeKind.Utc).AddTicks(7188), "$2a$11$jnvjBWdcfBvXM5rVFx6zP.sGbZHpRBMk3OglrlhIDCQVfoj944Jiq" });

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Seasons_SeasonId",
                table: "Auctions",
                column: "SeasonId",
                principalTable: "Seasons",
                principalColumn: "Id");
        }
    }
}
