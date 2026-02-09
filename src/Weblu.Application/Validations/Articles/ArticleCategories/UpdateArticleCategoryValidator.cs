using FluentValidation;
using Weblu.Application.DTOs.Articles.ArticleCategoryDTOs;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Validations.Articles.ArticleCategories
{
    public class UpdateArticleCategoryValidator : AbstractValidator<UpdateArticleCategoryDTO>
    {
        public UpdateArticleCategoryValidator()
        {
            RuleFor(n => n.Name)
             .NotEmpty().WithMessage(ArticleCategoryErrorCodes.NameRequired)
             .MaximumLength(100).WithMessage(ArticleCategoryErrorCodes.NameMaxLength);

            RuleFor(n => n.Description)
            .MaximumLength(255).WithMessage(ArticleCategoryErrorCodes.DescriptionMaxLength);
        }
    }
}