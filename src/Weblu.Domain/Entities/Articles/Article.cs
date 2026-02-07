using Weblu.Domain.Entities.Articles.Comments;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Common.Contributors;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Tags;
using Weblu.Domain.Errors.Articles;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Events.Articles;
using Weblu.Domain.Events.Common;
using Weblu.Domain.Exceptions;

namespace Weblu.Domain.Entities.Articles
{
    public class Article : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string? BelowTitle { get; set; }
        public string Slug { get; set; } = default!;
        public string Text { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ShortDescription { get; set; } = default!;
        public int ViewCount { get; set; } = 0;
        public bool IsPublished { get; set; } = false;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public int CategoryId { get; set; }
        public ArticleCategory Category { get; set; } = default!;
        public List<ArticleImage> ArticleImages { get; set; } = new List<ArticleImage>();
        public List<Contributor> Contributors { get; set; } = new List<Contributor>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<ArticleLike> ArticleLikes { get; set; } = new List<ArticleLike>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> Events => _events;
        public void Add()
        {
            AddDomainEvent(new ArticleAddedEvent(GuidId));
        }
        public void Update()
        {
            AddDomainEvent(new ArticleUpdatedEvent(GuidId));
        }
        public override void Delete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTimeOffset.Now;
            AddDomainEvent(new ArticleDeletedEvent(GuidId));
        }


        public void AddDomainEvent(IDomainEvent domainEvent)
            => _events.Add(domainEvent);
        public void ClearDomainEvents()
            => _events.Clear();


        public void AddTag(Tag tag)
        {
            if (Tags.Any(t => t.Id == tag.Id))
            {
                throw new DomainException(ArticleErrorCodes.TagAlreadyAddedToArticle, 409);
            }
            Tags.Add(tag);
        }
        public void AddContributor(Contributor contributor)
        {
            if (Contributors.Any(c => c.Id == contributor.Id))
            {
                throw new DomainException(ArticleErrorCodes.ContributorAlreadyAddedToArticle, 409);
            }
            Contributors.Add(contributor);
        }
        public void AddImage(ArticleImage articleImage)
        {
            if (ArticleImages.Any(p => p.ImageId == articleImage.ImageId))
            {
                throw new DomainException(ArticleErrorCodes.ImageAlreadyAddedToArticle, 409);
            }
            if (ArticleImages.Any(p => p.IsThumbnail && articleImage.IsThumbnail))
            {
                throw new DomainException(ArticleErrorCodes.ArticleHasThumbnailImage, 409);
            }
            ArticleImages.Add(articleImage);
        }
        public void Like(ArticleLike articleLike)
        {
            if (ArticleLikes.Any(u => u.UserId == articleLike.UserId))
            {
                throw new DomainException(ArticleErrorCodes.AlreadyLikedByUser, 409);
            }

            ArticleLikes.Add(articleLike);
        }
        public void DeleteTag(Tag tag)
        {
            if (!Tags.Any(c => c.Id == tag.Id))
            {
                throw new DomainException(TagErrorCodes.NotFound, 404);
            }
            Tags.Remove(tag);
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
            ArticleImage? articleImage = ArticleImages.FirstOrDefault(i => i.ImageId == imageMedia.Id);
            if (articleImage == null)
            {
                throw new DomainException(ImageErrorCodes.ImageNotFound, 404);
            }
            ArticleImages.Remove(articleImage);
        }
        public void UnLike(string userId)
        {
            ArticleLike? currentArticleLike = ArticleLikes.FirstOrDefault(u => u.UserId == userId);
            if (currentArticleLike == null)
            {
                throw new DomainException(ArticleErrorCodes.DidNotLikeByUser, 409);
            }

            ArticleLikes.Remove(currentArticleLike);
        }
        public void UpdateViewCount()
        {
            ViewCount += 1;
        }
        public void UpdatePublishedStatus(bool isPublished)
        {
            if (IsPublished == isPublished) return;
            IsPublished = isPublished;
            if (isPublished)
            {
                PublishedAt = DateTimeOffset.Now;
            }
            else
            {
                PublishedAt = null;
            }
        }
    }
}