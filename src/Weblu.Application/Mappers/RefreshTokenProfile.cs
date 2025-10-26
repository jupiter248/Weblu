using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.TokenDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Users;

namespace Weblu.Application.Mappers
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