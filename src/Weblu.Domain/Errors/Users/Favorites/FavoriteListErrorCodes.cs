namespace Weblu.Domain.Errors.Users.Favorites
{
    public class FavoriteListErrorCodes
    {
        public const string NotFound = "FAVORITE_LIST_NOT_FOUND";
        public const string DeleteForbidden = "FAVORITE_LIST_DELETE_FORBIDDEN";
        public const string UpdateForbidden = "FAVORITE_LIST_UPDATE_FORBIDDEN";
        public const string NameRequired = "FAVORITE_LIST_NAME_REQUIRED";
        public const string NameMaxLength = "FAVORITE_LIST_NAME_MAX_LENGTH";
        public const string PortfolioAlreadyAddedToFavoriteList = "PORTFOLIO_ALREADY_ADDED_TO_FAVORITE_LIST";
        public const string ArticleAlreadyAddedToFavoriteList = "ARTICLE_ALREADY_ADDED_TO_FAVORITE_LIST";

    }
}