using FluentValidation;
using Weblu.Application.DTOs.FAQs.FAQCategoryDTOs;
using Weblu.Domain.Errors.FAQs;

namespace Weblu.Application.Validations.FAQs.FAQCategory
{
    public class CreateFAQCategoryValidator : AbstractValidator<CreateFAQCategoryDTO>
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