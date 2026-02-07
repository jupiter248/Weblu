using AutoMapper;
using Weblu.Application.Dtos.Users.Favorites.FavoriteDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Users.Favorites;

namespace Weblu.Application.Mappers.Users
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