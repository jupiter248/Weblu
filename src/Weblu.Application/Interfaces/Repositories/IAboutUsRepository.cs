using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Domain.Entities.About;

namespace Weblu.Application.Interfaces.Repositories
{
    public interface IAboutUsRepository
    {
        Task<IReadOnlyList<AboutUs>> GetAllAboutUsInfosAsync();
        Task<AboutUs?> GetAboutUsInfoByIdAsync(int aboutUsId);
        Task AddAboutUsAsync(AboutUs aboutUs);
        void UpdateAboutUs(AboutUs aboutUs);
        void DeleteAboutUs(AboutUs aboutUs);
    }
}