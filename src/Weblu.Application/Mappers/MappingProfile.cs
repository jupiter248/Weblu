using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Application.Dtos.MethodDtos;
using Weblu.Application.Dtos.ServiceDtos;
using Weblu.Application.Extensions;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities;

namespace Weblu.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
                CreateMap<Service, ServiceDto>()
                        .ForMember(dest => dest.ActivatedAt, opt => opt.MapFrom(src => src.ActivatedAt.HasValue ? src.ActivatedAt.Value.ToShamsi() : null))
                        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
                CreateMap<AddServiceDto, Service>()
                        .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()));
                CreateMap<UpdateServiceDto, Service>()
                        .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()));

                CreateMap<Feature, FeatureDto>()
                        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
                CreateMap<AddFeatureDto, Feature>();
                CreateMap<UpdateFeatureDto, Feature>();
            
                CreateMap<Method, MethodDto>()
                        .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
                CreateMap<AddMethodDto, Method>();
                CreateMap<UpdateMethodDto, Method>();
        }
    }
}