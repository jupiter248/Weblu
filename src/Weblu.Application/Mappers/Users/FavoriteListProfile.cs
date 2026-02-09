using AutoMapper;
using Weblu.Application.DTOs.Users.Favorites.FavoriteListDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Mappers.Users
{
    public class FavoriteListProfile : Profile
    {
        public FavoriteListProfile()
        {
            CreateMap<FavoriteList, FavoriteListDTO>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                    .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(src => src.FavoritePortfolios.Count));
            CreateMap<CreateFavoriteListDTO, FavoriteList>();
            CreateMap<UpdateFavoriteListDTO, FavoriteList>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}