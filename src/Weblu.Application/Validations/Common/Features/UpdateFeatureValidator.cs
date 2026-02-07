using FluentValidation;
using Weblu.Application.Dtos.Common.FeatureDtos;
using Weblu.Domain.Errors.Common;

namespace Weblu.Application.Validations.Common.Features
{
    public class UpdateFeatureValidator : AbstractValidator<UpdateFeatureDto>
    {
        public UpdateFeatureValidator()
        {
            RuleFor(n => n.Name)
            .NotEmpty().WithMessage(FeatureErrorCodes.FeatureNameRequired)
            .MaximumLength(50).WithMessage(FeatureErrorCodes.FeatureNameMaxLength);

            RuleFor(n => n.Description)
            .NotEmpty().WithMessage(FeatureErrorCodes.FeatureDescriptionRequired)
            .MaximumLength(1000).WithMessage(FeatureErrorCodes.FeatureDescriptionMaxLength);
        }
    }
}