using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.PortfolioCategory;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Application.Extensions;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Mappers
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Portfolio, PortfolioSummaryDto>();
            CreateMap<Portfolio, PortfolioDetailDto>()
                    .ForMember(dest => dest.ActivatedAt, opt => opt.MapFrom(src => src.ActivatedAt.HasValue ? src.ActivatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.PortfolioCategoryName, opt => opt.MapFrom(src => src.PortfolioCategory.Name ?? string.Empty));
            CreateMap<AddPortfolioDto, Portfolio>()
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
            CreateMap<AddPortfolioCategoryDto, PortfolioCategory>();
            CreateMap<UpdatePortfolioCategoryDto, PortfolioCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

        }
    }
}