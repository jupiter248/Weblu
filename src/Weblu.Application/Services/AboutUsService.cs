using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities.About;
using Weblu.Domain.Errors.AboutUs;

namespace Weblu.Application.Services
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AboutUsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            _unitOfWork.AboutUs.DeleteAboutUs(aboutUs);
            await _unitOfWork.CommitAsync();
        }

        public async Task<AboutUsDto?> GetAboutUsInfoByIdAsync(int aboutUsId)
        {
            AboutUs aboutUs = await _unitOfWork.AboutUs.GetAboutUsInfoByIdAsync(aboutUsId) ?? throw new NotFoundException(AboutUsErrorCodes.NotFound);
            AboutUsDto aboutUsDto = _mapper.Map<AboutUsDto>(aboutUs);
            return aboutUsDto;
        }

        public async Task<List<AboutUsDto>> GetAllAboutUsInfosAsync()
        {
            IReadOnlyList<AboutUs> aboutUs = await _unitOfWork.AboutUs.GetAllAboutUsInfosAsync();
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
    }
}