using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedArticleImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImage_Articles_ArticleId",
                table: "ArticleImage");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImage_Media_ImageId",
                table: "ArticleImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleImage",
                table: "ArticleImage");

            migrationBuilder.RenameTable(
                name: "ArticleImage",
                newName: "ArticleImages");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleImage_ImageId",
                table: "ArticleImages",
                newName: "IX_ArticleImages_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleImage_ArticleId",
                table: "ArticleImages",
                newName: "IX_ArticleImages_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleImages",
                table: "ArticleImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImages_Articles_ArticleId",
                table: "ArticleImages",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImages_Media_ImageId",
                table: "ArticleImages",
                column: "ImageId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImages_Articles_ArticleId",
                table: "ArticleImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleImages_Media_ImageId",
                table: "ArticleImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleImages",
                table: "ArticleImages");

            migrationBuilder.RenameTable(
                name: "ArticleImages",
                newName: "ArticleImage");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleImages_ImageId",
                table: "ArticleImage",
                newName: "IX_ArticleImage_ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleImages_ArticleId",
                table: "ArticleImage",
                newName: "IX_ArticleImage_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleImage",
                table: "ArticleImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImage_Articles_ArticleId",
                table: "ArticleImage",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleImage_Media_ImageId",
                table: "ArticleImage",
                column: "ImageId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
