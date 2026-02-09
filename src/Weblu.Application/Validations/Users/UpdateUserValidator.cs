using FluentValidation;
using Weblu.Application.Dtos.Users.UserDtos;
using Weblu.Domain.Errors.Auth;

namespace Weblu.Application.Validations.Users
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
             RuleFor(x => x.UserName)
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
        }
    }
}