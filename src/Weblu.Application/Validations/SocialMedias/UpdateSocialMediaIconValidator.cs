using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.SocialMediaDtos;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Validations.SocialMedias
{
    public class UpdateSocialMediaIconValidator : AbstractValidator<UpdateSocialMediaIconDto>
    {
        public UpdateSocialMediaIconValidator()
        {
            RuleFor(x => x.Icon)
                .NotNull().WithMessage(ImageErrorCodes.IconFileRequired)
                .Must(f => f.Length <= 5 * 1024 * 1024).WithMessage(ImageErrorCodes.IconFileSize);

            RuleFor(x => x.AltText)
                .NotEmpty().WithMessage(ImageErrorCodes.IconAltTextRequired)
                .MaximumLength(255).WithMessage(ImageErrorCodes.IconAltTextMaxLength);
        }
    }
}