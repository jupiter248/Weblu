using AutoMapper;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.Images;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Services.Images;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.Dtos.Images.ImageDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Dtos.Images.MediaDtos;
using Weblu.Application.Parameters.Images;
namespace Weblu.Application.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageRepository _imageRepository;
        private readonly IFilePathProvider _webHost;
        private readonly IMapper _mapper;
        private readonly string _webHostPath;


        public ImageService(IUnitOfWork unitOfWork, IFilePathProvider webHost, IMapper mapper, IImageRepository imageRepository)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
            _mapper = mapper;
            _imageRepository = imageRepository;
            _webHostPath = webHost.GetWebRootPath();
        }
        public async Task<ImageDto> AddImageAsync(AddImageDto addImageDto)
        {
            if (addImageDto.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            var image = addImageDto.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHostPath,
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

            _imageRepository.Add(imageModel);
            await _unitOfWork.CommitAsync();

            ImageDto imageDto = _mapper.Map<ImageDto>(imageModel);
            return imageDto;
        }

        public async Task DeleteImageAsync(int imageId)
        {
            ImageMedia image = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            _imageRepository.Remove(image);
            await MediaManager.DeleteMedia(_webHostPath, image.Url);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ImageDto>> GetAllImagesAsync(ImageParameters imageParameters)
        {
            IReadOnlyList<ImageMedia> images = await _imageRepository.GetAllAsync(imageParameters);
            List<ImageDto> imageDtos = _mapper.Map<List<ImageDto>>(images);
            return imageDtos;
        }

        public async Task<ImageDto> GetImageByIdAsync(int imageId)
        {
            ImageMedia image = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            ImageDto imageDto = _mapper.Map<ImageDto>(image);
            return imageDto;
        }
    }
}