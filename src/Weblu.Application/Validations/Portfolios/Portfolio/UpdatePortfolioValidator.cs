using FluentValidation;
using Weblu.Application.Dtos.Portfolios.PortfolioDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Validations.Portfolios.Portfolio
{
    public class UpdatePortfolioValidator : AbstractValidator<UpdatePortfolioDto>
    {
        public UpdatePortfolioValidator()
        {
            RuleFor(x => x.Title)
           .NotEmpty()
               .WithMessage(PortfolioErrorCodes.TitleRequired)
           .MaximumLength(100)
               .WithMessage(PortfolioErrorCodes.TitleTooLong);

            RuleFor(x => x.Description)
                .NotEmpty()
                    .WithMessage(PortfolioErrorCodes.DescriptionRequired)
                .MaximumLength(2000)
                    .WithMessage(PortfolioErrorCodes.DescriptionTooLong);

            RuleFor(x => x.ShortDescription)
                .NotEmpty()
                    .WithMessage(PortfolioErrorCodes.ShortDescriptionRequired)
                .MaximumLength(300)
                    .WithMessage(PortfolioErrorCodes.ShortDescriptionTooLong);

            RuleFor(x => x.GithubUrl)
                .Must(UrlValidator.BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.GithubUrl))
                .WithMessage(PortfolioErrorCodes.InvalidGithubUrl);

            RuleFor(x => x.LiveUrl)
                .Must(UrlValidator.BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.LiveUrl))
                .WithMessage(PortfolioErrorCodes.InvalidLiveUrl);

            RuleFor(x => x.PortfolioCategoryId)
                .GreaterThan(0)
                .WithMessage(PortfolioErrorCodes.InvalidCategoryId);
        }
    }
}