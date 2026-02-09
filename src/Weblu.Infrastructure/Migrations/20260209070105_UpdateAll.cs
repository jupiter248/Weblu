using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Services",
                newName: "IsUpdated");

            migrationBuilder.RenameColumn(
                name: "ActivatedAt",
                table: "Services",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Portfolios",
                newName: "IsUpdated");

            migrationBuilder.RenameColumn(
                name: "ActivatedAt",
                table: "Portfolios",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "AddedAt",
                table: "Media",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "AddedAt",
                table: "FavoritePortfolios",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "AddedAt",
                table: "FavoriteArticles",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "FAQs",
                newName: "IsUpdated");

            migrationBuilder.RenameColumn(
                name: "ActivatedAt",
                table: "FAQs",
                newName: "PublishedAt");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Contributors",
                newName: "IsUpdated");

            migrationBuilder.RenameColumn(
                name: "IsEdited",
                table: "Comments",
                newName: "IsUpdated");

            migrationBuilder.RenameColumn(
                name: "LikedAt",
                table: "ArticleLikes",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "TicketMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Tags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "SocialMedias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "ServiceImages",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "ServiceImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "ServiceImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "SearchItems",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "SearchItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "SearchItems",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "RefreshTokens",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Portfolios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReadingTimeMinutes",
                table: "Portfolios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "PortfolioImages",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "PortfolioImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "PortfolioImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PortfolioCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "PortfolioCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Methods",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Methods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Media",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Media",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Features",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "FavoritePortfolios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "FavoritePortfolios",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "FavoriteLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "FavoriteArticles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "FavoriteArticles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "FAQs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "FAQCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Contributors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PublishedAt",
                table: "Contributors",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReadingTimeMinutes",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "ArticleLikes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "ArticleLikes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "ArticleImages",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "ArticleImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "ArticleImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "ArticleCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Vision",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SubTitle",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "AboutUs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SearchItems");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "SearchItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SearchItems");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ReadingTimeMinutes",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PortfolioImages");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "PortfolioImages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PortfolioImages");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Methods");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "FavoritePortfolios");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FavoritePortfolios");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "FavoriteLists");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "FavoriteArticles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FavoriteArticles");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "FAQCategories");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ReadingTimeMinutes",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "ArticleLikes");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ArticleLikes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ArticleImages");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "ArticleImages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ArticleImages");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "ArticleCategories");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "AboutUs");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "Services",
                newName: "ActivatedAt");

            migrationBuilder.RenameColumn(
                name: "IsUpdated",
                table: "Services",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "Portfolios",
                newName: "ActivatedAt");

            migrationBuilder.RenameColumn(
                name: "IsUpdated",
                table: "Portfolios",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Media",
                newName: "AddedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "FavoritePortfolios",
                newName: "AddedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "FavoriteArticles",
                newName: "AddedAt");

            migrationBuilder.RenameColumn(
                name: "PublishedAt",
                table: "FAQs",
                newName: "ActivatedAt");

            migrationBuilder.RenameColumn(
                name: "IsUpdated",
                table: "FAQs",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsUpdated",
                table: "Contributors",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsUpdated",
                table: "Comments",
                newName: "IsEdited");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ArticleLikes",
                newName: "LikedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PortfolioCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Methods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Vision",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubTitle",
                table: "AboutUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
