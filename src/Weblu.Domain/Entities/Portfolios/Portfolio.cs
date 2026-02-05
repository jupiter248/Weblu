using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Contributors;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Entities.Features;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Methods;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Contributors;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Portfolios;
using Weblu.Domain.Events.Common;
using Weblu.Domain.Events.Portfolios;
using Weblu.Domain.Exceptions;

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
        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> Events => _events;
        public void Add()
        {
            AddDomainEvent(new PortfolioAddedEvent(GuidId));
        }
        public override void Delete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTimeOffset.Now;
            AddDomainEvent(new PortfolioDeletedEvent(GuidId));
        }
        public void Update()
        {
            AddDomainEvent(new PortfolioUpdatedEvent(GuidId));
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
            => _events.Add(domainEvent);
        public void ClearDomainEvents()
            => _events.Clear();

        public void AddMethod(Method method)
        {
            if (Methods.Any(m => m.Id == method.Id))
            {
                throw new DomainException(PortfolioErrorCodes.MethodAlreadyAddedToPortfolio, 409);
            }
            Methods.Add(method);
        }
        public void AddFeature(Feature feature)
        {
            if (Features.Any(f => f.Id == feature.Id))
            {
                throw new DomainException(PortfolioErrorCodes.FeatureAlreadyAddedToPortfolio, 409);
            }
            Features.Add(feature);
        }
        public void AddContributor(Contributor contributor)
        {
            if (Contributors.Any(c => c.Id == contributor.Id))
            {
                throw new DomainException(PortfolioErrorCodes.ContributorAlreadyAddedToPortfolio, 409);
            }
            Contributors.Add(contributor);
        }
        public void AddImage(PortfolioImage image)
        {
            if (PortfolioImages.Any(p => p.ImageMediaId == image.ImageMediaId))
            {
                throw new DomainException(PortfolioErrorCodes.ImageAlreadyAddedToPortfolio, 409);
            }
            if (PortfolioImages.Any(p => p.IsThumbnail && image.IsThumbnail))
            {
                throw new DomainException(PortfolioErrorCodes.PortfolioHasThumbnailImage, 409);
            }
            PortfolioImages.Add(image);
        }
        public void DeleteMethod(Method method)
        {
            if (!Methods.Any(c => c.Id == method.Id))
            {
                throw new DomainException(MethodErrorCodes.MethodNotFound, 404);
            }
            Methods.Remove(method);
        }
        public void DeleteFeature(Feature feature)
        {
            if (!Features.Any(c => c.Id == feature.Id))
            {
                throw new DomainException(MethodErrorCodes.MethodNotFound, 404);
            }
            Features.Remove(feature);
        }
        public void DeleteContributor(Contributor contributor)
        {
            if (!Contributors.Any(c => c.Id == contributor.Id))
            {
                throw new DomainException(ContributorErrorCodes.ContributorNotFound, 404);
            }
            Contributors.Remove(contributor);
        }
        public void DeleteImage(ImageMedia imageMedia)
        {
            PortfolioImage? portfolioImage = PortfolioImages.FirstOrDefault(i => i.ImageMediaId == imageMedia.Id);
            if (portfolioImage == null)
            {
                throw new DomainException(ImageErrorCodes.ImageNotFound, 404);
            }
            PortfolioImages.Remove(portfolioImage);
        }
        public void UpdateActivateStatus(bool isActive)
        {
            if (IsActive == isActive) return;
            IsActive = isActive;
            if (isActive)
            {
                ActivatedAt = DateTimeOffset.Now;
            }
            else
            {
                ActivatedAt = null;
            }
        }
    }
}