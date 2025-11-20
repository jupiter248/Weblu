using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Mappers
{
    public class AboutUsProfile : Profile
    {
        public AboutUsProfile()
        {
            CreateMap<AboutUs, AboutUsDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<AddAboutUsDto, AboutUs>();
            CreateMap<UpdateAboutUsDto, AboutUs>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}