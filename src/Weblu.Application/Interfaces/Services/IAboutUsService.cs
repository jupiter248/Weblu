using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Application.Dtos.ContributorDtos;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Interfaces.Services
{
    public interface IAboutUsService
    {
        Task<List<AboutUsDto>> GetAllAboutUsInfosAsync(AboutUsParameters aboutUsParameters);
        Task<AboutUsDto> GetAboutUsInfoByIdAsync(int aboutUsId);
        Task<AboutUsDto> AddAboutUsAsync(AddAboutUsDto addAboutUsDto);
        Task<AboutUsDto> UpdateAboutUsAsync(int aboutUsId, UpdateAboutUsDto updateAboutUsDto);
        Task<AboutUsDto> UpdateHeadImageAboutUsAsync(int aboutUsId, UpdateImageAboutUsDto updateImageAboutUs);
        Task DeleteAboutUsAsync(int aboutUsId);
        Task DeleteAboutUsHeadImageAsync(int aboutUsId);
    }
}