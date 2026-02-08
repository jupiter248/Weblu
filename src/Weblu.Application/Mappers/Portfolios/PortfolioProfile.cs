using AutoMapper;
using Weblu.Application.Dtos.Portfolios.PortfolioCategory;
using Weblu.Application.Dtos.Portfolios.PortfolioDtos;
using Weblu.Application.Dtos.Portfolios.PortfolioDtos.PortfolioImageDtos;
using Weblu.Application.Extensions;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Mappers.Portfolios
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioSummaryDto>()
                            .ForMember(dest => dest.ThumbnailPictureUrl, opt => opt.MapFrom(src => src.PortfolioImages.FirstOrDefault(i => i.IsThumbnail).ImageMedia.Url ?? string.Empty));

            CreateMap<Portfolio, PortfolioDetailDto>()
                    .ForMember(dest => dest.ActivatedAt, opt => opt.MapFrom(src => src.ActivatedAt.HasValue ? src.ActivatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.PortfolioCategoryName, opt => opt.MapFrom(src => src.PortfolioCategory.Name ?? string.Empty));
            CreateMap<CreatePortfolioDto, Portfolio>()
                    .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()))
                    .ForMember(dest => dest.ActivatedAt, opt =>
                    {
                        opt.PreCondition(src => src.IsActive);
                        opt.MapFrom(_ => DateTimeOffset.Now);
                    });
            CreateMap<UpdatePortfolioDto, Portfolio>()
                    .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<PortfolioCategory, PortfolioCategoryDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
            CreateMap<CreatePortfolioCategoryDto, PortfolioCategory>();
            CreateMap<UpdatePortfolioCategoryDto, PortfolioCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

            CreateMap<PortfolioImage, PortfolioImageDto>()
                    .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.ImageMedia.AddedAt.ToShamsi()))
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