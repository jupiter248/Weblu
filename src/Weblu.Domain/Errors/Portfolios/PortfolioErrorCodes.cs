using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Portfolios
{
    public class PortfolioErrorCodes
    {
        public const string PortfolioNotFound = "PORTFOLIO_NOT_FOUND"; 
        public const string MethodAlreadyAddedToPortfolio = "METHOD_ALREADY_ADDED_TOT_PORTFOLIO"; 
        public const string FeatureAlreadyAddedToPortfolio = "FEATURE_ALREADY_ADDED_TOT_PORTFOLIO"; 
        public const string TitleRequired = "PORTFOLIO_TITLE_REQUIRED";
        public const string TitleTooLong = "PORTFOLIO_TITLE_TOO_LONG";
        public const string DescriptionRequired = "PORTFOLIO_DESCRIPTION_REQUIRED";
        public const string DescriptionTooLong = "PORTFOLIO_DESCRIPTION_TOO_LONG";
        public const string ShortDescriptionRequired = "PORTFOLIO_SHORT_DESCRIPTION_REQUIRED";
        public const string ShortDescriptionTooLong = "PORTFOLIO_SHORT_DESCRIPTION_TOO_LONG";
        public const string InvalidGithubUrl = "PORTFOLIO_INVALID_GITHUB_URL";
        public const string InvalidLiveUrl = "PORTFOLIO_INVALID_LIVE_URL";
        public const string InvalidCategoryId = "PORTFOLIO_INVALID_CATEGORY_ID";

    }
}