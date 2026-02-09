using AutoMapper;
using Weblu.Application.DTOs.FAQs.FAQDTOs;
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
        public async Task<FAQDTO> CreateAsync(CreateFAQDTO createFAQDTO)
        {
            FAQ faq = _mapper.Map<FAQ>(createFAQDTO);

            FAQCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(createFAQDTO.CategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            faq.Category = faqCategory;

            _faqRepository.Add(faq);
            await _unitOfWork.CommitAsync();

            FAQDTO faqDTO = _mapper.Map<FAQDTO>(faq);
            return faqDTO;
        }
        public async Task DeleteAsync(int faqId)
        {
            FAQ faq = await _faqRepository.GetByIdAsync(faqId) ?? throw new NotFoundException(FAQErrorCodes.NotFound);
            if (faq.IsPublished) throw new ConflictException(FAQErrorCodes.IsPublish);
            faq.Delete();
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<FAQDTO>> GetAllAsync(FAQParameters faqParameters)
        {
            IReadOnlyList<FAQ> faqs = await _faqRepository.GetAllAsync(faqParameters);
            List<FAQDTO> faqDTOs = _mapper.Map<List<FAQDTO>>(faqs);
            return faqDTOs;
        }

        public async Task<FAQDTO> GetByIdAsync(int fAQId)
        {
            FAQ faq = await _faqRepository.GetByIdAsync(fAQId) ?? throw new NotFoundException(FAQErrorCodes.NotFound);
            FAQDTO faqDTO = _mapper.Map<FAQDTO>(faq);
            return faqDTO;
        }

        public async Task Publish(int fAQId)
        {
            FAQ fAQ = await _faqRepository.GetByIdAsync(fAQId) ?? throw new NotFoundException(FAQErrorCodes.NotFound);

            fAQ.Publish();
            await _unitOfWork.CommitAsync();
        }

        public async Task Unpublish(int fAQId)
        {
            FAQ fAQ = await _faqRepository.GetByIdAsync(fAQId) ?? throw new NotFoundException(FAQErrorCodes.NotFound);

            fAQ.Unpublish();
            await _unitOfWork.CommitAsync();
        }

        public async Task<FAQDTO> UpdateAsync(int currentFAQId, UpdateFAQDTO updateFAQDTO)
        {
            FAQ currentFAQ = await _faqRepository.GetByIdAsync(currentFAQId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            currentFAQ = _mapper.Map(updateFAQDTO, currentFAQ);

            FAQCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(updateFAQDTO.CategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            currentFAQ.Category = faqCategory;

            currentFAQ.MarkUpdated();
            _faqRepository.Update(currentFAQ);
            await _unitOfWork.CommitAsync();

            FAQDTO faqDTO = _mapper.Map<FAQDTO>(currentFAQ);
            return faqDTO;
        }
    }
}