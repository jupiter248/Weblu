using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Common.Dtos;
using Weblu.Application.Dtos.ImageDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.Images;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Weblu.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHost;
        private readonly IMapper _mapper;


        public ImageService(IUnitOfWork unitOfWork, IWebHostEnvironment webHost, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
            _mapper = mapper;
        }
        public async Task<ImageDto> AddImageAsync(AddImageDto addImageDto)
        {
            if (addImageDto.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            IFormFile image = addImageDto.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHost,
                    new MediaUploaderDto
                    {
                        Media = image,
                        MediaType = MediaType.picture
                    }
            );

            ImageMedia imageModel = new ImageMedia()
            {
                Name = imageName,
                AltText = addImageDto.AltText,
                Url = $"uploads/{MediaType.picture}/{imageName}",
            };

            await _unitOfWork.Images.AddImageAsync(imageModel);
            await _unitOfWork.CommitAsync();

            ImageDto imageDto = _mapper.Map<ImageDto>(imageModel);
            return imageDto;
        }

        public async Task DeleteImageAsync(int imageId)
        {
            ImageMedia image = await _unitOfWork.Images.GetImageItemByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            _unitOfWork.Images.DeleteImage(image);
            await MediaManager.DeleteMedia(_webHost, image.Url);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ImageDto>> GetAllImagesAsync(ImageParameters imageParameters)
        {
            IReadOnlyList<ImageMedia> images = await _unitOfWork.Images.GetAllImagesAsync(imageParameters);
            List<ImageDto> imageDtos = _mapper.Map<List<ImageDto>>(images);
            return imageDtos;
        }

        public async Task<ImageDto> GetImageByIdAsync(int imageId)
        {
            ImageMedia image = await _unitOfWork.Images.GetImageItemByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            ImageDto imageDto = _mapper.Map<ImageDto>(image);
            return imageDto;
        }
    }
}