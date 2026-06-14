using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRolePermissins : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Orders_OrderId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_OrderId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Features");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FeatureOrder",
                columns: table => new
                {
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureOrder", x => new { x.FeaturesId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_FeatureOrder_Features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeatureOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeatureOrder_OrdersId",
                table: "FeatureOrder",
                column: "OrdersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeatureOrder");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Features",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Features_OrderId",
                table: "Features",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Orders_OrderId",
                table: "Features",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
