using AutoMapper;
using Weblu.Application.Dtos.Images.ImageDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Mappers.Images
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageMedia, ImageDto>()
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => src.AddedAt.ToShamsi()));
        }
    }
}