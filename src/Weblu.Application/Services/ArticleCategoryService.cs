using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Weblu.Application.Dtos.ArticleCategoryDtos;
using Weblu.Application.Exceptions;
using Weblu.Application.Interfaces.Repositories;
using Weblu.Application.Interfaces.Services;
using Weblu.Domain.Entities.Articles;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Services
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ArticleCategoryDto> AddArticleCategoryAsync(AddArticleCategoryDto addArticleCategoryDto)
        {
            ArticleCategory articleCategory = _mapper.Map<ArticleCategory>(addArticleCategoryDto);

            await _unitOfWork.ArticleCategories.AddArticleCategoryAsync(articleCategory);
            await _unitOfWork.CommitAsync();

            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }

        public async Task DeleteArticleCategoryAsync(int categoryId)
        {
            ArticleCategory articleCategory = await _unitOfWork.ArticleCategories.GetArticleCategoryByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            _unitOfWork.ArticleCategories.DeleteArticleCategory(articleCategory);
            await _unitOfWork.CommitAsync();
        }

        public async Task<List<ArticleCategoryDto>> GetAllArticleCategoriesAsync()
        {
            IReadOnlyList<ArticleCategory> articleCategories = await _unitOfWork.ArticleCategories.GetAllArticleCategoriesAsync();
            List<ArticleCategoryDto> articleCategoryDtos = _mapper.Map<List<ArticleCategoryDto>>(articleCategories);
            return articleCategoryDtos;
        }

        public async Task<ArticleCategoryDto> GetArticleCategoryByIdAsync(int categoryId)
        {
            ArticleCategory articleCategory = await _unitOfWork.ArticleCategories.GetArticleCategoryByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }

        public async Task<ArticleCategoryDto> UpdateArticleCategoryAsync(int categoryId, UpdateArticleCategoryDto updateArticleCategoryDto)
        {
            ArticleCategory articleCategory = await _unitOfWork.ArticleCategories.GetArticleCategoryByIdAsync(categoryId) ?? throw new NotFoundException(ArticleCategoryErrorCodes.NotFound);
            articleCategory = _mapper.Map(updateArticleCategoryDto, articleCategory);

            _unitOfWork.ArticleCategories.UpdateArticleCategory(articleCategory);
            await _unitOfWork.CommitAsync();

            ArticleCategoryDto articleCategoryDto = _mapper.Map<ArticleCategoryDto>(articleCategory);
            return articleCategoryDto;
        }
    }
}