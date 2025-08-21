using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldInAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReshuffled",
                table: "Auctions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 21, 5, 46, 54, 219, DateTimeKind.Utc).AddTicks(345), "$2a$11$SpvxLVRdEl/5NTmFKBRsoeognHdhVRlHoln/3DT5Wm9s1sUixes9G" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReshuffled",
                table: "Auctions");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 20, 8, 48, 33, 664, DateTimeKind.Utc).AddTicks(7080), "$2a$11$2NMZKk86ggnUl68t74i15OXwSEOcmgqwA3xWyQOEt.Iv4pOGq2cAG" });
        }
    }
}
