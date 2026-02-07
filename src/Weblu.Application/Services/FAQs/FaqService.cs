using AutoMapper;
using Weblu.Application.Dtos.FAQs.FaqDtos;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories.Common;
using Weblu.Application.Interfaces.Repositories.FAQs;
using Weblu.Application.Interfaces.Services.FAQs;
using Weblu.Application.Parameters.FAQs;
using Weblu.Domain.Entities.Faqs;
using Weblu.Domain.Errors.FAQs;

namespace Weblu.Application.Services.FAQs
{
    public class FaqService : IFaqService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFaqCategoryRepository _faqCategoryRepository;
        private readonly IFaqRepository _faqRepository;
        private readonly IMapper _mapper;
        public FaqService(IUnitOfWork unitOfWork, IMapper mapper, IFaqCategoryRepository faqCategoryRepository, IFaqRepository faqRepository)
        {
            _faqCategoryRepository = faqCategoryRepository;
            _faqRepository = faqRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FaqDto> AddFaqAsync(AddFaqDto addFaqDto)
        {
            Faq faq = _mapper.Map<Faq>(addFaqDto);

            FaqCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(addFaqDto.CategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            faq.Category = faqCategory;

            _faqRepository.Add(faq);
            await _unitOfWork.CommitAsync();

            FaqDto faqDto = _mapper.Map<FaqDto>(faq);
            return faqDto;
        }
        public async Task DeleteFaqAsync(int faqId)
        {
            Faq faq = await _faqRepository.GetByIdAsync(faqId) ?? throw new NotFoundException(FaqErrorCodes.NotFound);

            faq.Delete();
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FaqDto>> GetAllFaqsAsync(FaqParameters faqParameters)
        {
            IReadOnlyList<Faq> faqs = await _faqRepository.GetAllAsync(faqParameters);
            List<FaqDto> faqDtos = _mapper.Map<List<FaqDto>>(faqs);
            return faqDtos;
        }

        public async Task<FaqDto> GetFaqByIdAsync(int faqId)
        {
            Faq faq = await _faqRepository.GetByIdAsync(faqId) ?? throw new NotFoundException(FaqErrorCodes.NotFound);
            FaqDto faqDto = _mapper.Map<FaqDto>(faq);
            return faqDto;
        }

        public async Task<FaqDto> UpdateFaqAsync(int currentFaqId, UpdateFaqDto updateFaqDto)
        {
            Faq currentFaq = await _faqRepository.GetByIdAsync(currentFaqId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
            currentFaq = _mapper.Map(updateFaqDto, currentFaq);

            FaqCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(updateFaqDto.CategoryId) ?? throw new NotFoundException(FaqCategoryErrorCodes.NotFound);
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


            _faqRepository.Update(currentFaq);
            await _unitOfWork.CommitAsync();

            FaqDto faqDto = _mapper.Map<FaqDto>(currentFaq);
            return faqDto;
        }
    }
}