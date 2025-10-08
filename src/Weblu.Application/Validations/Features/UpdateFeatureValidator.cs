using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Domain.Errors.Features;

namespace Weblu.Application.Validations.Features
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
            .MaximumLength(150).WithMessage(FeatureErrorCodes.FeatureDescriptionMaxLength);
        }
    }
}