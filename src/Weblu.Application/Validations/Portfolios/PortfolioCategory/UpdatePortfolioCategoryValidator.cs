using FluentValidation;
using Weblu.Application.Dtos.Portfolios.PortfolioCategory;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Validations.Portfolios.PortfolioCategory
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