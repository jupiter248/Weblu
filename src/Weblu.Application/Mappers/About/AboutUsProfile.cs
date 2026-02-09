using AutoMapper;
using Weblu.Application.DTOs.About.AboutUsDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Mappers.About
{
    public class AboutUsProfile : Profile
    {
        public AboutUsProfile()
        {
            CreateMap<AboutUs, AboutUsDTO>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateAboutUsDTO, AboutUs>();
            CreateMap<UpdateAboutUsDTO, AboutUs>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}