using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Parameters
{
    public class CommentParameters : BaseParameters
    {
        public required int ArticleId { get; set; }
        public string? UserId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}