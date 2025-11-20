using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Interfaces.Services
{
    public interface IAboutUsService
    {
        Task<List<AboutUsDto>> GetAllAboutUsInfosAsync();
        Task<AboutUsDto?> GetAboutUsInfoByIdAsync(int aboutUsId);
        Task<AboutUsDto> AddAboutUsAsync(AddAboutUsDto addAboutUsDto);
        Task<AboutUsDto> UpdateAboutUsAsync(int aboutUsId, UpdateAboutUsDto updateAboutUsDto);
        Task DeleteAboutUsAsync(int aboutUsId);
    }
}