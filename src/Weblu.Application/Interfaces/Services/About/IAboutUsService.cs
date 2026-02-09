using Weblu.Application.Dtos.About.AboutUsDtos;
using Weblu.Application.Parameters.About;

namespace Weblu.Application.Interfaces.Services.About
{
    public interface IAboutUsService
    {
        Task<AboutUsDto> GetAsync();
        Task<AboutUsDto> UpdateAsync(int aboutUsId, UpdateAboutUsDto updateAboutUsDto);
        Task<AboutUsDto> ChangeHeadImageAsync(int aboutUsId, ChangeAboutUsImageDto changeAboutUsImageDto);
        Task DeleteHeadImageAsync(int aboutUsId);
    }
}