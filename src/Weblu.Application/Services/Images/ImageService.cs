using AutoMapper;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Media;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Domain.Errors.Images;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Services.Images;
using Weblu.Application.Interfaces.Repositories.Images;
using Weblu.Application.DTOs.Images.ImageDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.DTOs.Images.MediaDTOs;
using Weblu.Application.Parameters.Images;
using Weblu.Application.Interfaces.Repositories;
namespace Weblu.Application.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageRepository _imageRepository;
        private readonly IFilePathProviderService _webHost;
        private readonly IMapper _mapper;
        private readonly string _webHostPath;


        public ImageService(IUnitOfWork unitOfWork, IFilePathProviderService webHost, IMapper mapper, IImageRepository imageRepository)
        {
            _unitOfWork = unitOfWork;
            _webHost = webHost;
            _mapper = mapper;
            _imageRepository = imageRepository;
            _webHostPath = webHost.GetWebRootPath();
        }
        public async Task<ImageDTO> AddAsync(AddImageDTO addImageDTO)
        {
            if (addImageDTO.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            var image = addImageDTO.Image;
            string imageName = await MediaManager.UploadMedia(
                    _webHostPath,
                    new MediaUploaderDTO
                    {
                        Media = image,
                        MediaType = MediaType.picture
                    }
            );

            ImageMedia imageModel = new ImageMedia()
            {
                Name = imageName,
                AltText = addImageDTO.AltText,
                Url = $"uploads/{MediaType.picture}/{imageName}",
            };

            _imageRepository.Add(imageModel);
            await _unitOfWork.CommitAsync();

            ImageDTO imageDTO = _mapper.Map<ImageDTO>(imageModel);
            return imageDTO;
        }

        public async Task DeleteAsync(int imageId)
        {
            ImageMedia image = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);

            _imageRepository.Remove(image);
            await MediaManager.DeleteMedia(_webHostPath, image.Url);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ImageDTO>> GetAllAsync(ImageParameters imageParameters)
        {
            IReadOnlyList<ImageMedia> images = await _imageRepository.GetAllAsync(imageParameters);
            List<ImageDTO> imageDTOs = _mapper.Map<List<ImageDTO>>(images);
            return imageDTOs;
        }

        public async Task<ImageDTO> GetByIdAsync(int imageId)
        {
            ImageMedia image = await _imageRepository.GetByIdAsync(imageId) ?? throw new NotFoundException(ImageErrorCodes.ImageNotFound);
            ImageDTO imageDTO = _mapper.Map<ImageDTO>(image);
            return imageDTO;
        }
    }
}