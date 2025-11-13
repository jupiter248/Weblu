using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FaqDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Faqs;

namespace Weblu.Application.Mappers
{
    public class FaqProfile : Profile
    {
        public FaqProfile()
        {
            CreateMap<Faq, FaqDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(f => f.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(f => f.UpdatedAt.HasValue ? f.UpdatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.ActivatedAt, opt => opt.MapFrom(f => f.ActivatedAt.HasValue ? f.ActivatedAt.Value.ToShamsi() : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name ?? string.Empty));

            CreateMap<UpdateFaqDto, Faq>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTimeOffset.Now));

            CreateMap<AddFaqDto, Faq>()
                .ForMember(dest => dest.ActivatedAt, opt =>
                {
                    opt.PreCondition(a => a.IsActive);
                    opt.MapFrom(_ => DateTimeOffset.Now);
                });

            CreateMap<Faq, FaqDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
            CreateMap<AddFaqDto, Faq>();
            CreateMap<UpdateFaqDto, Faq>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
        }
    }
}