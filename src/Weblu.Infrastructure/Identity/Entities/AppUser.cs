using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Articles.Comments;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Entities.Users.Favorites;
using Weblu.Domain.Entities.Users.Tokens;

namespace Weblu.Infrastructure.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        // Required properties
        [AllowNull]
        public override required string PhoneNumber { get => base.PhoneNumber!; set => base.PhoneNumber = value; }
        [AllowNull]
        public override required string UserName { get => base.UserName!; set => base.UserName = value; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTimeOffset? UpdatedAt { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public bool IsDeleted { get; protected set; } = false;
        public bool IsUpdated { get; protected set; } = false;
        // Relationships
        public List<RefreshToken> RefreshTokens { get; set; } = new();
        public List<ProfileMedia> Profiles { get; set; } = new();
        public List<Ticket> Tickets { get; set; } = new();
        public List<FavoritePortfolio> FavoritePortfolios { get; set; } = new();
        public List<FavoriteArticle> FavoriteArticles { get; set; } = new();
        public List<FavoriteList> FavoriteLists { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public List<ArticleLike> ArticleLikes { get; set; } = new();
        public virtual void Delete()
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = DateTimeOffset.Now;
        }
        public   AppUser()
        {
            CreatedAt = DateTimeOffset.Now;
        }
        protected virtual void MarkUpdated()
        {
            IsUpdated = true;
            UpdatedAt = DateTimeOffset.Now;
        }
    }
}