using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.FeatureDtos;
using Weblu.Domain.Errors.Features;

namespace Weblu.Application.Validations.Features
{
    public class AddFeatureValidator : AbstractValidator<AddFeatureDto>
    {
        public AddFeatureValidator()
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