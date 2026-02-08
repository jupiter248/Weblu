using AutoMapper;
using Weblu.Application.Dtos.Articles.ArticleCategoryDtos;
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
        public async Task<ArticleCategoryDto> CreateAsync(CreateArticleCategoryDto createArticleCategoryDto)
        {
            ArticleCategory articleCategory = _mapper.Map<ArticleCategory>(createArticleCategoryDto);

            _articleCategoryRepository.Add(articleCategory);
            await _unitOfWork.CommitAsync();

            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }


        public async Task<List<ArticleCategoryDto>> GetAllAsync(ArticleCategoryParameters articleCategoryParameters)
        {
            IReadOnlyList<ArticleCategory> articleCategories = await _articleCategoryRepository.GetAllAsync(articleCategoryParameters);
            List<ArticleCategoryDto> articleCategoryDtos = _mapper.Map<List<ArticleCategoryDto>>(articleCategories);
            return articleCategoryDtos;
        }

        public async Task<ArticleCategoryDto> GetByIdAsync(int categoryId)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }
        public async Task<ArticleCategoryDto> UpdateAsync(int categoryId, UpdateArticleCategoryDto updateArticleCategoryDto)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            articleCategory = _mapper.Map(updateArticleCategoryDto, articleCategory);

            _articleCategoryRepository.Update(articleCategory);
            await _unitOfWork.CommitAsync();

            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }

        public async Task DeleteAsync(int categoryId)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            articleCategory.Delete();
            await _unitOfWork.CommitAsync();
        }
    }
}