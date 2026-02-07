using FluentValidation;
using Weblu.Application.Dtos.Auth.AuthDtos;
using Weblu.Domain.Errors.Auth;

namespace Weblu.Application.Validations.Auth
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(AuthErrorCodes.UsernameRequired)
                .MaximumLength(40).WithMessage(AuthErrorCodes.UsernameMaxLength);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(AuthErrorCodes.PasswordRequired)
                .Length(8, 100).WithMessage(AuthErrorCodes.PasswordLength);
        }
    }
}