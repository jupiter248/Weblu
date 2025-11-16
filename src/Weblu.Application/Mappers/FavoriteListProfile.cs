using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FavoriteListDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Favorites;

namespace Weblu.Application.Mappers
{
    public class FavoriteListProfile : Profile
    {
        public FavoriteListProfile()
        {
            CreateMap<FavoriteList, FavoriteListDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.FavoritePortfolios.Count));
            CreateMap<AddFavoriteListDto, FavoriteList>();
            CreateMap<UpdateFavoriteListDto, FavoriteList>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}