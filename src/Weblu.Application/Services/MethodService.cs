using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.MethodDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Methods;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Methods;
using Weblu.Application.Helpers;
using Microsoft.AspNetCore.Hosting;
using Weblu.Domain.Errors.Images;
using Microsoft.AspNetCore.Http;
using Weblu.Application.Common.Dtos;
using Weblu.Domain.Enums.Common.Media;

namespace Weblu.Application.Services
{
    public class MethodService : IMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMethodRepository _methodRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public MethodService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment, IMethodRepository methodRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _methodRepository = methodRepository;
        }
        public async Task<MethodDto> AddMethodAsync(AddMethodDto addMethodDto)
        {
            Method method = _mapper.Map<Method>(addMethodDto);

            _methodRepository.Add(method);
            await _unitOfWork.CommitAsync();

            MethodDto methodDto = _mapper.Map<MethodDto>(method);
            return methodDto;
        }

        public async Task DeleteMethodAsync(int methodId)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            if (!string.IsNullOrEmpty(method.ImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostEnvironment, method.ImageUrl);
            }

            _methodRepository.Delete(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteMethodImageAsync(int methodId)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            if (string.IsNullOrEmpty(method.ImageUrl))
            {
                throw new BadRequestException(MethodErrorCodes.MethodImageIsEmpty);
            }
            else
                await MediaManager.DeleteMedia(_webHostEnvironment, method.ImageUrl);

            method.ImageUrl = null;
            method.ImageAltText = null;

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<MethodDto>> GetAllMethodsAsync(MethodParameters methodParameters)
        {
            IReadOnlyList<Method> methods = await _methodRepository.GetAllAsync(methodParameters);
            List<MethodDto> methodDtos = _mapper.Map<List<MethodDto>>(methods);
            return methodDtos;
        }

        public async Task<MethodDto> GetMethodByIdAsync(int methodId)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            MethodDto methodDto = _mapper.Map<MethodDto>(method);
            return methodDto;
        }

        public async Task<MethodDto> UpdateMethodAsync(int methodId, UpdateMethodDto updateMethodDto)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            method = _mapper.Map(updateMethodDto, method);

            _methodRepository.Update(method);
            await _unitOfWork.CommitAsync();

            MethodDto methodDto = _mapper.Map<MethodDto>(method);
            return methodDto;
        }

        public async Task<MethodDto> UpdateMethodImageAsync(int methodId, UpdateMethodImageDto updateMethodImageDto)
        {
            Method? method = await _methodRepository.GetByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            if (updateMethodImageDto.Image.Length < 0)
            {
                throw new BadRequestException(ImageErrorCodes.ImageFileInvalid);
            }
            if (!string.IsNullOrEmpty(method.ImageUrl))
            {
                await MediaManager.DeleteMedia(_webHostEnvironment, method.ImageUrl);
            }

            IFormFile image = updateMethodImageDto.Image;
            string imageName = await MediaManager.UploadMedia(_webHostEnvironment, new MediaUploaderDto()
            {
                Media = image,
                MediaType = Domain.Enums.Common.Media.MediaType.picture
            });

            method.ImageUrl = $"uploads/{MediaType.picture}/{imageName}";
            method.ImageAltText = updateMethodImageDto.AltText;

            _methodRepository.Update(method);
            await _unitOfWork.CommitAsync();

            MethodDto methodDto = _mapper.Map<MethodDto>(method);
            return methodDto;
        }
    }
}