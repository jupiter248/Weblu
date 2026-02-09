using AutoMapper;
using Weblu.Application.DTOs.About.SocialMediaDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Mappers.About
{
    public class SocialMediaProfile : Profile
    {
        public SocialMediaProfile()
        {
            CreateMap<SocialMedia, SocialMediaDTO>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateSocialMediaDTO, SocialMedia>();
            CreateMap<UpdateSocialMediaDTO, SocialMedia>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}