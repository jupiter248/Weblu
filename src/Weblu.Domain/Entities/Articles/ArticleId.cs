using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weblu.Domain.Entities.Articles
{
    public class ArticleId
    {
        public Guid Id { get; }
        public ArticleId(Guid id)
        {
            Id = id;
        }
        public static ArticleId New() => new ArticleId(Guid.NewGuid());

    }
}