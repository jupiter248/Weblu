using AutoMapper;
using Weblu.Application.Dtos.FaqCategoryDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Faqs;
using Weblu.Domain.Errors.Faqs;

namespace Weblu.Application.Services
{
    public class FaqCategoryService : IFaqCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFaqCategoryRepository _faqCategoryRepository;
        private readonly IMapper _mapper;
        public FaqCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IFaqCategoryRepository faqCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _faqCategoryRepository = faqCategoryRepository;
        }
        public async Task<FaqCategoryDto> AddFaqCategoryAsync(AddFaqCategoryDto addFaqCategoryDto)
        {
            FaqCategory faqCategory = _mapper.Map<FaqCategory>(addFaqCategoryDto);

            _faqCategoryRepository.Add(faqCategory);
            await _unitOfWork.CommitAsync();

            FaqCategoryDto faqCategoryDto = _mapper.Map<FaqCategoryDto>(faqCategory);
            return faqCategoryDto;
        }

        public async Task DeleteFaqCategoryAsync(int faqCategoryId)
        {
            FaqCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(faqCategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);

            _faqCategoryRepository.Delete(faqCategory);
            await _unitOfWork.CommitAsync();

        }

        public async Task<List<FaqCategoryDto>> GetAllFaqCategoriesAsync(FaqCategoryParameters faqCategoryParameters)
        {
            IReadOnlyList<FaqCategory> faqCategories = await _faqCategoryRepository.GetAllAsync(faqCategoryParameters);
            List<FaqCategoryDto> faqCategoryDtos = _mapper.Map<List<FaqCategoryDto>>(faqCategories);
            return faqCategoryDtos;
        }

        public async Task<FaqCategoryDto> GetFaqCategoryByIdAsync(int faqCategoryId)
        {
            FaqCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(faqCategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            FaqCategoryDto faqCategoryDto = _mapper.Map<FaqCategoryDto>(faqCategory);
            return faqCategoryDto;
        }

        public async Task<FaqCategoryDto> UpdateFaqCategoryAsync(int currentFaqCategoryId, UpdateFaqCategoryDto updateFaqCategoryDto)
        {
            FaqCategory currentFaqCategory = await _faqCategoryRepository.GetByIdAsync(currentFaqCategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            currentFaqCategory = _mapper.Map(updateFaqCategoryDto, currentFaqCategory);

            _faqCategoryRepository.Update(currentFaqCategory);
            await _unitOfWork.CommitAsync();

            FaqCategoryDto faqCategoryDto = _mapper.Map<FaqCategoryDto>(currentFaqCategory);
            return faqCategoryDto;
        }
    }
}