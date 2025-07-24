using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddInningStateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 24, 9, 46, 57, 264, DateTimeKind.Utc).AddTicks(431), "$2a$11$q39wMHPLlP2vA4zFj.ZU/uBn7jhi/h4zIksI/l33P3WjfHRIgA9iy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 7, 14, 13, 22, 2, 704, DateTimeKind.Utc).AddTicks(5843), "$2a$11$AGVvGTQZ3AGWf1UmW1Q7Y.oKfOKRoMsYP/Verwm6EYXD1R9pfw3vy" });
        }
    }
}
