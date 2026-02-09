using FluentValidation;
using Weblu.Application.DTOs.About.AboutUsDTOs;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Validations.About.AboutUs
{
    public class ChangeAboutUsImageValidator : AbstractValidator<ChangeAboutUsImageDTO>
    {
        public ChangeAboutUsImageValidator()
        {
            RuleFor(x => x.Image)
                .NotNull().WithMessage(ImageErrorCodes.ImageFileRequired)
                .Must(f => f.Length <= 5 * 1024 * 1024).WithMessage(ImageErrorCodes.ImageFileSize);

            RuleFor(x => x.AltText)
                .NotEmpty().WithMessage(ImageErrorCodes.ImageAltTextRequired)
                .MaximumLength(255).WithMessage(ImageErrorCodes.ImageAltTextMaxLength);
        }
    }
}