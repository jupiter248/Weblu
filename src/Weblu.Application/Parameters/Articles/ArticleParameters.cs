using Weblu.Application.Common.Parameters;
using Weblu.Domain.Enums.Articles.Parameters;

namespace Weblu.Application.Parameters.Articles
{
    public class ArticleParameters : BaseParameters
    {
        public int? CategoryId { get; set; }
        public int? ContributorId { get; set; }
        public CommentCountSort CommentCountSort { get; set; }
        public LikeCountSort LikeCountSort { get; set; }
        public ViewCountSort ViewCountSort { get; set; }

    }
}