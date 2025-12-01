using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ArticleCategoryDtos;
using Weblu.Application.Dtos.ArticleDtos;
using Weblu.Application.Dtos.ArticleDtos.ArticleImageDtos;
using Weblu.Application.Extensions;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Articles;

namespace Weblu.Application.Mappers
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleSummaryDto>()
                    .ForMember(dest => dest.ThumbnailPictureUrl, opt => opt.MapFrom(src => src.ArticleImages.FirstOrDefault(i => i.IsThumbnail).Image.Url ?? string.Empty))
                    .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
                    .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.ArticleLikes.Count));

            CreateMap<Article, ArticleDetailDto>()
                    .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt.HasValue ? src.PublishedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name ?? string.Empty))
                    .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
                    .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.ArticleLikes.Count));


            CreateMap<AddArticleDto, Article>()
                    .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()))
                    .ForMember(dest => dest.PublishedAt, opt =>
                    {
                        opt.PreCondition(src => src.IsPublished);
                        opt.MapFrom(_ => DateTimeOffset.Now);
                    });
            CreateMap<UpdateArticleDto, Article>()
                    .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<ArticleCategory, ArticleCategoryDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
            CreateMap<AddArticleCategoryDto, ArticleCategory>();
            CreateMap<UpdateArticleCategoryDto, ArticleCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<ArticleImage, ArticleImageDto>()
                    .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.Image.AddedAt.ToShamsi()))
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