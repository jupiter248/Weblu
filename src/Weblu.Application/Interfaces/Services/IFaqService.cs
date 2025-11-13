using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weblu.Application.Dtos.FaqDtos;
using Weblu.Application.Parameters;

namespace Weblu.Application.Interfaces.Services
{
    public interface IFaqService
    {
        Task<List<FaqDto>> GetAllFaqsAsync(FaqParameters faqParameters);
        Task<FaqDto> GetFaqByIdAsync(int faqId);
        Task<FaqDto> AddFaqAsync(AddFaqDto addFaqDto);
        Task<FaqDto> UpdateFaqAsync(int currentFaqId, UpdateFaqDto updateFaqDto);
        Task DeleteFaqAsync(int faqId);
    }
}