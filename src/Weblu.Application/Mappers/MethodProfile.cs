using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.MethodDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;

namespace Weblu.Application.Mappers
{
    public class MethodProfile : Profile
    {
        public MethodProfile()
        {
            CreateMap<Method, MethodDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<AddMethodDto, Method>();
            CreateMap<UpdateMethodDto, Method>();
        }
    }
}