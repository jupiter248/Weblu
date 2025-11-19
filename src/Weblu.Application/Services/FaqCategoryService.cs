using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.FaqCategoryDtos;
using Weblu.Application.Dtos.FaqDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities.Faqs;
using Weblu.Domain.Errors;
using Weblu.Domain.Errors.Faqs;

namespace Weblu.Application.Services
{
    public class FaqCategoryService : IFaqCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FaqCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FaqCategoryDto> AddFaqCategoryAsync(AddFaqCategoryDto addFaqCategoryDto)
        {
            FaqCategory faqCategory = _mapper.Map<FaqCategory>(addFaqCategoryDto);

            await _unitOfWork.FaqCategories.AddFaqCategoryAsync(faqCategory);
            await _unitOfWork.CommitAsync();

            FaqCategoryDto faqCategoryDto = _mapper.Map<FaqCategoryDto>(faqCategory);
            return faqCategoryDto;
        }

        public async Task DeleteFaqCategoryAsync(int faqCategoryId)
        {
            FaqCategory faqCategory = await _unitOfWork.FaqCategories.GetFaqCategoryByIdAsync(faqCategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);

            _unitOfWork.FaqCategories.DeleteFaqCategory(faqCategory);
            await _unitOfWork.CommitAsync();

        }

        public async Task<List<FaqCategoryDto>> GetAllFaqCategoriesAsync()
        {
            IReadOnlyList<FaqCategory> faqCategories = await _unitOfWork.FaqCategories.GetAllFaqCategoriesAsync();
            List<FaqCategoryDto> faqCategoryDtos = _mapper.Map<List<FaqCategoryDto>>(faqCategories);
            return faqCategoryDtos;
        }

        public async Task<FaqCategoryDto> GetFaqCategoryByIdAsync(int faqCategoryId)
        {
            FaqCategory faqCategory = await _unitOfWork.FaqCategories.GetFaqCategoryByIdAsync(faqCategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            FaqCategoryDto faqCategoryDto = _mapper.Map<FaqCategoryDto>(faqCategory);
            return faqCategoryDto;
        }

        public async Task<FaqCategoryDto> UpdateFaqCategoryAsync(int currentFaqCategoryId, UpdateFaqCategoryDto updateFaqCategoryDto)
        {
            FaqCategory currentFaqCategory = await _unitOfWork.FaqCategories.GetFaqCategoryByIdAsync(currentFaqCategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            currentFaqCategory = _mapper.Map(updateFaqCategoryDto, currentFaqCategory);

            _unitOfWork.FaqCategories.UpdateFaqCategory(currentFaqCategory);
            await _unitOfWork.CommitAsync();

            FaqCategoryDto faqCategoryDto = _mapper.Map<FaqCategoryDto>(currentFaqCategory);
            return faqCategoryDto;
        }
    }
}