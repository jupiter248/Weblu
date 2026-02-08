using FluentValidation;
using Weblu.Application.Dtos.About.AboutUsDtos;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Validations.About.AboutUs
{
    public class ChangeAboutUsImageValidator : AbstractValidator<ChangeAboutUsImageDto>
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