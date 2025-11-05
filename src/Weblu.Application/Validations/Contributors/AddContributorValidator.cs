using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.ContributorDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Errors.Contributors;

namespace Weblu.Application.Validations.Contributors
{
    public class AddContributorValidator : AbstractValidator<AddContributorDto>
    {
        public AddContributorValidator()
        {
            RuleFor(r => r.FirstName)
            .NotEmpty().WithMessage(ContributorErrorCodes.ContributorFirstNameRequired)
            .Length(2, 50).WithMessage(ContributorErrorCodes.ContributorFirstNameMaxLength);

            RuleFor(r => r.LastName)
            .NotEmpty().WithMessage(ContributorErrorCodes.ContributorLastNameRequired)
            .Length(2, 100).WithMessage(ContributorErrorCodes.ContributorLastNameMaxLength);

            RuleFor(r => r.Role)
            .NotEmpty().WithMessage(ContributorErrorCodes.ContributorRoleRequired)
            .MaximumLength(100).WithMessage(ContributorErrorCodes.ContributorRoleMaxLength);

            RuleFor(r => r.Bio)
            .MaximumLength(500).WithMessage(ContributorErrorCodes.ContributorBioMaxLength);

            RuleFor(r => r.Email)
            .EmailAddress().WithMessage(ContributorErrorCodes.ContributorEmailInvalid);

            RuleFor(g => g.GithubUrl)
            .Must(UrlValidator.BeAValidUrl)
            .When(x => !string.IsNullOrWhiteSpace(x.GithubUrl))
            .WithMessage(ContributorErrorCodes.ContributorGithubUrlInvalid);

            RuleFor(g => g.LinkedInUrl)
            .Must(UrlValidator.BeAValidUrl)
            .When(x => !string.IsNullOrWhiteSpace(x.GithubUrl))
            .WithMessage(ContributorErrorCodes.ContributorLinkedInUrlInvalid);
        }
    }
}