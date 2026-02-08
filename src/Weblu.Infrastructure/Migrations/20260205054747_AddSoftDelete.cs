using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weblu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Tickets",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "TicketMessages",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Tags",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "SocialMedias",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Services",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "ServiceImages",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "SearchItems",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "RefreshTokens",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Portfolios",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "PortfolioImages",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "PortfolioCategories",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Methods",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Media",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Features",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "FavoritePortfolios",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "FavoriteLists",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "FavoriteArticles",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "FAQs",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "FAQCategories",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Contributors",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Comments",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Articles",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "ArticleLikes",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "ArticleImages",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "ArticleCategories",
                newName: "GuidId");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "AboutUs",
                newName: "GuidId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Tickets",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "TicketMessages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TicketMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Tags",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "SocialMedias",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SocialMedias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Services",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "ServiceImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ServiceImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "SearchItems",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SearchItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "RefreshTokens",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RefreshTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Portfolios",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Portfolios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "PortfolioImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PortfolioImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "PortfolioCategories",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PortfolioCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Methods",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Methods",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Media",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Media",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Features",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "FavoritePortfolios",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FavoritePortfolios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "FavoriteLists",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FavoriteLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "FavoriteArticles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FavoriteArticles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "FAQs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FAQs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "FAQCategories",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FAQCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Contributors",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Contributors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Comments",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "AspNetUsers",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Articles",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Articles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "ArticleLikes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ArticleLikes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "ArticleImages",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ArticleImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "ArticleCategories",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ArticleCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "AboutUs",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AboutUs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TicketMessages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SocialMedias");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "SearchItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SearchItems");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PortfolioImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PortfolioImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PortfolioCategories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Methods");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Methods");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FavoritePortfolios");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FavoritePortfolios");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FavoriteLists");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FavoriteLists");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FavoriteArticles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FavoriteArticles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FAQs");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "FAQCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FAQCategories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Contributors");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ArticleLikes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ArticleLikes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ArticleImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ArticleImages");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ArticleCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ArticleCategories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "AboutUs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AboutUs");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Tickets",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "TicketMessages",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Tags",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "SocialMedias",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Services",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "ServiceImages",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "SearchItems",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "RefreshTokens",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Portfolios",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "PortfolioImages",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "PortfolioCategories",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Methods",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Media",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Features",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "FavoritePortfolios",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "FavoriteLists",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "FavoriteArticles",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "FAQs",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "FAQCategories",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Contributors",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Comments",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "Articles",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "ArticleLikes",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "ArticleImages",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "ArticleCategories",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "GuidId",
                table: "AboutUs",
                newName: "PublicId");
        }
    }
}
