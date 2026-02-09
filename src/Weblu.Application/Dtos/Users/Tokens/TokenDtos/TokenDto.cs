namespace Weblu.Application.Dtos.Users.Tokens.TokenDtos
{
    public class TokenDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
    }
}