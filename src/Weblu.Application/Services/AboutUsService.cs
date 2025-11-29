using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Common.Dtos;
using Weblu.Application.Dtos.AboutUsDtos;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public AboutUsService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHost)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
        }
        public async Task<AboutUsDto> AddAboutUsAsync(AddAboutUsDto addAboutUsDto)
        {
            AboutUs newAboutUs = _mapper.Map<AboutUs>(addAboutUsDto);

            await _unitOfWork.AboutUs.AddAboutUsAsync(newAboutUs);
            await _unitOfWork.CommitAsync();

            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(newAboutUs);
            return aboutUsDto;
        }

        public async Task DeleteAboutUsAsync(int aboutUsId)
        {
            AboutUs aboutUs = await _unitOfWork.AboutUs.GetAboutUsInfoByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);

            if (!string.IsNullOrEmpty(aboutUs.HeadImageUrl))
            {
                await MediaManager.DeleteMedia(_webHost, aboutUs.HeadImageUrl);
            }

            _unitOfWork.AboutUs.DeleteAboutUs(aboutUs);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAboutUsHeadImageAsync(int aboutUsId)
        {
            AboutUs aboutUs = await _unitOfWork.AboutUs.GetAboutUsInfoByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);

            if (string.IsNullOrEmpty(aboutUs.HeadImageUrl))
            {
                throw new BadRequestException(AboutUsErrorCodes.HeadImageIsEmpty);
            }
            else
                await MediaManager.DeleteMedia(_webHost, aboutUs.HeadImageUrl);

            aboutUs.HeadImageUrl = null;
            aboutUs.HeadImageAltText = null;

            await _unitOfWork.CommitAsync();
        }

        public async Task<AboutUsDto> GetAboutUsInfoByIdAsync(int aboutUsId)
        {
            AboutUs aboutUs = await _unitOfWork.AboutUs.GetAboutUsInfoByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);
            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(aboutUs);
            return aboutUsDto;
        }

        public async Task<List<AboutUsDto>> GetAllAboutUsInfosAsync(AboutUsParameters aboutUsParameters)
        {
            IReadOnlyList<AboutUs> aboutUs = await _unitOfWork.AboutUs.GetAllAboutUsInfosAsync(aboutUsParameters);
            List<AboutUsDto> aboutUsDtos = _mapper.Map<List<AboutUsDto>>(aboutUs);
            return aboutUsDtos;
        }

        public async Task<AboutUsDto> UpdateAboutUsAsync(int aboutUsId, UpdateAboutUsDto updateAboutUsDto)
        {
            AboutUs aboutUs = await _unitOfWork.AboutUs.GetAboutUsInfoByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);
            aboutUs = _mapper.Map(updateAboutUsDto, aboutUs);

            _unitOfWork.AboutUs.UpdateAboutUs(aboutUs);
            await _unitOfWork.CommitAsync();

            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(aboutUs);
            return aboutUsDto;
        }

        public async Task<AboutUsDto> UpdateHeadImageAboutUsAsync(int aboutUsId, UpdateImageAboutUsDto updateImageAboutUs)
        {

            AboutUs aboutUs = await _unitOfWork.AboutUs.GetAboutUsInfoByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);

            if (updateImageAboutUs.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(aboutUs.HeadImageUrl))
            {
                await MediaManager.DeleteMedia(_webHost, aboutUs.HeadImageUrl);
            }
            IFormFile image = updateImageAboutUs.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHost,
                    new MediaUploaderDto
                    {
                        Media = image,
                        MediaType = Domain.Enums.Common.Media.MediaType.picture
                    }
            );
            aboutUs.HeadImageUrl = $"uploads/{MediaType.picture}/{imageName}";
            aboutUs.HeadImageAltText = updateImageAboutUs.AltText;

            _unitOfWork.AboutUs.UpdateAboutUs(aboutUs);
            await _unitOfWork.CommitAsync();

            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(aboutUs);
            return aboutUsDto;
        }
    }
}