using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Dtos.ProfileDtos;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Media;

namespace Weblu.Application.Interfaces.Services
{
    public interface IProfileUserService
    {
        Task<List<ProfileDto>> GetAllProfilesAsync(ProfileMediaParameters profileMediaParameters);
        Task<ProfileDto> GetProfileByIdAsync(int profileId);
        Task<ProfileDto> AddProfileAsync(AddProfileDto addProfileDto);
        Task DeleteProfileAsync(int profileId);
    }
}