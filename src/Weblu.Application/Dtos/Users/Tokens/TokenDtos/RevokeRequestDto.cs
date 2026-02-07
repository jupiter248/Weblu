namespace Weblu.Application.Dtos.Users.Tokens.TokenDtos
{
    public class RevokeRequestDto
    {
        public required string RefreshToken { get; set; }
    }
}