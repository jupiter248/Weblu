using FluentValidation;
using Weblu.Application.DTOs.About.SocialMediaDTOs;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Validations.About.SocialMedias
{
    public class ChangeSocialMediaIconValidator : AbstractValidator<ChangeSocialMediaIconDTO>
    {
        public ChangeSocialMediaIconValidator()
        {
            RuleFor(x => x.Image)
                .NotNull().WithMessage(ImageErrorCodes.IconFileRequired)
                .Must(f => f.Length <= 5 * 1024 * 1024).WithMessage(ImageErrorCodes.IconFileSize);

            RuleFor(x => x.AltText)
                .NotEmpty().WithMessage(ImageErrorCodes.IconAltTextRequired)
                .MaximumLength(255).WithMessage(ImageErrorCodes.IconAltTextMaxLength);
        }
    }
}