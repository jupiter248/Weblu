using AutoMapper;
using Weblu.Application.DTOs.Common.MethodDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Common.Methods;

namespace Weblu.Application.Mappers.Common
{
    public class MethodProfile : Profile
    {
        public MethodProfile()
        {
            CreateMap<Method, MethodDTO>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateMethodDTO, Method>();
            CreateMap<UpdateMethodDTO, Method>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}