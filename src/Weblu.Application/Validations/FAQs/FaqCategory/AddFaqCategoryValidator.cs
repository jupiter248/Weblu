using FluentValidation;
using Weblu.Application.Dtos.FAQs.FaqCategoryDtos;
using Weblu.Domain.Errors.FAQs;

namespace Weblu.Application.Validations.FAQs.FaqCategory
{
    public class AddFaqCategoryValidator : AbstractValidator<AddFaqCategoryDto>
    {
        public AddFaqCategoryValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage(FaqCategoryErrorCodes.NameRequired)
                .MaximumLength(100).WithMessage(FaqCategoryErrorCodes.NameMaximumLength);

            RuleFor(f => f.Description)
                .MaximumLength(500).WithMessage(FaqCategoryErrorCodes.DescriptionMaximumLength);
        }
    }
}