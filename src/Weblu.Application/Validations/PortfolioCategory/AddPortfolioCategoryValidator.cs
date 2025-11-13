using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.PortfolioCategory;
using Weblu.Domain.Errors.PortfolioCategory;

namespace Weblu.Application.Validations.PortfolioCategory
{
    public class AddPortfolioCategoryValidator : AbstractValidator<AddPortfolioCategoryDto>
    {
        public AddPortfolioCategoryValidator()
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