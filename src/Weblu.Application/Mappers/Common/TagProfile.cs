using AutoMapper;
using Weblu.Application.DTOs.Common.TagDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Common.Tags;

namespace Weblu.Application.Mappers.Common
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDTO>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateTagDTO, Tag>();
            CreateMap<UpdateTagDTO, Tag>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}