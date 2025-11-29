using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Articles
{
    public class ArticleCategoryErrorCodes
    {
        public const string NotFound = "ARTICLE_CATEGORY_NOT_FOUND";
        public const string NameRequired = "ARTICLE_CATEGORY_NAME_REQUIRED";
        public const string NameMaxLength = "ARTICLE_CATEGORY_NAME_MAX_LENGTH";
        public const string DescriptionRequired = "ARTICLE_CATEGORY_DESCRIPTION_REQUIRED";
        public const string DescriptionMaxLength = "ARTICLE_CATEGORY_DESCRIPTION_MAX_LENGTH";
    }
}