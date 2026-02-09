using AutoMapper;
using Weblu.Application.DTOs.Users.Tokens.TokenDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Users.Tokens;

namespace Weblu.Application.Mappers.Users
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDTO>()
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt.ToShamsi()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<UpdateRefreshTokenDTO, RefreshToken>();
        }
    }
}