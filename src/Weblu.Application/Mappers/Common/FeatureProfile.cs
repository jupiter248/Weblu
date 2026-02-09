using AutoMapper;
using Weblu.Application.DTOs.Common.FeatureDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Common.Features;

namespace Weblu.Application.Mappers.Common
{
    public class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<Feature, FeatureDTO>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateFeatureDTO, Feature>();
            CreateMap<UpdateFeatureDTO, Feature>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

        }
    }
}