using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Domain.Errors.Images;

namespace Weblu.Application.Validations.AboutUsInfo
{
    public class UpdateImageAboutUsValidator : AbstractValidator<UpdateImageAboutUsDto>
    {
        public UpdateImageAboutUsValidator()
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