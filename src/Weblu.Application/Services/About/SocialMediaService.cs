using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.About.SocialMediaDtos;
using Weblu.Application.Dtos.Images.MediaDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Repositories.About;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Services.About;
using Weblu.Application.Parameters.About;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Errors.About;
using Weblu.Application.Interfaces.Repositories;

namespace Weblu.Application.Services.About
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISocialMediaRepository _socialMediaRepository;
        private readonly IMapper _mapper;
        private readonly IFilePathProviderService _webHost;
        private readonly string _webHostPath;

        public SocialMediaService(IUnitOfWork unitOfWork, IMapper mapper, IFilePathProviderService webHost, ISocialMediaRepository socialMediaRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
            _socialMediaRepository = socialMediaRepository;
            _webHostPath = webHost.GetWebRootPath();
        }

        public async Task<SocialMediaDto> CreateAsync(CreateSocialMediaDto createSocialMediaDto)
        {
            SocialMedia socialMedia = _mapper.Map<SocialMedia>(createSocialMediaDto);

            _socialMediaRepository.Add(socialMedia);
            await _unitOfWork.CommitAsync();

            SocialMediaDto socialMediaDto = _mapper.Map<SocialMediaDto>(socialMedia);
            return socialMediaDto;
        }

        public async Task DeleteAsync(int socialMediaId)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);

            if (!string.IsNullOrEmpty(socialMedia.IconUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, socialMedia.IconUrl);
            }

            socialMedia.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task<List<SocialMediaDto>> GetAllAsync(SocialMediaParameters socialMediaParameters)
        {
            IReadOnlyList<SocialMedia> socialMedias = await _socialMediaRepository.GetAllAsync(socialMediaParameters);
            List<SocialMediaDto> socialMediaDtos = _mapper.Map<List<SocialMediaDto>>(socialMedias);
            return socialMediaDtos;
        }

        public async Task<SocialMediaDto> GetByIdAsync(int socialMediaId)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);
            SocialMediaDto socialMediaDto = _mapper.Map<SocialMediaDto>(socialMedia);
            return socialMediaDto;
        }

        public async Task<SocialMediaDto> ChangeIconAsync(int socialMediaId, ChangeSocialMediaIconDto changeSocialMediaIconDto)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);

            if (changeSocialMediaIconDto.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(socialMedia.IconUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, socialMedia.IconUrl);
            }
            var icon = changeSocialMediaIconDto.Image;
            string iconName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDto
                    {
                        Media = icon,
                        MediaType = MediaType.Icon
                    }
            );
            socialMedia.IconUrl = $"uploads/{MediaType.Icon}/{iconName}";
            socialMedia.IconAltText = changeSocialMediaIconDto.AltText;

            _socialMediaRepository.Update(socialMedia);
            await _unitOfWork.CommitAsync();

            SocialMediaDto socialMediaDto = _mapper.Map<SocialMediaDto>(socialMedia);
            return socialMediaDto;
        }

        public async Task<SocialMediaDto> UpdateAsync(int socialMediaId, UpdateSocialMediaDto updateSocialMediaDto)
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