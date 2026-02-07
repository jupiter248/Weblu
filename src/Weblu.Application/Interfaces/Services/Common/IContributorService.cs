using Weblu.Application.Dtos.Common.ContributorDtos;
using Weblu.Application.Parameters;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
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