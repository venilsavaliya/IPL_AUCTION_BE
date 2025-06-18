using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IplAuction.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsinAuction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_Users_CreatedBy",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_CreatedBy",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Auctions",
                newName: "MaximumPurseSize");

            migrationBuilder.AddColumn<int>(
                name: "PurseBalance",
                table: "AuctionParticipants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurseBalance",
                table: "AuctionParticipants");

            migrationBuilder.RenameColumn(
                name: "MaximumPurseSize",
                table: "Auctions",
                newName: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_CreatedBy",
                table: "Auctions",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_Users_CreatedBy",
                table: "Auctions",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
