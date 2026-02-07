using FluentValidation;
using Weblu.Application.Dtos.Services.ServiceDtos;
using Weblu.Domain.Errors.Services;

namespace Weblu.Application.Validations.Services
{
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceDto>
    {
        public UpdateServiceValidator()
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

            RuleFor(s => s.BaseDurationInDays)
                .NotEmpty()
                .WithMessage(ServiceErrorCodes.ServiceBaseDurationRequired);

            RuleFor(s => s.BasePrice)
                .NotEmpty()
                .WithMessage(ServiceErrorCodes.ServiceBasePriceRequired);

            RuleFor(s => s.IsActive)
                .NotNull()
                .WithMessage(ServiceErrorCodes.ServiceIsActiveRequired);
        }
    }
}