namespace Weblu.Domain.Entities.Users;

public class User(string id, string username, string? userProfileUrl, string? userProfileAltText)
{
    public string Id { get; private set; } = id;
    public string Username { get; private set; } = username;
    public string? UserProfileUrl { get; private set; } = userProfileUrl;
    public string? UserProfileAltText { get; private set; } = userProfileAltText;
}