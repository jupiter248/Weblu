using AutoMapper;
using Weblu.Application.Helpers;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Errors.Users;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Services.Images;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.DTOs.Images.ProfileDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.DTOs.Images.MediaDTOs;
using Weblu.Application.Parameters.Images;
using Weblu.Application.Interfaces.Repositories;


namespace Weblu.Application.Services.Images
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IProfileImageRepository _profileImageRepository;
        private readonly IFilePathProviderService _webHost;
        private readonly IMapper _mapper;
        private readonly string _webHostPath;
        public ProfileImageService(IUnitOfWork unitOfWork, IFilePathProviderService webHost, IMapper mapper,
            IProfileImageRepository profileImageRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
            _mapper = mapper;
            _profileImageRepository = profileImageRepository;
            _userRepository = userRepository;
            _webHostPath = webHost.GetWebRootPath();
        }

        public async Task<ProfileDTO> AddAsync(AddProfileDTO addProfileDTO)
        {
            if (addProfileDTO.OwnerType == ProfileMediaType.User)
            {
                bool appUserExists = await _userRepository.UserExistsAsync(addProfileDTO.OwnerId);
                if (!appUserExists)
                {
                    throw new NotFoundException(UserErrorCodes.UserNotFound);
                }
            }
            if (addProfileDTO.OwnerType == ProfileMediaType.Contributor)
            {
            }

            bool userHasMainProfile = await _profileImageRepository.UserHasMainProfileAsync(addProfileDTO.OwnerId);
            if (userHasMainProfile)
            {
                throw new ConflictException(UserErrorCodes.UserAlreadyAddedMainProfileImage);
            }

            if (addProfileDTO.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            var image = addProfileDTO.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDTO
                    {
                        Media = image,
                        MediaType = MediaType.profile
                    }
            );

            ProfileMedia profileModel = new ProfileMedia()
            {
                Name = imageName,
                AltText = addProfileDTO.AltText,
                Url = $"uploads/{MediaType.picture}/{imageName}",
                OwnerId = addProfileDTO.OwnerId,
                OwnerType = addProfileDTO.OwnerType,
                IsMain = addProfileDTO.IsMain
            };

            _profileImageRepository.Add(profileModel);
            await _unitOfWork.CommitAsync();

            ProfileDTO profileDTO = _mapper.Map<ProfileDTO>(profileModel);
            return profileDTO;
        }

        public async Task DeleteAsync(int profileId)
        {
            ProfileMedia image = await _profileImageRepository.GetByIdAsync(profileId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            _profileImageRepository.Remove(image);
            await MediaManager.DeleteMedia(_webHostPath, image.Url);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ProfileDTO>> GetAllAsync(ProfileMediaParameters profileMediaParameters)
        {
            IReadOnlyList<ProfileMedia> images = await _profileImageRepository.GetAllAsync(profileMediaParameters);
            List<ProfileDTO> imageDTOs = _mapper.Map<List<ProfileDTO>>(images);
            return imageDTOs;
        }

        public async Task<ProfileDTO> GetByIdAsync(int profileId)
        {
            ProfileMedia image = await _profileImageRepository.GetByIdAsync(profileId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            ProfileDTO imageDTO = _mapper.Map<ProfileDTO>(image);
            return imageDTO;
        }
    }
}