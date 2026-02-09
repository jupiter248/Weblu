using Weblu.Domain.Entities.Common;
using Weblu.Domain.Enums.Users.Favorites;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Users.Favorites;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Exceptions;

namespace Weblu.Domain.Entities.Users.Favorites
{
    public class FavoriteList : BaseEntity
    {
        // Required properties
        public required string Name { get; set; }
        // Relationships
        public string UserId { get; set; } = default!;
        public FavoriteListType FavoriteListType { get; set; }
        public List<FavoritePortfolio> FavoritePortfolios { get; set; } = new();
        public List<FavoriteArticle> FavoriteArticles { get; set; } = new();
        public void AddPortfolio(FavoritePortfolio favoritePortfolio)
        {
            if (!FavoritePortfolios.Any(f => f.UserId == favoritePortfolio.UserId && f.PortfolioId == favoritePortfolio.PortfolioId))
            {
                throw new DomainException(PortfolioErrorCodes.PortfolioNotFound, 404);
            }
            FavoritePortfolios.Add(favoritePortfolio);
        }
        public void DeletePortfolio(FavoritePortfolio favoritePortfolio)
        {
            if (FavoritePortfolios.Any(f => f.UserId == favoritePortfolio.UserId && f.PortfolioId == favoritePortfolio.PortfolioId))
            {
                throw new DomainException(PortfolioErrorCodes.PortfolioNotFound, 404);
            }
            FavoritePortfolios.Add(favoritePortfolio);
        }
        public void AddArticle(FavoriteArticle favoriteArticle)
        {
            if (FavoriteArticles.Any(f => f.UserId == favoriteArticle.UserId && f.ArticleId == favoriteArticle.ArticleId))
            {
                throw new DomainException(FavoriteListErrorCodes.ArticleAlreadyAddedToFavoriteList, 409);
            }
            FavoriteArticles.Add(favoriteArticle);
        }
        public void DeleteArticle(FavoriteArticle favoriteArticle)
        {
            if (!FavoriteArticles.Any(f => f.UserId == favoriteArticle.UserId && f.ArticleId == favoriteArticle.ArticleId))
            {
                throw new DomainException(ArticleErrorCodes.NotFound, 404);
            }
            FavoriteArticles.Add(favoriteArticle);
        }
    }
}