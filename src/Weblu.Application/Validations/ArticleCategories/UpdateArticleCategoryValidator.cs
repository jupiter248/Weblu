using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.ArticleCategoryDtos;
using Weblu.Domain.Errors.Articles;

namespace Weblu.Application.Validations.ArticleCategories
{
    public class UpdateArticleCategoryValidator : AbstractValidator<UpdateArticleCategoryDto>
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