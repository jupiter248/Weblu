using AutoMapper;
using Weblu.Application.DTOs.Portfolios.PortfolioCategoryDTOs;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs;
using Weblu.Application.DTOs.Portfolios.PortfolioDTOs.PortfolioImageDTOs;
using Weblu.Application.Extensions;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Mappers.Portfolios
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioSummaryDTO>()
                            .ForMember(dest => dest.ThumbnailPictureUrl, opt => opt.MapFrom(src => src.PortfolioImages.FirstOrDefault(i => i.IsThumbnail).ImageMedia.Url ?? string.Empty));

            CreateMap<Portfolio, PortfolioDetailDTO>()
                    .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(src => src.PublishedAt.HasValue ? src.PublishedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.PortfolioCategoryName, opt => opt.MapFrom(src => src.PortfolioCategory.Name ?? string.Empty));
            CreateMap<CreatePortfolioDTO, Portfolio>()
                    .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()));
            CreateMap<UpdatePortfolioDTO, Portfolio>()
                    .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<PortfolioCategory, PortfolioCategoryDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
            CreateMap<CreatePortfolioCategoryDTO, PortfolioCategory>();
            CreateMap<UpdatePortfolioCategoryDTO, PortfolioCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<PortfolioImage, PortfolioImageDTO>()
                    .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.ImageMedia.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ImageMedia.Name))
                    .ForMember(dest => dest.AltText, opt => opt.MapFrom(src => src.ImageMedia.AltText))
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ImageMedia.Id))
                    .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.ImageMedia.Url))
                    .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.ImageMedia.Width))
                    .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.ImageMedia.Height))
                    .ForMember(dest => dest.IsThumbnail, opt => opt.MapFrom(src => src.IsThumbnail));
        }
    }
}