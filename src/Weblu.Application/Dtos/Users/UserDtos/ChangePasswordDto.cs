namespace Weblu.Application.DTOs.Users.UserDTOs
{
    public class ChangePasswordDTO
    {
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}