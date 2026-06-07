using AutoMapper;
using Weblu.Application.DTOs.Images.ProfileDTOs;
using Weblu.Application.DTOs.Users.UserDTOs;
using Weblu.Application.Helpers;
using Weblu.Infrastructure.Identity.Entities;

namespace Weblu.Infrastructure.Identity.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this AppUser user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = user.FirstName + "" + user.LastName,
                CreatedAt = user.CreatedAt.ToShamsi(),
                UpdatedAt = user.UpdatedAt.HasValue ? user.UpdatedAt.Value.ToShamsi() : null,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = [],
                Profiles = user.Profiles.Select(p => new ProfileDTO()
                {
                    Id = p.Id,
                    Name = p.Name,
                    AltText = p.AltText,
                    IsMain = p.IsMain,
                    Height = p.Height,
                    OwnerId = p.OwnerId,
                    OwnerType = p.OwnerType.ToString(),
                    Size = p.FileSize,
                    Url = p.Url,
                    Width = p.Width,
                    AddedAt = p.CreatedAt.ToShamsi()
                }).ToList()
            };
        }
    }
}