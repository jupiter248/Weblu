using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FavoriteDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Mappers
{
    public class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<FavoritePortfolio, FavoritePortfolioDto>()
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.AddedAt.ToShamsi()));
        }
    }
}