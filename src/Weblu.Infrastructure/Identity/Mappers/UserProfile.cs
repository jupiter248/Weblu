using AutoMapper;
using Weblu.Application.DTOs.Users.UserDTOs;
using Weblu.Application.Helpers;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Identity.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
            CreateMap<UpdateUserDTO, AppUser>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));

        }
    }
}