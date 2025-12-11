using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ArticleCategoryDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Application.Parameters;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Services
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
        public async Task<ArticleCategoryDto> AddArticleCategoryAsync(AddArticleCategoryDto addArticleCategoryDto)
        {
            ArticleCategory articleCategory = _mapper.Map<ArticleCategory>(addArticleCategoryDto);

            _articleCategoryRepository.Add(articleCategory);
            await _unitOfWork.CommitAsync();

            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }

        public async Task DeleteArticleCategoryAsync(int categoryId)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            _articleCategoryRepository.Delete(articleCategory);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ArticleCategoryDto>> GetAllArticleCategoriesAsync(ArticleCategoryParameters articleCategoryParameters)
        {
            IReadOnlyList<ArticleCategory> articleCategories = await _articleCategoryRepository.GetAllAsync(articleCategoryParameters);
            List<ArticleCategoryDto> articleCategoryDtos = _mapper.Map<List<ArticleCategoryDto>>(articleCategories);
            return articleCategoryDtos;
        }

        public async Task<ArticleCategoryDto> GetArticleCategoryByIdAsync(int categoryId)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }

        public async Task<ArticleCategoryDto> UpdateArticleCategoryAsync(int categoryId, UpdateArticleCategoryDto updateArticleCategoryDto)
        {
            ArticleCategory articleCategory = await _articleCategoryRepository.GetByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            articleCategory = _mapper.Map(updateArticleCategoryDto, articleCategory);

            _articleCategoryRepository.Update(articleCategory);
            await _unitOfWork.CommitAsync();

            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }
    }
}