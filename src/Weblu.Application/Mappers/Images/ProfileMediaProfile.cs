using AutoMapper;
using Weblu.Application.Dtos.Images.ProfileDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Mappers.Images
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