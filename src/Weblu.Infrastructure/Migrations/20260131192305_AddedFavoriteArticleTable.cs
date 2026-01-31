using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFavoriteArticleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavoriteArticleId",
                table: "FavoriteLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoriteArticles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteArticles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteArticles_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteArticles_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteLists_FavoriteArticleId",
                table: "FavoriteLists",
                column: "FavoriteArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteArticles_AppUserId",
                table: "FavoriteArticles",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteArticles_ArticleId",
                table: "FavoriteArticles",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteLists_FavoriteArticles_FavoriteArticleId",
                table: "FavoriteLists",
                column: "FavoriteArticleId",
                principalTable: "FavoriteArticles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteLists_FavoriteArticles_FavoriteArticleId",
                table: "FavoriteLists");

            migrationBuilder.DropTable(
                name: "FavoriteArticles");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteLists_FavoriteArticleId",
                table: "FavoriteLists");

            migrationBuilder.DropColumn(
                name: "FavoriteArticleId",
                table: "FavoriteLists");
        }
    }
}
