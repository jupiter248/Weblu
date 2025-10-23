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


namespace Weblu.Application.Services
{
    public class ProfileUserService : IProfileUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        private readonly IMapper _mapper;
        public ProfileUserService(IUnitOfWork unitOfWork, IWebHostEnvironment webHost, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
            _mapper = mapper;
        }

        public async Task<ProfileDto> AddProfileAsync(AddProfileDto addProfileDto)
        {
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
                        MediaType = MediaType.picture
                    }
            );

            ProfileMedia profileModel = new ProfileMedia()
            {
                Name = imageName,
                AltText = addProfileDto.AltText,
                Url = $"uploads/{MediaType.picture}/{imageName}",
            };

            await _unitOfWork.Profiles.AddProfileAsync(profileModel);
            await _unitOfWork.CommitAsync();

            ProfileDto profileDto = _mapper.Map<ProfileDto>(profileModel);
            return profileDto;
            }

        public Task DeleteProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProfileDto>> GetAllProfilesAsync(ProfileMediaParameters profileMediaParameters)
        {
            throw new NotImplementedException();
        }

        public Task<ProfileDto> GetProfileByIdAsync(int profileId)
        {
            throw new NotImplementedException();
        }
    }
}