using FluentValidation;
using Weblu.Application.Dtos.Common.TagDtos;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Validations.Common.Tags
{
    public class CreateTagValidator : AbstractValidator<CreateTagDto>
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