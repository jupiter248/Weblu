namespace Weblu.Domain.Errors.Articles
{
    public class ArticleErrorCodes
    {
        public const string NotFound = "ARTICLE_NOT_FOUND";
        public const string TitleRequired = "ARTICLE_TITLE_REQUIRED";
        public const string TitleMaxLength = "ARTICLE_TITLE_MAX_LENGTH";
        public const string AboveTextMaxLength = "ARTICLE_ABOVE_TEXT_MAX_LENGTH";
        public const string BelowTextMaxLength = "ARTICLE_BELOW_TEXT_MAX_LENGTH";
        public const string TextRequired = "ARTICLE_TEXT_REQUIRED";
        public const string DescriptionRequired = "ARTICLE_DESCRIPTION_REQUIRED";
        public const string ShortDescriptionRequired = "ARTICLE_SHORT_DESCRIPTION_REQUIRED";
        public const string ShortDescriptionMaxLength = "ARTICLE_SHORT_DESCRIPTION_MAX_LENGTH";
        public const string AlreadyLikedByUser = "ARTICLE_ALREADY_LIKED_BY_USER";
        public const string DidNotLikeByUser = "ARTICLE_DID_NOT_LIKE_BY_USER";
        public const string ContributorAlreadyAddedToArticle = "CONTRIBUTOR_ALREADY_ADDED_TO_ARTICLE";
        public const string ImageAlreadyAddedToArticle = "CONTRIBUTOR_ALREADY_ADDED_TO_ARTICLE";
        public const string ArticleHasThumbnailImage = "ARTICLE_HAS_THUMBNAIL_IMAGE";
        public const string AlreadyPublished = "ARTICLE_ALREADY_PUBLISHED";
        public const string DidNotPublish = "ARTICLE_DID_NOT_PUBLISHED";
        public const string IsPublish = "ARTICLE_IS_PUBLISH";

        public const string TagAlreadyAddedToArticle = "TAG_ALREADY_ADDED_TO_ARTICLE";

    }
}