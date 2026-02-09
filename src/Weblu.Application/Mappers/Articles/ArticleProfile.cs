using AutoMapper;
using Weblu.Application.DTOs.Articles.ArticleCategoryDTOs;
using Weblu.Application.DTOs.Articles.ArticleDTOs;
using Weblu.Application.DTOs.Articles.ArticleDTOs.ArticleImageDTOs;
using Weblu.Application.Extensions;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Mappers.Articles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleSummaryDTO>()
                    .ForMember(dest => dest.ThumbnailPictureUrl, opt => opt.MapFrom(src => src.ArticleImages.FirstOrDefault(i => i.IsThumbnail).Image.Url ?? string.Empty));

            CreateMap<Article, ArticleDetailDTO>()
                    .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt.HasValue ? src.PublishedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name ?? string.Empty));



            CreateMap<CreateArticleDTO, Article>()
                    .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()));
            CreateMap<UpdateArticleDTO, Article>()
                    .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<ArticleCategory, ArticleCategoryDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
            CreateMap<CreateArticleCategoryDTO, ArticleCategory>();
            CreateMap<UpdateArticleCategoryDTO, ArticleCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<ArticleImage, ArticleImageDTO>()
                    .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.Image.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Image.Name))
                    .ForMember(dest => dest.AltText, opt => opt.MapFrom(src => src.Image.AltText))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Image.Id))
                    .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Image.Url))
                    .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Image.Width))
                    .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Image.Height))
                    .ForMember(dest => dest.IsThumbnail, opt => opt.MapFrom(src => src.IsThumbnail));
        }
    }
}