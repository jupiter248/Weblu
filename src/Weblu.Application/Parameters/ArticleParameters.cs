using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Enums.Articles.Parameters;
using Weblu.Domain.Enums.Common.Parameters;

namespace Weblu.Application.Parameters
{
    public class ArticleParameters
    {
        public CreatedDateSort CreatedDateSort { get; set; }
        public int? CategoryId { get; set; }
        public int? ContributorId { get; set; }
        public CommentCount CommentCountSort { get; set; }
        public LikeCountSort LikeCountSort { get; set; }
        public ViewCountSort ViewCountSort { get; set; }

    }
}