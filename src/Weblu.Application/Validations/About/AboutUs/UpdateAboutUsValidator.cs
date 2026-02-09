using FluentValidation;
using Weblu.Application.Dtos.About.AboutUsDtos;
using Weblu.Domain.Errors.About;
using Weblu.Domain.Errors.Auth;

namespace Weblu.Application.Validations.About.AboutUs
{
    public class UpdateAboutUsValidator : AbstractValidator<UpdateAboutUsDto>
    {
        public UpdateAboutUsValidator()
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