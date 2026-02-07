namespace Weblu.Application.Dtos.Auth.AuthDtos
{
    public class RegisterDto
    {
        public required string PhoneNumber { get; set; }
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
    }
}