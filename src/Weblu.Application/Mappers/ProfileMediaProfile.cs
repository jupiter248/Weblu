using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ProfileDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Mappers
{
    public class ProfileMediaProfile : Profile
    {
        public ProfileMediaProfile()
        {
            CreateMap<ProfileMedia, ProfileDto>()
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.AddedAt.ToShamsi()))
                .ForMember(dest => dest.OwnerType, opt => opt.MapFrom(src => src.OwnerType.ToString()));

        }
    }
}