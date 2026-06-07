using Weblu.Application.DTOs.About.AboutUsDTOs;
using Weblu.Application.DTOs.Images.ImageDTOs;
using Weblu.Application.Parameters.About;

namespace Weblu.Application.Interfaces.Services.About
{
    public interface IAboutUsService
    {
        Task<AboutUsDTO> GetAsync();
        Task<AboutUsDTO> UpdateAsync(int aboutUsId, UpdateAboutUsDTO updateAboutUsDTO);
        Task<AboutUsDTO> ChangeHeadImageAsync(int aboutUsId, UploadImageDTO uploadImageDTO);
        Task DeleteHeadImageAsync(int aboutUsId);
    }
}