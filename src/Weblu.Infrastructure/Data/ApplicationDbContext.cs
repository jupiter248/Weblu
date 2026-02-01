using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Comments;
using Weblu.Domain.Entities.Contributors;
using Weblu.Domain.Entities.Faqs;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Entities.Features;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Methods;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Entities.Tags;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Entities.Users;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Service> Services { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Method> Methods { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<ImageMedia> ImageMedia { get; set; }
        public DbSet<ServiceImage> ServiceImages { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<ProfileMedia> ProfileMedia { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<PortfolioImage> PortfolioImages { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<FaqCategory> FaqCategories { get; set; }
        public DbSet<FavoriteList> FavoriteLists { get; set; }
        public DbSet<FavoritePortfolio> FavoritePortfolios { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ArticleLike> ArticleLikes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<FavoriteArticle> FavoriteArticles { get; set; }
















        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasMany(f => f.Features)
                .WithMany(s => s.Services);

            modelBuilder.Entity<Service>()
                .HasMany(m => m.Methods)
                .WithMany(s => s.Services);

            modelBuilder.Entity<Portfolio>()
                .HasMany(f => f.Features)
                .WithMany(s => s.Portfolios);

            modelBuilder.Entity<Portfolio>()
                .HasMany(m => m.Methods)
                .WithMany(s => s.Portfolios);

            modelBuilder.Entity<Portfolio>()
                .HasMany(m => m.Contributors)
                .WithMany(s => s.Portfolios);

            modelBuilder.Entity<AppUser>()
                .HasMany(t => t.RefreshTokens)
                .WithOne()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.Profiles)
                .WithOne()
                .HasForeignKey(u => u.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(t => t.Tickets)
                .WithOne()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.FavoritePortfolios)
                .WithOne()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.FavoriteLists)
                .WithOne()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.ArticleLikes)
                .WithOne()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FavoriteList>()
                .HasMany(p => p.FavoritePortfolios)
                .WithMany(l => l.FavoriteLists)
                .UsingEntity<Dictionary<string, object>>
                (
                    "FavoriteListFavoritePortfolio",

                    j => j
                        .HasOne<FavoritePortfolio>()
                        .WithMany()
                        .HasForeignKey("FavoritePortfolioId")
                        .OnDelete(DeleteBehavior.Restrict),

                    j => j
                        .HasOne<FavoriteList>()
                        .WithMany()
                        .HasForeignKey("FavoriteListId")
                        .OnDelete(DeleteBehavior.Restrict)
                );
            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.Comments)
                .WithOne()
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Article>()
                .HasMany(m => m.Tags)
                .WithMany(s => s.Articles);

            base.OnModelCreating(modelBuilder);
        }

    }
}