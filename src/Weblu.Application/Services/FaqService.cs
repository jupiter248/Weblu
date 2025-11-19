using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FaqDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;
using Weblu.Domain.Errors.Faqs;

namespace Weblu.Application.Services
{
    public class FaqService : IFaqService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FaqService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FaqDto> AddFaqAsync(AddFaqDto addFaqDto)
        {
            Faq faq = _mapper.Map<Faq>(addFaqDto);

            FaqCategory faqCategory = await _unitOfWork.FaqCategories.GetFaqCategoryByIdAsync(addFaqDto.CategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            faq.Category = faqCategory;

            await _unitOfWork.Faqs.AddFaqAsync(faq);
            await _unitOfWork.CommitAsync();

            FaqDto faqDto = _mapper.Map<FaqDto>(faq);
            return faqDto;
        }

        public async Task DeleteFaqAsync(int faqId)
        {
            Faq faq = await _unitOfWork.Faqs.GetFaqByIdAsync(faqId) ?? throw new NotFoundException(FaqErrorCodes.NotFound);

            _unitOfWork.Faqs.DeleteFaq(faq);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FaqDto>> GetAllFaqsAsync(FaqParameters faqParameters)
        {
            IReadOnlyList<Faq> faqs = await _unitOfWork.Faqs.GetAllFaqsAsync(faqParameters);
            List<FaqDto> faqDtos = _mapper.Map<List<FaqDto>>(faqs);
            return faqDtos;
        }

        public async Task<FaqDto> GetFaqByIdAsync(int faqId)
        {
            Faq faq = await _unitOfWork.Faqs.GetFaqByIdAsync(faqId) ?? throw new NotFoundException(FaqErrorCodes.NotFound);
            FaqDto faqDto = _mapper.Map<FaqDto>(faq);
            return faqDto;
        }

        public async Task<FaqDto> UpdateFaqAsync(int currentFaqId, UpdateFaqDto updateFaqDto)
        {
            Faq currentFaq = await _unitOfWork.Faqs.GetFaqByIdAsync(currentFaqId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            currentFaq = _mapper.Map(updateFaqDto, currentFaq);

            FaqCategory faqCategory = await _unitOfWork.FaqCategories.GetFaqCategoryByIdAsync(updateFaqDto.CategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            currentFaq.Category = faqCategory;

            if (currentFaq.IsActive)
            {
                if (currentFaq.ActivatedAt == DateTimeOffset.MinValue || currentFaq.ActivatedAt == null)
                {
                    currentFaq.ActivatedAt = DateTimeOffset.Now;
                }
            }
            else if (!currentFaq.IsActive)
            {
                currentFaq.ActivatedAt = DateTimeOffset.MinValue;
            }


            _unitOfWork.Faqs.UpdateFaq(currentFaq);
            await _unitOfWork.CommitAsync();

            FaqDto faqDto = _mapper.Map<FaqDto>(currentFaq);
            return faqDto;
        }
    }
}