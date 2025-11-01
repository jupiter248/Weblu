using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.PortfolioDtos;
using Weblu.Domain.Errors.Portfolios;

namespace Weblu.Application.Validations.Portfolios
{
    public class AddPortfolioValidator : AbstractValidator<AddPortfolioDto>
    {
        public AddPortfolioValidator()
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
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.GithubUrl))
                .WithMessage(PortfolioErrorCodes.InvalidGithubUrl);

            RuleFor(x => x.LiveUrl)
                .Must(BeAValidUrl)
                .When(x => !string.IsNullOrWhiteSpace(x.LiveUrl))
                .WithMessage(PortfolioErrorCodes.InvalidLiveUrl);

            RuleFor(x => x.PortfolioCategoryId)
                .GreaterThan(0)
                .WithMessage(PortfolioErrorCodes.InvalidCategoryId);
        }
        private bool BeAValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uri)
                && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
        }
    }
}