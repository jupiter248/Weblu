namespace Weblu.Application.Dtos.Users.Tokens.TokenDtos
{
    public class RefreshTokenDto
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public required string ExpiresAt { get; set; }
        public required string CreatedAt { get; set; } 
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}