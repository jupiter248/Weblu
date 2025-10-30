using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.PortfolioCategory;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Portfolios;

namespace Weblu.Application.Mappers
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<PortfolioCategory, PortfolioCategoryDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));

            CreateMap<AddPortfolioCategoryDto, PortfolioCategory>();
            CreateMap<UpdatePortfolioCategoryDto, PortfolioCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

        }
    }
}