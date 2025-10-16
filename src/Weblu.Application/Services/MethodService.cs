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

namespace Weblu.Application.Services
{
    public class MethodService : IMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MethodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MethodDto> AddMethodAsync(AddMethodDto addMethodDto)
        {
            Method method = _mapper.Map<Method>(addMethodDto);

            await _unitOfWork.Methods.AddMethodAsync(method);
            await _unitOfWork.CommitAsync();

            MethodDto methodDto = _mapper.Map<MethodDto>(method);
            return methodDto;
        }

        public async Task DeleteMethodAsync(int methodId)
        {
            Method? method = await _unitOfWork.Methods.GetMethodByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

            _unitOfWork.Methods.DeleteMethod(method);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<MethodDto>> GetAllMethodsAsync(MethodParameters methodParameters)
        {
            List<Method> methods = await _unitOfWork.Methods.GetAllMethodsAsync(methodParameters);
            List<MethodDto> methodDtos = _mapper.Map<List<MethodDto>>(methods);
            return methodDtos;
        }

        public async Task<MethodDto> GetMethodByIdAsync(int methodId)
        {
            Method? method = await _unitOfWork.Methods.GetMethodByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            MethodDto methodDto = _mapper.Map<MethodDto>(method);
            return methodDto;
        }

        public async Task<MethodDto> UpdateMethodAsync(int methodId, UpdateMethodDto updateMethodDto)
        {
            Method? method = await _unitOfWork.Methods.GetMethodByIdAsync(methodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);
            method = _mapper.Map(updateMethodDto, method);

            method.UpdatedAt = DateTimeOffset.Now;

            _unitOfWork.Methods.UpdateMethod(method);
            await _unitOfWork.CommitAsync();

            MethodDto methodDto = _mapper.Map<MethodDto>(method);
            return methodDto;
        }
    }
}