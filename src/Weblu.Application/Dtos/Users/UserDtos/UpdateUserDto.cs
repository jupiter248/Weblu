namespace Weblu.Application.Dtos.Users.UserDtos
{
    public class UpdateUserDto
    {
        public required string PhoneNumber { get; set; }
        public required string UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}