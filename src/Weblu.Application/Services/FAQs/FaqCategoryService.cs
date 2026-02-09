using AutoMapper;
using Weblu.Application.Dtos.FAQs.FAQCategoryDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.FAQs;
using Weblu.Application.Interfaces.Services.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.FAQs;
using Weblu.Domain.Errors.FAQs;

namespace Weblu.Application.Services.FAQs
{
    public class FAQCategoryService : IFAQCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFAQCategoryRepository _faqCategoryRepository;
        private readonly IMapper _mapper;
        public FAQCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IFAQCategoryRepository faqCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _faqCategoryRepository = faqCategoryRepository;
        }
        public async Task<FAQCategoryDto> CreateAsync(CreateFAQCategoryDto createFAQCategoryDto)
        {
            FAQCategory faqCategory = _mapper.Map<FAQCategory>(createFAQCategoryDto);

            _faqCategoryRepository.Add(faqCategory);
            await _unitOfWork.CommitAsync();

            FAQCategoryDto faqCategoryDto = _mapper.Map<FAQCategoryDto>(faqCategory);
            return faqCategoryDto;
        }
        public async Task DeleteAsync(int faqCategoryId)
        {
            FAQCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(faqCategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);

            faqCategory.Delete();
            await _unitOfWork.CommitAsync();

        }


        public async Task<List<FAQCategoryDto>> GetAllAsync(FAQCategoryParameters faqCategoryParameters)
        {
            IReadOnlyList<FAQCategory> faqCategories = await _faqCategoryRepository.GetAllAsync(faqCategoryParameters);
            List<FAQCategoryDto> faqCategoryDtos = _mapper.Map<List<FAQCategoryDto>>(faqCategories);
            return faqCategoryDtos;
        }

        public async Task<FAQCategoryDto> GetByIdAsync(int faqCategoryId)
        {
            FAQCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(faqCategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            FAQCategoryDto faqCategoryDto = _mapper.Map<FAQCategoryDto>(faqCategory);
            return faqCategoryDto;
        }

        public async Task<FAQCategoryDto> UpdateAsync(int currentFAQCategoryId, UpdateFAQCategoryDto updateFAQCategoryDto)
        {
            FAQCategory currentFAQCategory = await _faqCategoryRepository.GetByIdAsync(currentFAQCategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            currentFAQCategory = _mapper.Map(updateFAQCategoryDto, currentFAQCategory);

            currentFAQCategory.MarkUpdated();
            _faqCategoryRepository.Update(currentFAQCategory);
            await _unitOfWork.CommitAsync();

            FAQCategoryDto faqCategoryDto = _mapper.Map<FAQCategoryDto>(currentFAQCategory);
            return faqCategoryDto;
        }
    }
}