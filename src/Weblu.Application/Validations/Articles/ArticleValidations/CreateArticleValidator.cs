using FluentValidation;
using Weblu.Application.Dtos.Articles.ArticleDtos;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Validations.Articles.ArticleValidations
{
    public class CreateArticleValidator : AbstractValidator<CreateArticleDto>
    {
        public CreateArticleValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage(ArticleErrorCodes.TitleRequired)
                .MaximumLength(150).WithMessage(ArticleErrorCodes.TitleMaxLength);

            RuleFor(x => x.BelowTitle)
                .MaximumLength(150).WithMessage(ArticleErrorCodes.BelowTextMaxLength)
                .When(x => !string.IsNullOrWhiteSpace(x.BelowTitle));

            RuleFor(x => x.Text)
                .NotEmpty().WithMessage(ArticleErrorCodes.TextRequired);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ArticleErrorCodes.DescriptionRequired);

            RuleFor(x => x.ShortDescription)
                .NotEmpty().WithMessage(ArticleErrorCodes.ShortDescriptionRequired)
                .MaximumLength(250).WithMessage(ArticleErrorCodes.ShortDescriptionMaxLength);
        }
    }
}