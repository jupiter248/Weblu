using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.PortfolioCategory;
using Weblu.Domain.Errors.PortfolioCategory;

namespace Weblu.Application.Validations.PortfolioCategory
{
    public class UpdatePortfolioCategoryValidator : AbstractValidator<UpdatePortfolioCategoryDto>
    {
        public UpdatePortfolioCategoryValidator()
        {
            RuleFor(n => n.Name)
             .NotEmpty().WithMessage(PortfolioCategoryErrorCodes.PortfolioCategoryNameRequired)
             .MaximumLength(100).WithMessage(PortfolioCategoryErrorCodes.PortfolioCategoryNameMaxLength);

            RuleFor(n => n.Description)
            .NotEmpty().WithMessage(PortfolioCategoryErrorCodes.PortfolioCategoryDescriptionRequired)
            .MaximumLength(255).WithMessage(PortfolioCategoryErrorCodes.PortfolioCategoryDescriptionMaxLength);
        }
    }
}