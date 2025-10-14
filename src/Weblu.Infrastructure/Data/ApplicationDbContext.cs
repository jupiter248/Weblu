using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Services;

namespace Weblu.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
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




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasMany(f => f.Features)
                .WithMany(s => s.Services);

            modelBuilder.Entity<Service>()
                .HasMany(m => m.Methods)
                .WithMany(s => s.Services);

            base.OnModelCreating(modelBuilder);
        }

    }
}