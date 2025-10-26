using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Common.Dtos;
using Weblu.Application.Dtos.ProfileDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Enums.Common;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Entities.Media;
using Microsoft.AspNetCore.Identity;
using Weblu.Domain.Errors.Users;


namespace Weblu.Application.Services
{
    public class ProfileImageService : IProfileImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        private readonly IMapper _mapper;
        public ProfileImageService(IUnitOfWork unitOfWork, IWebHostEnvironment webHost, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
            _mapper = mapper;
        }

        public async Task<ProfileDto> AddProfileAsync(AddProfileDto addProfileDto)
        {
            if (addProfileDto.OwnerType == ProfileMediaType.User)
            {
                bool appUserExists = await _unitOfWork.Users.UserExistsAsync(addProfileDto.OwnerId);
                if (!appUserExists)
                {
                    throw new NotFoundException(UserErrorCodes.UserNotFound);
                }
            }
            if (addProfileDto.OwnerType == ProfileMediaType.Contributor)
            {
            }

            bool userHasMainProfile = await _unitOfWork.Profiles.UserHasMainProfileAsync(addProfileDto.OwnerId);
            if (userHasMainProfile)
            {
                throw new ConflictException(UserErrorCodes.UserAlreadyAddedMainProfileImage);
            }

            if (addProfileDto.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            IFormFile image = addProfileDto.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHost,
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

            await _unitOfWork.Profiles.AddProfileAsync(profileModel);
            await _unitOfWork.CommitAsync();

            ProfileDto profileDto = _mapper.Map<ProfileDto>(profileModel);
            return profileDto;
        }

        public async Task DeleteProfileAsync(int profileId)
        {
            ProfileMedia image = await _unitOfWork.Profiles.GetProfileByIdAsync(profileId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            _unitOfWork.Profiles.DeleteProfile(image);
            await MediaManager.DeleteMedia(_webHost, image.Url);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ProfileDto>> GetAllProfilesAsync(ProfileMediaParameters profileMediaParameters)
        {
            List<ProfileMedia> images = await _unitOfWork.Profiles.GetAllProfilesAsync(profileMediaParameters);
            List<ProfileDto> imageDtos = _mapper.Map<List<ProfileDto>>(images);
            return imageDtos;
        }

        public async Task<ProfileDto> GetProfileByIdAsync(int profileId)
        {
            ProfileMedia image = await _unitOfWork.Profiles.GetProfileByIdAsync(profileId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            ProfileDto imageDto = _mapper.Map<ProfileDto>(image);
            return imageDto;
        }
    }
}