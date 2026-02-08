using AutoMapper;
using Weblu.Application.Dtos.Users.Tokens.TokenDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Users.Tokens;

namespace Weblu.Application.Mappers.Users
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDto>()
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt.ToShamsi()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<UpdateRefreshTokenDto, RefreshToken>();
        }
    }
}