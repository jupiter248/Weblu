using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationsBetweenUserAndProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_AspNetUsers_AppUserId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_AppUserId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Media");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Media",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_OwnerId",
                table: "Media",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_AspNetUsers_OwnerId",
                table: "Media",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_AspNetUsers_OwnerId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_OwnerId",
                table: "Media");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Media",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_AppUserId",
                table: "Media",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_AspNetUsers_AppUserId",
                table: "Media",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
