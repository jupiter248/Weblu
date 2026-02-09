using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.DTOs.About.AboutUsDTOs;
using Weblu.Application.DTOs.Images.MediaDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.About;
using Weblu.Application.Interfaces.Services.About;
using Weblu.Application.Parameters.About;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.About;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Services.About
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFilePathProviderService _webHost;
        private readonly string _webHostPath;

        public AboutUsService(IUnitOfWork unitOfWork, IMapper mapper, IFilePathProviderService webHost, IAboutUsRepository aboutUsRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
            _aboutUsRepository = aboutUsRepository;
            _webHostPath = webHost.GetWebRootPath();
        }
        public async Task DeleteHeadImageAsync(int aboutUsId)
        {
            AboutUs aboutUs = await _aboutUsRepository.GetByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);

            if (string.IsNullOrEmpty(aboutUs.HeadImageUrl))
            {
                throw new BadRequestException(AboutUsErrorCodes.HeadImageIsEmpty);
            }
            else
                await MediaManager.DeleteMedia(_webHostPath, aboutUs.HeadImageUrl);

            aboutUs.HeadImageUrl = null;
            aboutUs.HeadImageAltText = null;
            aboutUs.MarkUpdated();
            await _unitOfWork.CommitAsync();
        }

        public async Task<AboutUsDTO> UpdateAsync(int aboutUsId, UpdateAboutUsDTO updateAboutUsDTO)
        {
            AboutUs aboutUs = await _aboutUsRepository.GetByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);
            aboutUs = _mapper.Map(updateAboutUsDTO, aboutUs);

            aboutUs.MarkUpdated();
            _aboutUsRepository.Update(aboutUs);
            await _unitOfWork.CommitAsync();

            AboutUsDTO aboutUsDTO = _mapper.Map<AboutUsDTO>(aboutUs);
            return aboutUsDTO;
        }

        public async Task<AboutUsDTO> ChangeHeadImageAsync(int aboutUsId, ChangeAboutUsImageDTO changeAboutUsImageDTO)
        {

            AboutUs aboutUs = await _aboutUsRepository.GetByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);

            if (changeAboutUsImageDTO.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(aboutUs.HeadImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, aboutUs.HeadImageUrl);
            }
            var image = changeAboutUsImageDTO.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDTO
                    {
                        Media = image,
                        MediaType = MediaType.picture
                    }
            );
            aboutUs.HeadImageUrl = $"uploads/{MediaType.picture}/{imageName}";
            aboutUs.HeadImageAltText = changeAboutUsImageDTO.AltText;

            aboutUs.MarkUpdated();
            _aboutUsRepository.Update(aboutUs);
            await _unitOfWork.CommitAsync();

            AboutUsDTO aboutUsDTO = _mapper.Map<AboutUsDTO>(aboutUs);
            return aboutUsDTO;
        }

        public async Task<AboutUsDTO> GetAsync()
        {
            AboutUs aboutUs = await _aboutUsRepository.GetAsync() ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);
            AboutUsDTO aboutUsDTO = _mapper.Map<AboutUsDTO>(aboutUs);
            return aboutUsDTO;
        }
    }
}