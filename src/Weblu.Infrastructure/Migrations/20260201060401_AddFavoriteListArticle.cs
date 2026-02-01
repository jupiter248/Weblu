using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteListArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteLists_FavoriteArticles_FavoriteArticleId",
                table: "FavoriteLists");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteLists_FavoriteArticleId",
                table: "FavoriteLists");

            migrationBuilder.DropColumn(
                name: "FavoriteArticleId",
                table: "FavoriteLists");

            migrationBuilder.CreateTable(
                name: "FavoriteArticleFavoriteList",
                columns: table => new
                {
                    FavoriteArticlesId = table.Column<int>(type: "int", nullable: false),
                    FavoriteListsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteArticleFavoriteList", x => new { x.FavoriteArticlesId, x.FavoriteListsId });
                    table.ForeignKey(
                        name: "FK_FavoriteArticleFavoriteList_FavoriteArticles_FavoriteArticlesId",
                        column: x => x.FavoriteArticlesId,
                        principalTable: "FavoriteArticles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteArticleFavoriteList_FavoriteLists_FavoriteListsId",
                        column: x => x.FavoriteListsId,
                        principalTable: "FavoriteLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteArticleFavoriteList_FavoriteListsId",
                table: "FavoriteArticleFavoriteList",
                column: "FavoriteListsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteArticleFavoriteList");

            migrationBuilder.AddColumn<int>(
                name: "FavoriteArticleId",
                table: "FavoriteLists",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteLists_FavoriteArticleId",
                table: "FavoriteLists",
                column: "FavoriteArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteLists_FavoriteArticles_FavoriteArticleId",
                table: "FavoriteLists",
                column: "FavoriteArticleId",
                principalTable: "FavoriteArticles",
                principalColumn: "Id");
        }
    }
}
