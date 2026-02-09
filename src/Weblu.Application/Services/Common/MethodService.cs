using AutoMapper;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Application.Helpers;
using Weblu.Domain.Errors.Images;
using Weblu.Domain.Enums.Common.Media;
using Weblu.Application.Common.Interfaces;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.DTOs.Common.MethodDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.DTOs.Images.MediaDTOs;
using Weblu.Application.Parameters.Common;
using Weblu.Application.Interfaces.Repositories;

namespace Weblu.Application.Services.Common
{
    public class MethodService : IMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMethodRepository _methodRepository;
        private readonly IMapper _mapper;
        private readonly IFilePathProviderService _webHost;
        private readonly string _webHostPath;

        public MethodService(IUnitOfWork unitOfWork, IMapper mapper, IFilePathProviderService webHost, IMethodRepository methodRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHost = webHost;
            _methodRepository = methodRepository;
            _webHostPath = webHost.GetWebRootPath();
        }
        public async Task<MethodDTO> CreateAsync(CreateMethodDTO createMethodDTO)
        {
            Method method = _mapper.Map<Method>(createMethodDTO);

            _methodRepository.Add(method);
            await _unitOfWork.CommitAsync();

            MethodDTO methodDTO = _mapper.Map<MethodDTO>(method);
            return methodDTO;
        }
        public async Task DeleteAsync(int methodId)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            if (!string.IsNullOrEmpty(method.ImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, method.ImageUrl);
            }

            method.Delete();
            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteImageAsync(int methodId)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            if (string.IsNullOrEmpty(method.ImageUrl))
            {
                throw new BadRequestException(MethodErrorCodes.MethodImageIsEmpty);
            }
            else
                await MediaManager.DeleteMedia(_webHostPath, method.ImageUrl);

            method.ImageUrl = null;
            method.ImageAltText = null;

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<MethodDTO>> GetAllAsync(MethodParameters methodParameters)
        {
            IReadOnlyList<Method> methods = await _methodRepository.GetAllAsync(methodParameters);
            List<MethodDTO> methodDTOs = _mapper.Map<List<MethodDTO>>(methods);
            return methodDTOs;
        }

        public async Task<MethodDTO> GetByIdAsync(int methodId)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            MethodDTO methodDTO = _mapper.Map<MethodDTO>(method);
            return methodDTO;
        }

        public async Task<MethodDTO> UpdateAsync(int methodId, UpdateMethodDTO updateMethodDTO)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            method = _mapper.Map(updateMethodDTO, method);

            method.MarkUpdated();
            _methodRepository.Update(method);
            await _unitOfWork.CommitAsync();

            MethodDTO methodDTO = _mapper.Map<MethodDTO>(method);
            return methodDTO;
        }

        public async Task<MethodDTO> ChangeImageAsync(int methodId, ChangeMethodImageDTO changeMethodImageDTO)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            if (changeMethodImageDTO.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(method.ImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostPath, method.ImageUrl);
            }

            var image = changeMethodImageDTO.Image;
            string imageName = await MediaManager.UploadMedia(_webHostPath, new MediaUploaderDTO()
            {
                Media = image,
                MediaType = Domain.Enums.Common.Media.MediaType.picture
            });

            method.ImageUrl = $"uploads/{MediaType.picture}/{imageName}";
            method.ImageAltText = changeMethodImageDTO.AltText;

            _methodRepository.Update(method);
            await _unitOfWork.CommitAsync();

            MethodDTO methodDTO = _mapper.Map<MethodDTO>(method);
            return methodDTO;
        }
    }
}