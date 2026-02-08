using AutoMapper;
using Weblu.Application.Dtos.FAQs.FAQDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.FAQs;
using Weblu.Application.Interfaces.Services.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.FAQs;
using Weblu.Domain.Errors.FAQs;

namespace Weblu.Application.Services.FAQs
{
    public class FAQService : IFAQService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFAQCategoryRepository _faqCategoryRepository;
        private readonly IFAQRepository _faqRepository;
        private readonly IMapper _mapper;
        public FAQService(IUnitOfWork unitOfWork, IMapper mapper, IFAQCategoryRepository faqCategoryRepository, IFAQRepository faqRepository)
        {
            _faqCategoryRepository = faqCategoryRepository;
            _faqRepository = faqRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FAQDto> CreateAsync(CreateFAQDto createFAQDto)
        {
            FAQ faq = _mapper.Map<FAQ>(createFAQDto);

            FAQCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(createFAQDto.CategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            faq.Category = faqCategory;

            _faqRepository.Add(faq);
            await _unitOfWork.CommitAsync();

            FAQDto faqDto = _mapper.Map<FAQDto>(faq);
            return faqDto;
        }
        public async Task DeleteAsync(int faqId)
        {
            FAQ faq = await _faqRepository.GetByIdAsync(faqId) ?? throw new NotFoundException(FAQErrorCodes.NotFound);

            faq.Delete();
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FAQDto>> GetAllAsync(FAQParameters faqParameters)
        {
            IReadOnlyList<FAQ> faqs = await _faqRepository.GetAllAsync(faqParameters);
            List<FAQDto> faqDtos = _mapper.Map<List<FAQDto>>(faqs);
            return faqDtos;
        }

        public async Task<FAQDto> GetByIdAsync(int faqId)
        {
            FAQ faq = await _faqRepository.GetByIdAsync(faqId) ?? throw new NotFoundException(FAQErrorCodes.NotFound);
            FAQDto faqDto = _mapper.Map<FAQDto>(faq);
            return faqDto;
        }

        public async Task<FAQDto> UpdateAsync(int currentFAQId, UpdateFAQDto updateFAQDto)
        {
            FAQ currentFAQ = await _faqRepository.GetByIdAsync(currentFAQId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            currentFAQ = _mapper.Map(updateFAQDto, currentFAQ);

            FAQCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(updateFAQDto.CategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            currentFAQ.Category = faqCategory;

            if (currentFAQ.IsActive)
            {
                if (currentFAQ.ActivatedAt == DateTimeOffset.MinValue || currentFAQ.ActivatedAt == null)
                {
                    currentFAQ.ActivatedAt = DateTimeOffset.Now;
                }
            }
            else if (!currentFAQ.IsActive)
            {
                currentFAQ.ActivatedAt = DateTimeOffset.MinValue;
            }


            _faqRepository.Update(currentFAQ);
            await _unitOfWork.CommitAsync();

            FAQDto faqDto = _mapper.Map<FAQDto>(currentFAQ);
            return faqDto;
        }
    }
}