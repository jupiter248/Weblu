using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        public List<UserProfile> UserProfile { get; set; } = new List<UserProfile>();

    }
}