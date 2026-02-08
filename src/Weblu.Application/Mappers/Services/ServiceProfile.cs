using AutoMapper;
using Weblu.Application.Dtos.Services.ServiceDtos;
using Weblu.Application.Dtos.Services.ServiceDtos.ServiceImageDtos;
using Weblu.Application.Extensions;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Services;

namespace Weblu.Application.Mappers.Services
{
        public class ServiceProfile : Profile
        {
                public ServiceProfile()
                {
                        CreateMap<Service, ServiceSummaryDto>()
                            .ForMember(dest => dest.ThumbnailPictureUrl, opt => opt.MapFrom(src => src.ServiceImages.FirstOrDefault(i => i.IsThumbnail).Image.Url ?? string.Empty));
                        CreateMap<Service, ServiceDetailDto>()
                                .ForMember(dest => dest.ActivatedAt, opt => opt.MapFrom(src => src.ActivatedAt.HasValue ? src.ActivatedAt.Value.ToShamsi() : null))
                                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null))
                                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
                        CreateMap<CreateServiceDto, Service>()
                                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()));
                        CreateMap<UpdateServiceDto, Service>()
                                .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Title.Slugify()))
                                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
                                
                        CreateMap<ServiceImage, ServiceImageDto>()
                                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.Image.AddedAt.ToShamsi()))
                                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Image.Name))
                                .ForMember(dest => dest.AltText, opt => opt.MapFrom(src => src.Image.AltText))
                                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Image.Id))
                                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Image.Url))
                                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Image.Width))
                                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Image.Height))
                                .ForMember(dest => dest.IsThumbnail, opt => opt.MapFrom(src => src.IsThumbnail));

                }

        }
}