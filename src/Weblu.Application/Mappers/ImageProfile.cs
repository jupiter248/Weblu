using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities;

namespace Weblu.Application.Mappers
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageMedia, ImageDto>()
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.AddedAt.ToShamsi()));
        }
    }
}