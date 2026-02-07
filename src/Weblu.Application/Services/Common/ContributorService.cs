using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.Common.ContributorDtos;
using Weblu.Application.Dtos.Images.MediaDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Parameters.Common;
using Weblu.Domain.Entities.Common.Contributors;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Services.Common
{
    public class ContributorService : IContributorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IContributorRepository _contributorRepository;
        private readonly IMapper _mapper;
        private readonly IFilePathProvider _webHost;
        private readonly string _webHostPath;

        public ContributorService(IUnitOfWork unitOfWork, IMapper mapper, IFilePathProvider webHost, IContributorRepository contributorRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
            _contributorRepository = contributorRepository;
            _webHostPath = webHost.GetWebRootPath();

        }
        public async Task<ContributorDto> AddContributorAsync(AddContributorDto addContributorDto)
        {
            Contributor newContributor = _mapper.Map<Contributor>(addContributorDto);

            _contributorRepository.Add(newContributor);
            await _unitOfWork.CommitAsync();

            ContributorDto contributorDto = _mapper.Map<ContributorDto>(newContributor);
            return contributorDto;
        }

        public async Task DeleteContributorAsync(int contributorId)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (!string.IsNullOrEmpty(contributor.ProfileImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, contributor.ProfileImageUrl);
            }

            contributor.Delete();
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteContributorProfileAsync(int contributorId)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (string.IsNullOrEmpty(contributor.ProfileImageUrl))
            {
                throw new BadRequestException(ContributorErrorCodes.ContributorProfileIsEmpty);
            }
            else
                await MediaManager.DeleteMedia(_webHostPath, contributor.ProfileImageUrl);

            contributor.ProfileImageUrl = null;
            contributor.ProfileImageAltText = null;

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ContributorDto>> GetAllContributorsAsync(ContributorParameters contributorParameters)
        {
            IReadOnlyList<Contributor> contributors = await _contributorRepository.GetAllAsync(contributorParameters);
            List<ContributorDto> contributorDtos = _mapper.Map<List<ContributorDto>>(contributors);
            return contributorDtos;
        }

        public async Task<ContributorDto> GetContributorByIdAsync(int contributorId)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            ContributorDto contributorDto = _mapper.Map<ContributorDto>(contributor);
            return contributorDto;
        }

        public async Task<ContributorDto> UpdateContributorAsync(int currentContributorId, UpdateContributorDto updateContributorDto)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(currentContributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            contributor = _mapper.Map(updateContributorDto, contributor);

            _contributorRepository.Update(contributor);
            await _unitOfWork.CommitAsync();

            ContributorDto contributorDto = _mapper.Map<ContributorDto>(contributor);
            return contributorDto;
        }

        public async Task<ContributorDto> UpdateProfileImageContributorAsync(int currentContributorId, UpdateProfileImageContributorDto updateProfileImage)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(currentContributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (updateProfileImage.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(contributor.ProfileImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, contributor.ProfileImageUrl);
            }
            var image = updateProfileImage.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDto
                    {
                        Media = image,
                        MediaType = MediaType.profile
                    }
            );

            contributor.ProfileImageUrl = $"uploads/{MediaType.profile}/{imageName}";
            contributor.ProfileImageAltText = updateProfileImage.AltText;

            _contributorRepository.Update(contributor);
            await _unitOfWork.CommitAsync();

            ContributorDto contributorDto = _mapper.Map<ContributorDto>(contributor);
            return contributorDto;
        }
    }
}