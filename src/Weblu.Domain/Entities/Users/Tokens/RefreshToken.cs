using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Users.Tokens
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public required DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        public string UserId { get; set; } = null!;
    }
}