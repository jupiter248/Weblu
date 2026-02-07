namespace Weblu.Domain.Errors.Services
{
    public class ServiceErrorCodes
    {
        public const string ServiceNotFound = "SERVICE_NOT_FOUND";
        public const string ServiceTitleRequired = "SERVICE_TITLE_REQUIRED";
        public const string ServiceTitleMaxLength = "SERVICE_TITLE_MAX_LENGTH";
        public const string ServiceDescriptionRequired = "SERVICE_DESCRIPTION_REQUIRED";
        public const string ServiceDescriptionMaxLength = "SERVICE_DESCRIPTION_MAX_LENGTH";
        public const string ServiceShortDescriptionRequired = "SERVICE_SHORT_DESCRIPTION_REQUIRED";
        public const string ServiceShortDescriptionMaxLength = "SERVICE_SHORT_DESCRIPTION_MAX_LENGTH";
        public const string ServiceBaseDurationRequired = "SERVICE_BASE_DURATION_REQUIRED";
        public const string ServiceBasePriceRequired = "SERVICE_BASE_PRICE_REQUIRED";
        public const string ServiceIsActiveRequired = "SERVICE_IS_ACTIVE_REQUIRED";
        public const string FeatureAlreadyAddedToService = "FEATURE_ALREADY_ADDED_TO_SERVICE";
        public const string MethodAlreadyAddedToService = "METHOD_ALREADY_ADDED_TO_SERVICE";
        public const string ImageAlreadyAddedToService = "IMAGE_ALREADY_ADDED_TO_SERVICE";
        public const string ServiceHasThumbnailImage = "SERVICE_HAS_THUMBNAIL_IMAGE";




    }
}