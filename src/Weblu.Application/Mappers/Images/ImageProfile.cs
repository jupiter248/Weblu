using AutoMapper;
using Weblu.Application.DTOs.Images.ImageDTOs;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Mappers.Images
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageMedia, ImageDTO>()
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()));
        }
    }
}