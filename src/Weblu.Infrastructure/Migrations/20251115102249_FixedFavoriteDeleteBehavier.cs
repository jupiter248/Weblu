using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedFavoriteDeleteBehavier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePortfolios_AspNetUsers_UserId",
                table: "FavoritePortfolios");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePortfolios_AspNetUsers_UserId",
                table: "FavoritePortfolios",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePortfolios_AspNetUsers_UserId",
                table: "FavoritePortfolios");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePortfolios_AspNetUsers_UserId",
                table: "FavoritePortfolios",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
