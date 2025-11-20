using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.AboutUsDtos;
using Weblu.Domain.Errors.AboutUs;
using Weblu.Domain.Errors.Auth;
using Weblu.Domain.Errors.Commons;

namespace Weblu.Application.Validations.AboutUsInfo
{
    public class AddAboutUsValidator : AbstractValidator<AddAboutUsDto>
    {
        public AddAboutUsValidator()
        {
            RuleFor(s => s.Title)
                .NotEmpty()
                .WithMessage(AboutUsErrorCodes.TitleRequired)
                .MaximumLength(200)
                .WithMessage(AboutUsErrorCodes.TitleMaxLength);

            RuleFor(s => s.Description)
                .NotEmpty()
                .WithMessage(AboutUsErrorCodes.DescriptionRequired)
                .MaximumLength(1000)
                .WithMessage(AboutUsErrorCodes.DescriptionMaxLength);

            RuleFor(s => s.Vision)
                .NotEmpty()
                .WithMessage(AboutUsErrorCodes.VisionRequired);

            RuleFor(s => s.SubTitle)
                .NotEmpty()
                .WithMessage(AboutUsErrorCodes.SubTitleRequired);

            RuleFor(x => x.Phone)
                .Matches(@"^\+?\d{10,15}$").WithMessage(AuthErrorCodes.InvalidPhone);

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(AuthErrorCodes.InvalidEmail);
        }
    }
}