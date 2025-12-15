using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ArticleDtos.ArticleImageDtos;

namespace Weblu.Application.Interfaces.Services.Articles
{
    public interface IArticleImageService
    {
        Task AddImageAsync(int articleId, int imageId, AddArticleImageDto addArticleImageDto);
        Task DeleteImageAsync(int articleId, int imageId);
    }
}