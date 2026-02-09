using FluentValidation;
using Weblu.Application.DTOs.Common.ContributorDTOs;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Validations.Common.Contributors
{
    public class ChangeContributorProfileImageValidator : AbstractValidator<ChangeContributorProfileImageDTO>
    {
        public ChangeContributorProfileImageValidator()
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