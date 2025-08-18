using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderNumberField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "PlayerMatchStates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 18, 9, 15, 27, 237, DateTimeKind.Utc).AddTicks(7793), "$2a$11$y9UL.6rJnwRpysqSCrPckubR7iIbKUVX9qtLpJs1NHc8uSCrF7eca" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "PlayerMatchStates");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2025, 8, 5, 11, 35, 44, 398, DateTimeKind.Utc).AddTicks(7432), "$2a$11$8MXTKo0x4/IBcf9Yfpxt5eZ9ASiUldkW/dLMfecGDucVBUQKVbIcK" });
        }
    }
}
