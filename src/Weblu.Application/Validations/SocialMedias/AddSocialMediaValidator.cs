using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.SocialMediaDtos;
using Weblu.Application.Helpers;
using Weblu.Domain.Errors.SocialMedia;

namespace Weblu.Application.Validations.SocialMedias
{
    public class AddSocialMediaValidator : AbstractValidator<AddSocialMediaDto>
    {
        public AddSocialMediaValidator()
        {
            RuleFor(n => n.Name)
                .NotEmpty().WithMessage(SocialMediaErrorCodes.NameRequired)
                .MaximumLength(50).WithMessage(SocialMediaErrorCodes.NameMaxLength);

            RuleFor(n => n.Link)
                .Must(UrlValidator.BeAValidUrl).When(l => !string.IsNullOrEmpty(l.Link)).WithMessage(SocialMediaErrorCodes.InvalidLink);
        }
    }
}