using Weblu.Application.Dtos.Common.ContributorDtos;
using Weblu.Application.Parameters.Common;

namespace Weblu.Application.Interfaces.Services.Common
{
    public interface IContributorService
    {
        Task<List<ContributorDto>> GetAllAsync(ContributorParameters contributorParameters);
        Task<ContributorDto> GetByIdAsync(int contributorId);
        Task<ContributorDto> CreateAsync(CreateContributorDto addContributorDto);
        Task<ContributorDto> UpdateAsync(int currentContributorId, UpdateContributorDto updateContributorDto);
        Task<ContributorDto> ChangeProfileImageAsync(int currentContributorId, ChangeContributorProfileImageDto ChangeProfileImage);
        Task DeleteProfileAsync(int contributorId);
        Task DeleteAsync(int contributorId);
        Task Publish(int contributorId);
        Task Unpublish(int contributorId);
    }
}