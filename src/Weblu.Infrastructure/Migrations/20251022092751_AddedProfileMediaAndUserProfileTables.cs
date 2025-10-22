using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedProfileMediaAndUserProfileTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "Media",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileMedia_Height",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileMedia_Width",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileMediaId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfile_Media_ProfileMediaId",
                        column: x => x.ProfileMediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_AppUserId",
                table: "UserProfile",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_ProfileMediaId",
                table: "UserProfile",
                column: "ProfileMediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ProfileMedia_Height",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "ProfileMedia_Width",
                table: "Media");
        }
    }
}
