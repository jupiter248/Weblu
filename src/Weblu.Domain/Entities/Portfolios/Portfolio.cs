using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Common.Contributors;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Domain.Errors.Common;
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
        // Required properties
        public string Title { get; set; } = default!;
        public int ReadingTimeMinutes { get; set; }
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public string Slug { get; set; } = default!;
        public string? GithubUrl { get; set; }
        public string? LiveUrl { get; set; }
        // Publishing Info
        public bool IsPublished { get; private set; }
        public DateTimeOffset? PublishedAt { get; private set; }
        // Relationships
        public int PortfolioCategoryId { get; set; }
        public PortfolioCategory PortfolioCategory { get; set; } = default!;
        public List<Feature> Features { get; set; } = new();
        public List<Method> Methods { get; set; } = new();
        public List<PortfolioImage> PortfolioImages { get; set; } = new();
        public List<Contributor> Contributors { get; set; } = new();
        public List<FavoritePortfolio> FavoritePortfolios { get; set; } = new();
        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> Events => _events;
        // Domain behavior methods
        public void Update()
        {
            MarkUpdated();
            RaiseEvent(new PortfolioUpdatedEvent(GuidId));
        }
        public void Publish()
        {
            if (IsPublished) throw new DomainException(PortfolioErrorCodes.AlreadyPublished, 409);
            IsPublished = true;
            PublishedAt = DateTimeOffset.Now;
            RaiseEvent(new PortfolioPublishedEvent(GuidId));
        }
        public void Unpublish()
        {
            if (!IsPublished) throw new DomainException(PortfolioErrorCodes.DidNotPublish, 409); ;
            IsPublished = false;
            PublishedAt = null;
            RaiseEvent(new PortfolioUnpublishedEvent(GuidId));
        }


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
        public void RemoveMethod(Method method)
        {
            if (!Methods.Any(c => c.Id == method.Id))
            {
                throw new DomainException(MethodErrorCodes.MethodNotFound, 404);
            }
            Methods.Remove(method);
        }
        public void RemoveFeature(Feature feature)
        {
            if (!Features.Any(c => c.Id == feature.Id))
            {
                throw new DomainException(MethodErrorCodes.MethodNotFound, 404);
            }
            Features.Remove(feature);
        }
        public void RemoveContributor(Contributor contributor)
        {
            if (!Contributors.Any(c => c.Id == contributor.Id))
            {
                throw new DomainException(ContributorErrorCodes.ContributorNotFound, 404);
            }
            Contributors.Remove(contributor);
        }
        public void RemoveImage(ImageMedia imageMedia)
        {
            PortfolioImage? portfolioImage = PortfolioImages.FirstOrDefault(i => i.ImageMediaId == imageMedia.Id);
            if (portfolioImage == null)
            {
                throw new DomainException(ImageErrorCodes.ImageNotFound, 404);
            }
            PortfolioImages.Remove(portfolioImage);
        }
        public void RaiseEvent(IDomainEvent domainEvent)
            => _events.Add(domainEvent);
        public void ClearEvents()
            => _events.Clear();
    }
}