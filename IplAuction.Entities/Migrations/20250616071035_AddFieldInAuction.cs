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
            migrationBuilder.DropColumn(
                name: "CurrentBid",
                table: "AuctionPlayer");

            migrationBuilder.AddColumn<int>(
                name: "CurrentBid",
                table: "Auctions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayerId",
                table: "Auctions",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBid",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerId",
                table: "Auctions");

            migrationBuilder.AddColumn<int>(
                name: "CurrentBid",
                table: "AuctionPlayer",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
