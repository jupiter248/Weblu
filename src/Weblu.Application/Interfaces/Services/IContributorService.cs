using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.ContributorDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IContributorService
    {
        Task<List<ContributorDto>> GetAllContributorsAsync(ContributorParameters contributorParameters);
        Task<ContributorDto> GetContributorByIdAsync(int contributorId);
        Task<ContributorDto> AddContributorAsync(AddContributorDto addContributorDto);
        Task<ContributorDto> UpdateContributorAsync(int currentContributorId, UpdateContributorDto updateContributorDto);
        Task<ContributorDto> UpdateProfileImageContributorAsync(int currentContributorId, UpdateProfileImageContributorDto updateProfileImage);
        Task DeleteContributorProfileAsync(int contributorId);
        Task DeleteContributorAsync(int contributorId);
    }
}