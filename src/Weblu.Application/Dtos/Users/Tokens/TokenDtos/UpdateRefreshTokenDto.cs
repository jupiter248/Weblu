namespace Weblu.Application.Dtos.Users.Tokens.TokenDtos
{
    public class UpdateRefreshTokenDto
    {
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
    }
}