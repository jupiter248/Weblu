using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.DTOs.About.SocialMediaDTOs;
using Weblu.Application.DTOs.Images.MediaDTOs;
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

        public async Task<SocialMediaDTO> CreateAsync(CreateSocialMediaDTO createSocialMediaDTO)
        {
            SocialMedia socialMedia = _mapper.Map<SocialMedia>(createSocialMediaDTO);

            _socialMediaRepository.Add(socialMedia);
            await _unitOfWork.CommitAsync();

            SocialMediaDTO socialMediaDTO = _mapper.Map<SocialMediaDTO>(socialMedia);
            return socialMediaDTO;
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
        public async Task<List<SocialMediaDTO>> GetAllAsync(SocialMediaParameters socialMediaParameters)
        {
            IReadOnlyList<SocialMedia> socialMedias = await _socialMediaRepository.GetAllAsync(socialMediaParameters);
            List<SocialMediaDTO> socialMediaDTOs = _mapper.Map<List<SocialMediaDTO>>(socialMedias);
            return socialMediaDTOs;
        }

        public async Task<SocialMediaDTO> GetByIdAsync(int socialMediaId)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);
            SocialMediaDTO socialMediaDTO = _mapper.Map<SocialMediaDTO>(socialMedia);
            return socialMediaDTO;
        }

        public async Task<SocialMediaDTO> ChangeIconAsync(int socialMediaId, ChangeSocialMediaIconDTO changeSocialMediaIconDTO)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);

            if (changeSocialMediaIconDTO.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(socialMedia.IconUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, socialMedia.IconUrl);
            }
            var icon = changeSocialMediaIconDTO.Image;
            string iconName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDTO
                    {
                        Media = icon,
                        MediaType = MediaType.Icon
                    }
            );
            socialMedia.IconUrl = $"uploads/{MediaType.Icon}/{iconName}";
            socialMedia.IconAltText = changeSocialMediaIconDTO.AltText;

            socialMedia.MarkUpdated();
            _socialMediaRepository.Update(socialMedia);
            await _unitOfWork.CommitAsync();

            SocialMediaDTO socialMediaDTO = _mapper.Map<SocialMediaDTO>(socialMedia);
            return socialMediaDTO;
        }

        public async Task<SocialMediaDTO> UpdateAsync(int socialMediaId, UpdateSocialMediaDTO updateSocialMediaDTO)
        {
            SocialMedia socialMedia = await _socialMediaRepository.GetByIdAsync(socialMediaId) ?? throw new NotFoundException(SocialMediaErrorCodes.NotFound);
            socialMedia = _mapper.Map(updateSocialMediaDTO, socialMedia);

            socialMedia.MarkUpdated();
            _socialMediaRepository.Update(socialMedia);
            await _unitOfWork.CommitAsync();

            SocialMediaDTO socialMediaDTO = _mapper.Map<SocialMediaDTO>(socialMedia);
            return socialMediaDTO;
        }
    }
}