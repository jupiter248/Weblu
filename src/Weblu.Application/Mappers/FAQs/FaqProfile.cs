using AutoMapper;
using Weblu.Application.Dtos.FAQs.FAQCategoryDtos;
using Weblu.Application.Dtos.FAQs.FAQDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.FAQs;

namespace Weblu.Application.Mappers.FAQs
{
    public class FAQProfile : Profile
    {
        public FAQProfile()
        {
            CreateMap<FAQ, FAQDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(f => f.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(f => f.UpdatedAt.HasValue ? f.UpdatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.ActivatedAt, opt => opt.MapFrom(f => f.ActivatedAt.HasValue ? f.ActivatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name ?? string.Empty));

            CreateMap<UpdateFAQDto, FAQ>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTimeOffset.Now));

            CreateMap<CreateFAQDto, FAQ>()
                .ForMember(dest => dest.ActivatedAt, opt =>
                {
                    opt.PreCondition(a => a.IsActive);
                    opt.MapFrom(_ => DateTimeOffset.Now);
                });

            CreateMap<FAQCategory, FAQCategoryDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
            CreateMap<CreateFAQCategoryDto, FAQCategory>();
            CreateMap<UpdateFAQCategoryDto, FAQCategory>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}