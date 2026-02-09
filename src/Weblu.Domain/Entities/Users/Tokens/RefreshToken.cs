using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Users.Tokens
{
    public class RefreshToken : BaseEntity
    {
        // Required properties
        public string Token { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public required DateTimeOffset ExpiresAt { get; set; }
        // Relationships
        public string UserId { get; set; } = default!;
    }
}