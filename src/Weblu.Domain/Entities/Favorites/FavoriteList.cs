using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Enums.Favorites;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Favorites;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Exceptions;

namespace Weblu.Domain.Entities.Favorites
{
    public class FavoriteList : BaseEntity
    {
        public required string Name { get; set; }
        public FavoriteListType FavoriteListType { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public List<FavoritePortfolio> FavoritePortfolios { get; set; } = new List<FavoritePortfolio>();
        public List<FavoriteArticle> FavoriteArticles { get; set; } = new List<FavoriteArticle>();
        public string UserId { get; set; } = default!;
        public void AddFavoritePortfolio(FavoritePortfolio favoritePortfolio)
        {
            if (!FavoritePortfolios.Any(f => f.UserId == favoritePortfolio.UserId && f.PortfolioId == favoritePortfolio.PortfolioId))
            {
                throw new DomainException(PortfolioErrorCodes.PortfolioNotFound, 404);
            }
            FavoritePortfolios.Add(favoritePortfolio);
        }
        public void DeleteFavoritePortfolio(FavoritePortfolio favoritePortfolio)
        {
            if (FavoritePortfolios.Any(f => f.UserId == favoritePortfolio.UserId && f.PortfolioId == favoritePortfolio.PortfolioId))
            {
                throw new DomainException(PortfolioErrorCodes.PortfolioNotFound, 404);
            }
            FavoritePortfolios.Add(favoritePortfolio);
        }
        public void AddFavoriteArticle(FavoriteArticle favoriteArticle)
        {
            if (FavoriteArticles.Any(f => f.UserId == favoriteArticle.UserId && f.ArticleId == favoriteArticle.ArticleId))
            {
                throw new DomainException(FavoriteListErrorCodes.ArticleAlreadyAddedToFavoriteList, 409);
            }
            FavoriteArticles.Add(favoriteArticle);
        }
        public void DeleteFavoriteArticle(FavoriteArticle favoriteArticle)
        {
            if (!FavoriteArticles.Any(f => f.UserId == favoriteArticle.UserId && f.ArticleId == favoriteArticle.ArticleId))
            {
                throw new DomainException(ArticleErrorCodes.NotFound, 404);
            }
            FavoriteArticles.Add(favoriteArticle);
        }
    }
}