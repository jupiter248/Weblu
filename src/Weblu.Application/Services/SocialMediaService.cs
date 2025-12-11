using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Common.Dtos;
using Weblu.Application.Dtos.SocialMediaDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.SocialMedia;

namespace Weblu.Application.Services
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public SocialMediaService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost, ISocialMediaRepository socialMediaRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task<SocialMediaDto> AddSocialMediaAsync(AddSocialMediaDto addSocialMediaDto)
        {
            SocialMedia socialMedia = _mapper.Map<SocialMedia>(addSocialMediaDto);

            _socialMediaRepository.Add(socialMedia);
            await _unitOfWork.CommitAsync();

            SocialMediaDto socialMediaDto = _mapper.Map<SocialMediaDto>(socialMedia);
            return socialMediaDto;
        }

        public async Task DeleteSocialMediaAsync(int socialMediaId)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);

            if (!string.IsNullOrEmpty(socialMedia.IconUrl))
            {
                await MediaManager.DeleteMedia(_webHost, socialMedia.IconUrl);
            }

            _socialMediaRepository.Delete(socialMedia);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<SocialMediaDto>> GetAllSocialMediasAsync(SocialMediaParameters socialMediaParameters)
        {
            IReadOnlyList<SocialMedia> socialMedias = await _socialMediaRepository.GetAllAsync(socialMediaParameters);
            List<SocialMediaDto> socialMediaDtos = _mapper.Map<List<SocialMediaDto>>(socialMedias);
            return socialMediaDtos;
        }

        public async Task<SocialMediaDto> GetSocialMediaByIdAsync(int socialMediaId)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);
            SocialMediaDto socialMediaDto = _mapper.Map<SocialMediaDto>(socialMedia);
            return socialMediaDto;
        }

        public async Task<SocialMediaDto> UpdateSocialMediaIconAsync(int socialMediaId, UpdateSocialMediaIconDto updateSocialMediaIcon)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);

            if (updateSocialMediaIcon.Icon.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(socialMedia.IconUrl))
            {
                await MediaManager.DeleteMedia(_webHost, socialMedia.IconUrl);
            }
            IFormFile icon = updateSocialMediaIcon.Icon;
            string iconName = await MediaManager.UploadMedia(
                    _webHost,
                    new MediaUploaderDto
                    {
                        Media = icon,
                        MediaType = MediaType.Icon
                    }
            );
            socialMedia.IconUrl = $"uploads/{MediaType.Icon}/{iconName}";
            socialMedia.IconAltText = updateSocialMediaIcon.AltText;

            _socialMediaRepository.Update(socialMedia);
            await _unitOfWork.CommitAsync();

            SocialMediaDto socialMediaDto = _mapper.Map<SocialMediaDto>(socialMedia);
            return socialMediaDto;
        }

        public async Task<SocialMediaDto> UpdateSocialMediaAsync(int socialMediaId, UpdateSocialMediaDto updateSocialMediaDto)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);
            socialMedia = _mapper.Map(updateSocialMediaDto, socialMedia);

            _socialMediaRepository.Update(socialMedia);
            await _unitOfWork.CommitAsync();

            SocialMediaDto socialMediaDto = _mapper.Map<SocialMediaDto>(socialMedia);
            return socialMediaDto;
        }
    }
}