using FluentValidation;
using Weblu.Application.Dtos.Articles.ArticleCategoryDtos;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Validations.Articles.ArticleCategories
{
    public class AddArticleCategoryValidator : AbstractValidator<AddArticleCategoryDto>
    {
        public AddArticleCategoryValidator()
        {
            RuleFor(n => n.Name)
             .NotEmpty().WithMessage(ArticleCategoryErrorCodes.NameRequired)
             .MaximumLength(100).WithMessage(ArticleCategoryErrorCodes.NameMaxLength);

            RuleFor(n => n.Description)
            .MaximumLength(255).WithMessage(ArticleCategoryErrorCodes.DescriptionMaxLength);
        }
    }
}