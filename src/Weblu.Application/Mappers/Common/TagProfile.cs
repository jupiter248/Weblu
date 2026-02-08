using AutoMapper;
using Weblu.Application.Dtos.Common.TagDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Tags;

namespace Weblu.Application.Mappers.Common
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>()
                    .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}