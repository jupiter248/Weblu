namespace Weblu.Application.DTOs.Users.Tokens.TokenDTOs
{
    public class UpdateRefreshTokenDTO
    {
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
    }
}