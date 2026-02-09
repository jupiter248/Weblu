using AutoMapper;
using Weblu.Application.DTOs.FAQs.FAQCategoryDTOs;
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
        public async Task<FAQCategoryDTO> CreateAsync(CreateFAQCategoryDTO createFAQCategoryDTO)
        {
            FAQCategory faqCategory = _mapper.Map<FAQCategory>(createFAQCategoryDTO);

            _faqCategoryRepository.Add(faqCategory);
            await _unitOfWork.CommitAsync();

            FAQCategoryDTO faqCategoryDTO = _mapper.Map<FAQCategoryDTO>(faqCategory);
            return faqCategoryDTO;
        }
        public async Task DeleteAsync(int faqCategoryId)
        {
            FAQCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(faqCategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);

            faqCategory.Delete();
            await _unitOfWork.CommitAsync();

        }


        public async Task<List<FAQCategoryDTO>> GetAllAsync(FAQCategoryParameters faqCategoryParameters)
        {
            IReadOnlyList<FAQCategory> faqCategories = await _faqCategoryRepository.GetAllAsync(faqCategoryParameters);
            List<FAQCategoryDTO> faqCategoryDTOs = _mapper.Map<List<FAQCategoryDTO>>(faqCategories);
            return faqCategoryDTOs;
        }

        public async Task<FAQCategoryDTO> GetByIdAsync(int faqCategoryId)
        {
            FAQCategory faqCategory = await _faqCategoryRepository.GetByIdAsync(faqCategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            FAQCategoryDTO faqCategoryDTO = _mapper.Map<FAQCategoryDTO>(faqCategory);
            return faqCategoryDTO;
        }

        public async Task<FAQCategoryDTO> UpdateAsync(int currentFAQCategoryId, UpdateFAQCategoryDTO updateFAQCategoryDTO)
        {
            FAQCategory currentFAQCategory = await _faqCategoryRepository.GetByIdAsync(currentFAQCategoryId) ?? throw new NotFoundException(FAQCategoryErrorCodes.NotFound);
            currentFAQCategory = _mapper.Map(updateFAQCategoryDTO, currentFAQCategory);

            currentFAQCategory.MarkUpdated();
            _faqCategoryRepository.Update(currentFAQCategory);
            await _unitOfWork.CommitAsync();

            FAQCategoryDTO faqCategoryDTO = _mapper.Map<FAQCategoryDTO>(currentFAQCategory);
            return faqCategoryDTO;
        }
    }
}