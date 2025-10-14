using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;

namespace Weblu.Application.Mappers
{
    public class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<Feature, FeatureDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<AddFeatureDto, Feature>();
            CreateMap<UpdateFeatureDto, Feature>();
        }
    }
}