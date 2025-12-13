using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Entities.Services;

namespace Weblu.Domain.Entities.Portfolios
{
    public class Portfolio : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string ShortDescription { get; set; }
        public required string Slug { get; set; }
        public string? GithubUrl { get; set; }
        public string? LiveUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? ActivatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public int PortfolioCategoryId { get; set; }
        public PortfolioCategory PortfolioCategory { get; set; } = default!;
        public List<Feature> Features { get; set; } = new List<Feature>();
        public List<Method> Methods { get; set; } = new List<Method>();
        public List<PortfolioImage> PortfolioImages { get; set; } = new List<PortfolioImage>();
        public List<Contributor> Contributors { get; set; } = new List<Contributor>();
        public List<FavoritePortfolio> FavoritePortfolios { get; set; } = new List<FavoritePortfolio>();
    }
}