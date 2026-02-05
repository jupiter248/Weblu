using AutoMapper;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Application.Dtos.MediaDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.AboutUs;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Services
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAboutUsRepository _aboutUsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFilePathProvider _webHost;
        private readonly string _webHostPath;

        public AboutUsService(IUnitOfWork unitOfWork, IMapper mapper, IFilePathProvider webHost, IAboutUsRepository aboutUsRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
            _aboutUsRepository = aboutUsRepository;
            _webHostPath = webHost.GetWebRootPath();
        }
        public async Task<AboutUsDto> AddAboutUsAsync(AddAboutUsDto addAboutUsDto)
        {
            AboutUs newAboutUs = _mapper.Map<AboutUs>(addAboutUsDto);

            _aboutUsRepository.Add(newAboutUs);
            await _unitOfWork.CommitAsync();

            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(newAboutUs);
            return aboutUsDto;
        }

        public async Task DeleteAboutUsAsync(int aboutUsId)
        {
            AboutUs aboutUs = await _aboutUsRepository.GetByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);

            if (!string.IsNullOrEmpty(aboutUs.HeadImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, aboutUs.HeadImageUrl);
            }

            aboutUs.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteAboutUsHeadImageAsync(int aboutUsId)
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

            await _unitOfWork.CommitAsync();
        }

        public async Task<AboutUsDto> GetAboutUsInfoByIdAsync(int aboutUsId)
        {
            AboutUs aboutUs = await _aboutUsRepository.GetByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);
            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(aboutUs);
            return aboutUsDto;
        }

        public async Task<List<AboutUsDto>> GetAllAboutUsInfosAsync(AboutUsParameters aboutUsParameters)
        {
            IReadOnlyList<AboutUs> aboutUs = await _aboutUsRepository.GetAllAsync(aboutUsParameters);
            List<AboutUsDto> aboutUsDtos = _mapper.Map<List<AboutUsDto>>(aboutUs);
            return aboutUsDtos;
        }

        public async Task<AboutUsDto> UpdateAboutUsAsync(int aboutUsId, UpdateAboutUsDto updateAboutUsDto)
        {
            AboutUs aboutUs = await _aboutUsRepository.GetByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);
            aboutUs = _mapper.Map(updateAboutUsDto, aboutUs);

            _aboutUsRepository.Update(aboutUs);
            await _unitOfWork.CommitAsync();

            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(aboutUs);
            return aboutUsDto;
        }

        public async Task<AboutUsDto> UpdateHeadImageAboutUsAsync(int aboutUsId, UpdateImageAboutUsDto updateImageAboutUs)
        {

            AboutUs aboutUs = await _aboutUsRepository.GetByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);

            if (updateImageAboutUs.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(aboutUs.HeadImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, aboutUs.HeadImageUrl);
            }
            var image = updateImageAboutUs.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDto
                    {
                        Media = image,
                        MediaType = Domain.Enums.Common.Media.MediaType.picture
                    }
            );
            aboutUs.HeadImageUrl = $"uploads/{MediaType.picture}/{imageName}";
            aboutUs.HeadImageAltText = updateImageAboutUs.AltText;

            _aboutUsRepository.Update(aboutUs);
            await _unitOfWork.CommitAsync();

            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(aboutUs);
            return aboutUsDto;
        }
    }
}