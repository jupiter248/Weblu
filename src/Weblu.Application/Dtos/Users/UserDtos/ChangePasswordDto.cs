namespace Weblu.Application.Dtos.Users.UserDtos
{
    public class ChangePasswordDto
    {
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}