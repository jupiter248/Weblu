using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Errors.Comments
{
    public class CommentErrorCodes
    {
        public const string NotFound = "COMMENT_NOT_FOUND";
        public const string DeleteForbidden = "COMMENT_DELETE_FORBIDDEN";
        public const string UpdateForbidden = "COMMENT_UPDATE_FORBIDDEN";
        public const string TextRequired = "COMMENT_TEXT_REQUIRED";
        public const string TextMaxLength = "COMMENT_TEXT_MAX_LENGTH";
    }
}