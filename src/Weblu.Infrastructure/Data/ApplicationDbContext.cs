using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Portfolios;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Entities.Users;
using Weblu.Infrastructure.Identity;
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

            base.OnModelCreating(modelBuilder);
        }

    }
}