using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Entities.Common;

namespace Weblu.Domain.Entities.Users.Favorites
{
    public class FavoriteArticle : BaseEntity
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; } = default!;
        public string UserId { get; set; } = default!;
        
        public List<FavoriteList> FavoriteLists { get; set; } = new List<FavoriteList>();
    }
}