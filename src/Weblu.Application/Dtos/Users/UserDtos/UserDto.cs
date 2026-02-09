using Weblu.Application.Dtos.Images.ProfileDtos;

namespace Weblu.Application.Dtos.Users.UserDtos
{
    public class UserDto
    {
        public string Id { get; set; } = default!;
        public required string PhoneNumber { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string FullName { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public string CreatedAt { get; set; } = default!;
        public string? UpdatedAt { get; set; }
        public List<ProfileDto>? Profiles { get; set; }
    }
}