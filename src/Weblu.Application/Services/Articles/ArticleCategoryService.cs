using AutoMapper;
using Weblu.Application.DTOs.Articles.ArticleCategoryDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Repositories.Articles;
using Weblu.Application.Interfaces.Services.Articles;
using Weblu.Application.Parameters.Articles;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Services.Articles
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IArticleCategoryRepository articleCategoryRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _articleCategoryRepository = articleCategoryRepository;
        }
        public async Task<ArticleCategoryDTO> CreateAsync(CreateArticleCategoryDTO createArticleCategoryDTO)
        {
            ArticleCategory articleCategory = _mapper.Map<ArticleCategory>(createArticleCategoryDTO);

            _articleCategoryRepository.Add(articleCategory);
            await _unitOfWork.CommitAsync();

            ArticleCategoryDTO articleCategoryDTO = _mapper.Map<ArticleCategoryDTO>(articleCategory);
            return articleCategoryDTO;
        }


        public async Task<List<ArticleCategoryDTO>> GetAllAsync(ArticleCategoryParameters articleCategoryParameters)
        {
            IReadOnlyList<ArticleCategory> articleCategories = await _articleCategoryRepository.GetAllAsync(articleCategoryParameters);
            List<ArticleCategoryDTO> articleCategoryDTOs = _mapper.Map<List<ArticleCategoryDTO>>(articleCategories);
            return articleCategoryDTOs;
        }

        public async Task<ArticleCategoryDTO> GetByIdAsync(int categoryId)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            ArticleCategoryDTO articleCategoryDTO = _mapper.Map<ArticleCategoryDTO>(articleCategory);
            return articleCategoryDTO;
        }
        public async Task<ArticleCategoryDTO> UpdateAsync(int categoryId, UpdateArticleCategoryDTO updateArticleCategoryDTO)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            articleCategory = _mapper.Map(updateArticleCategoryDTO, articleCategory);

            articleCategory.MarkUpdated();
            _articleCategoryRepository.Update(articleCategory);
            await _unitOfWork.CommitAsync();

            ArticleCategoryDTO articleCategoryDTO = _mapper.Map<ArticleCategoryDTO>(articleCategory);
            return articleCategoryDTO;
        }

        public async Task DeleteAsync(int categoryId)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            articleCategory.Delete();
            await _unitOfWork.CommitAsync();
        }
    }
}