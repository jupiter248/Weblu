using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.PortfolioCategory
{
    public class PortfolioCategoryErrorCodes
    {
        public const string PortfolioCategoryNotFound = "PORTFOLIO_CATEGORY_NOT_FOUND";
        public const string PortfolioCategoryNameRequired = "PORTFOLIO_CATEGORY_NAME_REQUIRED";
        public const string PortfolioCategoryNameMaxLength = "PORTFOLIO_CATEGORY_NAME_MAX_LENGTH";
        public const string PortfolioCategoryDescriptionRequired = "PORTFOLIO_CATEGORY_DESCRIPTION_REQUIRED";
        public const string PortfolioCategoryDescriptionMaxLength = "PORTFOLIO_CATEGORY_DESCRIPTION_MAX_LENGTH";
    }
}