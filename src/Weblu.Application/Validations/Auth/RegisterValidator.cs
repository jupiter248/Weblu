using FluentValidation;
using Weblu.Application.Dtos.Auth.AuthDtos;
using Weblu.Domain.Errors.Auth;

namespace Weblu.Application.Validations.Auth
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
             RuleFor(x => x.Username)
                .NotEmpty().WithMessage(AuthErrorCodes.UsernameRequired)
                .MaximumLength(40).WithMessage(AuthErrorCodes.UsernameMaxLength);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage(AuthErrorCodes.PhoneRequired)
                .Matches(@"^\+?\d{10,15}$").WithMessage(AuthErrorCodes.InvalidPhone);

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(AuthErrorCodes.UserFirstNameRequired)
                .Length(2, 50).WithMessage(AuthErrorCodes.UserFirstNameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(AuthErrorCodes.UserLastNameRequired)
                .Length(2, 100).WithMessage(AuthErrorCodes.UserLastNameMaxLength);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(AuthErrorCodes.PasswordRequired)
                .Length(8, 100).WithMessage(AuthErrorCodes.PasswordLength);
        }
    }
}