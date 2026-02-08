using AutoMapper;
using Weblu.Application.Dtos.About.SocialMediaDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Mappers.About
{
    public class SocialMediaProfile : Profile
    {
        public SocialMediaProfile()
        {
            CreateMap<SocialMedia, SocialMediaDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateSocialMediaDto, SocialMedia>();
            CreateMap<UpdateSocialMediaDto, SocialMedia>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}