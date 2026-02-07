using AutoMapper;
using Weblu.Application.Dtos.Common.MethodDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Common.Methods;

namespace Weblu.Application.Mappers.Common
{
    public class MethodProfile : Profile
    {
        public MethodProfile()
        {
            CreateMap<Method, MethodDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<AddMethodDto, Method>();
            CreateMap<UpdateMethodDto, Method>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}