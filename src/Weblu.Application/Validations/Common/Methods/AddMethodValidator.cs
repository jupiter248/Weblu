using FluentValidation;
using Weblu.Application.Dtos.Common.MethodDtos;
using Weblu.Domain.Errors.Methods;

namespace Weblu.Application.Validations.Common.Methods
{
    public class AddMethodValidator : AbstractValidator<AddMethodDto>
    {
        public AddMethodValidator()
        {
            RuleFor(n => n.Name)
             .NotEmpty().WithMessage(MethodErrorCodes.MethodNameRequired)
             .MaximumLength(50).WithMessage(MethodErrorCodes.MethodNameMaxLength);

            RuleFor(n => n.Description)
            .NotEmpty().WithMessage(MethodErrorCodes.MethodDescriptionRequired)
            .MaximumLength(1500).WithMessage(MethodErrorCodes.MethodDescriptionMaxLength);
        }
    }
}