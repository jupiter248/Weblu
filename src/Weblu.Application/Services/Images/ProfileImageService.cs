using AutoMapper;
using Weblu.Application.Helpers;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Errors.Users;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Repositories.Users;
using Weblu.Application.Interfaces.Services.Images;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.Dtos.Images.ProfileDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Dtos.Images.MediaDtos;
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

        public async Task<ProfileDto> AddAsync(AddProfileDto addProfileDto)
        {
            if (addProfileDto.OwnerType == ProfileMediaType.User)
            {
                bool appUserExists = await _userRepository.UserExistsAsync(addProfileDto.OwnerId);
                if (!appUserExists)
                {
                    throw new NotFoundException(UserErrorCodes.UserNotFound);
                }
            }
            if (addProfileDto.OwnerType == ProfileMediaType.Contributor)
            {
            }

            bool userHasMainProfile = await _profileImageRepository.UserHasMainProfileAsync(addProfileDto.OwnerId);
            if (userHasMainProfile)
            {
                throw new ConflictException(UserErrorCodes.UserAlreadyAddedMainProfileImage);
            }

            if (addProfileDto.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            var image = addProfileDto.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDto
                    {
                        Media = image,
                        MediaType = MediaType.profile
                    }
            );

            ProfileMedia profileModel = new ProfileMedia()
            {
                Name = imageName,
                AltText = addProfileDto.AltText,
                Url = $"uploads/{MediaType.picture}/{imageName}",
                OwnerId = addProfileDto.OwnerId,
                OwnerType = addProfileDto.OwnerType,
                IsMain = addProfileDto.IsMain
            };

            _profileImageRepository.Add(profileModel);
            await _unitOfWork.CommitAsync();

            ProfileDto profileDto = _mapper.Map<ProfileDto>(profileModel);
            return profileDto;
        }

        public async Task DeleteAsync(int profileId)
        {
            ProfileMedia image = await _profileImageRepository.GetByIdAsync(profileId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            _profileImageRepository.Remove(image);
            await MediaManager.DeleteMedia(_webHostPath, image.Url);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ProfileDto>> GetAllAsync(ProfileMediaParameters profileMediaParameters)
        {
            IReadOnlyList<ProfileMedia> images = await _profileImageRepository.GetAllAsync(profileMediaParameters);
            List<ProfileDto> imageDtos = _mapper.Map<List<ProfileDto>>(images);
            return imageDtos;
        }

        public async Task<ProfileDto> GetByIdAsync(int profileId)
        {
            ProfileMedia image = await _profileImageRepository.GetByIdAsync(profileId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            ProfileDto imageDto = _mapper.Map<ProfileDto>(image);
            return imageDto;
        }
    }
}