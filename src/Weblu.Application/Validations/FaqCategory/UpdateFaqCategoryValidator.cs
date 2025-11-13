using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.FaqCategoryDtos;
using Weblu.Application.Dtos.FaqDtos;
using Weblu.Domain.Errors.Faqs;

namespace Weblu.Application.Validations.FaqCategory
{
    public class UpdateFaqCategoryValidator : AbstractValidator<UpdateFaqCategoryDto>
    {
        public UpdateFaqCategoryValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage(FaqCategoryErrorCodes.NameRequired)
                .MaximumLength(100).WithMessage(FaqCategoryErrorCodes.NameMaximumLength);

            RuleFor(f => f.Description)
                .MaximumLength(500).WithMessage(FaqCategoryErrorCodes.DescriptionMaximumLength);
        }
    }
}