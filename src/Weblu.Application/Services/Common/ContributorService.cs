using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.DTOs.Common.ContributorDTOs;
using Weblu.Application.DTOs.Images.MediaDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Repositories;
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
        private readonly IFilePathProviderService _webHost;
        private readonly string _webHostPath;

        public ContributorService(IUnitOfWork unitOfWork, IMapper mapper, IFilePathProviderService webHost, IContributorRepository contributorRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
            _contributorRepository = contributorRepository;
            _webHostPath = webHost.GetWebRootPath();

        }
        public async Task<ContributorDTO> CreateAsync(CreateContributorDTO addContributorDTO)
        {
            Contributor newContributor = _mapper.Map<Contributor>(addContributorDTO);

            _contributorRepository.Add(newContributor);
            await _unitOfWork.CommitAsync();

            ContributorDTO contributorDTO = _mapper.Map<ContributorDTO>(newContributor);
            return contributorDTO;
        }

        public async Task DeleteAsync(int contributorId)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            if (contributor.IsPublished) throw new ConflictException(ContributorErrorCodes.IsPublish);
            if (!string.IsNullOrEmpty(contributor.ProfileImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, contributor.ProfileImageUrl);
            }

            contributor.Delete();
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProfileAsync(int contributorId)
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
            contributor.MarkUpdated();
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ContributorDTO>> GetAllAsync(ContributorParameters contributorParameters)
        {
            IReadOnlyList<Contributor> contributors = await _contributorRepository.GetAllAsync(contributorParameters);
            List<ContributorDTO> contributorDTOs = _mapper.Map<List<ContributorDTO>>(contributors);
            return contributorDTOs;
        }

        public async Task<ContributorDTO> GetByIdAsync(int contributorId)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            ContributorDTO contributorDTO = _mapper.Map<ContributorDTO>(contributor);
            return contributorDTO;
        }

        public async Task<ContributorDTO> UpdateAsync(int currentContributorId, UpdateContributorDTO updateContributorDTO)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(currentContributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);
            contributor = _mapper.Map(updateContributorDTO, contributor);

            contributor.MarkUpdated();
            _contributorRepository.Update(contributor);
            await _unitOfWork.CommitAsync();

            ContributorDTO contributorDTO = _mapper.Map<ContributorDTO>(contributor);
            return contributorDTO;
        }

        public async Task<ContributorDTO> ChangeProfileImageAsync(int currentContributorId, ChangeContributorProfileImageDTO changeProfileImage)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(currentContributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            if (changeProfileImage.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(contributor.ProfileImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, contributor.ProfileImageUrl);
            }
            var image = changeProfileImage.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDTO
                    {
                        Media = image,
                        MediaType = MediaType.profile
                    }
            );

            contributor.ProfileImageUrl = $"uploads/{MediaType.profile}/{imageName}";
            contributor.ProfileImageAltText = changeProfileImage.AltText;

            contributor.MarkUpdated();
            _contributorRepository.Update(contributor);
            await _unitOfWork.CommitAsync();

            ContributorDTO contributorDTO = _mapper.Map<ContributorDTO>(contributor);
            return contributorDTO;
        }

        public async Task Publish(int contributorId)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            contributor.Publish();
            await _unitOfWork.CommitAsync();
        }

        public async Task Unpublish(int contributorId)
        {
            Contributor contributor = await _contributorRepository.GetByIdAsync(contributorId) ?? throw new NotFoundException(ContributorErrorCodes.ContributorNotFound);

            contributor.Unpublish();
            await _unitOfWork.CommitAsync();
        }
    }
}