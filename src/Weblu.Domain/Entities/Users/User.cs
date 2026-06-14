namespace Weblu.Domain.Entities.Users;

public class User(string id, string username, string phoneNumber, string fullName , string? userProfileUrl, string? userProfileAltText)
{
    public string Id { get; private set; } = id;
    public string Username { get; private set; } = username;
    public string PhoneNumber { get; private set; } = phoneNumber;
    public string FullName { get; private set; } = fullName;
    public string? UserProfileUrl { get; private set; } = userProfileUrl;
    public string? UserProfileAltText { get; private set; } = userProfileAltText;
}