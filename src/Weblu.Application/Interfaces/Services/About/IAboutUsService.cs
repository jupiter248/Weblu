using Weblu.Application.Dtos.About.AboutUsDtos;
using Weblu.Application.Parameters.About;

namespace Weblu.Application.Interfaces.Services.About
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