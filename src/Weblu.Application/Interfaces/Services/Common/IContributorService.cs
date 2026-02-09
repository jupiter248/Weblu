using Weblu.Application.DTOs.Common.ContributorDTOs;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface IContributorService
    {
        Task<List<ContributorDTO>> GetAllAsync(ContributorParameters contributorParameters);
        Task<ContributorDTO> GetByIdAsync(int contributorId);
        Task<ContributorDTO> CreateAsync(CreateContributorDTO addContributorDTO);
        Task<ContributorDTO> UpdateAsync(int currentContributorId, UpdateContributorDTO updateContributorDTO);
        Task<ContributorDTO> ChangeProfileImageAsync(int currentContributorId, ChangeContributorProfileImageDTO ChangeProfileImage);
        Task DeleteProfileAsync(int contributorId);
        Task DeleteAsync(int contributorId);
        Task Publish(int contributorId);
        Task Unpublish(int contributorId);
    }
}