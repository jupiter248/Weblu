using FluentValidation;
using Weblu.Application.DTOs.Common.TagDTOs;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Validations.Common.Tags
{
    public class CreateTagValidator : AbstractValidator<CreateTagDTO>
    {
        public CreateTagValidator()
        {
            RuleFor(n => n.Name)
            .NotEmpty().WithMessage(TagErrorCodes.NameRequired)
            .MaximumLength(100).WithMessage(TagErrorCodes.NameMaxLength);

            RuleFor(n => n.Description)
            .MaximumLength(1000).WithMessage(TagErrorCodes.DescriptionMaxLength);
        }
    }
}