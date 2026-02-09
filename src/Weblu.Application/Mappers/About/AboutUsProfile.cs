using AutoMapper;
using Weblu.Application.Dtos.About.AboutUsDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Mappers.About
{
    public class AboutUsProfile : Profile
    {
        public AboutUsProfile()
        {
            CreateMap<AboutUs, AboutUsDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateAboutUsDto, AboutUs>();
            CreateMap<UpdateAboutUsDto, AboutUs>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}