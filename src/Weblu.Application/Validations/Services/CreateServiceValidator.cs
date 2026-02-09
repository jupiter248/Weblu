using FluentValidation;
using Weblu.Application.DTOs.Services.ServiceDTOs;
using Weblu.Domain.Errors.Services;

namespace Weblu.Application.Validations.Services
{
    public class CreateServiceValidator : AbstractValidator<CreateServiceDTO>
    {
        public CreateServiceValidator()
        {
            RuleFor(s => s.Title)
                .NotEmpty()
                .WithMessage(ServiceErrorCodes.ServiceTitleRequired)
                .MaximumLength(200)
                .WithMessage(ServiceErrorCodes.ServiceTitleMaxLength);

            RuleFor(s => s.Description)
                .NotEmpty()
                .WithMessage(ServiceErrorCodes.ServiceDescriptionRequired)
                .MaximumLength(1000)
                .WithMessage(ServiceErrorCodes.ServiceDescriptionMaxLength);

            RuleFor(s => s.ShortDescription)
                .NotEmpty()
                .WithMessage(ServiceErrorCodes.ServiceShortDescriptionRequired)
                .MaximumLength(350)
                .WithMessage(ServiceErrorCodes.ServiceShortDescriptionMaxLength);
        }
    }
}