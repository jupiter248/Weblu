using AutoMapper;
using Weblu.Application.DTOs.FAQs.FAQCategoryDTOs;
using Weblu.Application.DTOs.FAQs.FAQDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.FAQs;

namespace Weblu.Application.Mappers.FAQs
{
    public class FAQProfile : Profile
    {
        public FAQProfile()
        {
            CreateMap<FAQ, FAQDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(f => f.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(f => f.UpdatedAt.HasValue ? f.UpdatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.PublishedAt, opt => opt.MapFrom(f => f.PublishedAt.HasValue ? f.PublishedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name ?? string.Empty));

            CreateMap<UpdateFAQDTO, FAQ>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTimeOffset.Now));

            CreateMap<CreateFAQDTO, FAQ>();

            CreateMap<FAQCategory, FAQCategoryDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
            CreateMap<CreateFAQCategoryDTO, FAQCategory>();
            CreateMap<UpdateFAQCategoryDTO, FAQCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}