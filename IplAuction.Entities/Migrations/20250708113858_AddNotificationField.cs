using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotificationOn",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsNotificationOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 8, 11, 38, 57, 634, DateTimeKind.Utc).AddTicks(4770), true, "$2a$11$2R3fwYGieHeNjgWByOZad.W4an/Acsr238QXFAjRt.a9tOQV6nZZu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotificationOn",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 8, 9, 31, 43, 427, DateTimeKind.Utc).AddTicks(8603), "$2a$11$qlCVMMsMyeyV6rqXPezvO.mR.x2fH08514OKGaZ2evXoHb.vrdO8G" });
        }
    }
}
