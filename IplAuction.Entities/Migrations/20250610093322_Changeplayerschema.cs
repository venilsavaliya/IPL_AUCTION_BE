using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class Changeplayerschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Players",
                newName: "Skill");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Players",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Skill",
                table: "Players",
                newName: "Role");
        }
    }
}
