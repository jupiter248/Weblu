using Weblu.Application.Dtos.About.AboutUsDtos;
using Weblu.Application.Parameters.About;

namespace Weblu.Application.Interfaces.Services.About
{
    public interface IAboutUsService
    {
        Task<List<AboutUsDto>> GetAllAsync(AboutUsParameters aboutUsParameters);
        Task<AboutUsDto> GetByIdAsync(int aboutUsId);
        Task<AboutUsDto> CreateAsync(CreateAboutUsDto createAboutUsDto);
        Task<AboutUsDto> UpdateAsync(int aboutUsId, UpdateAboutUsDto updateAboutUsDto);
        Task<AboutUsDto> ChangeHeadImageAsync(int aboutUsId, ChangeAboutUsImageDto changeAboutUsImageDto);
        Task DeleteAsync(int aboutUsId);
        Task DeleteHeadImageAsync(int aboutUsId);
    }
}