using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Weblu.Domain.Entities.Favorites;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Entities.Tickets;
using Weblu.Domain.Entities.Users;

namespace Weblu.Infrastructure.Identity.Entities
{
    public class AppUser : IdentityUser
    {
        [AllowNull]
        public override required string PhoneNumber { get => base.PhoneNumber!; set => base.PhoneNumber = value; }
        [AllowNull]
        public override required string UserName { get => base.UserName!; set => base.UserName = value; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public DateTimeOffset? UpdatedAt { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public List<ProfileMedia> Profiles { get; set; } = new List<ProfileMedia>();
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
        public List<FavoritePortfolio> FavoritePortfolios { get; set; } = new List<FavoritePortfolio>();
        public List<FavoriteList> FavoriteLists { get; set; } = new List<FavoriteList>();



    }
}