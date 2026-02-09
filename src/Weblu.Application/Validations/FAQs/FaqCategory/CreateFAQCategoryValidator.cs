using FluentValidation;
using Weblu.Application.Dtos.FAQs.FAQCategoryDtos;
using Weblu.Domain.Errors.FAQs;

namespace Weblu.Application.Validations.FAQs.FAQCategory
{
    public class CreateFAQCategoryValidator : AbstractValidator<CreateFAQCategoryDto>
    {
        public CreateFAQCategoryValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty().WithMessage(FAQCategoryErrorCodes.NameRequired)
                .MaximumLength(100).WithMessage(FAQCategoryErrorCodes.NameMaximumLength);

            RuleFor(f => f.Description)
                .MaximumLength(500).WithMessage(FAQCategoryErrorCodes.DescriptionMaximumLength);
        }
    }
}