using FluentValidation;
using Weblu.Application.DTOs.Common.FeatureDTOs;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Validations.Common.Features
{
    public class CreateFeatureValidator : AbstractValidator<CreateFeatureDTO>
    {
        public CreateFeatureValidator()
        {
            RuleFor(n => n.Name)
            .NotEmpty().WithMessage(FeatureErrorCodes.FeatureNameRequired)
            .MaximumLength(100).WithMessage(FeatureErrorCodes.FeatureNameMaxLength);

            RuleFor(n => n.Description)
            .NotEmpty().WithMessage(FeatureErrorCodes.FeatureDescriptionRequired)
            .MaximumLength(1000).WithMessage(FeatureErrorCodes.FeatureDescriptionMaxLength);
        }
    }
}