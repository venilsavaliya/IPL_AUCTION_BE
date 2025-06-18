using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddIsdeletedInTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Teams",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Teams");
        }
    }
}
