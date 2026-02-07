using FluentValidation;
using Weblu.Application.Dtos.Users.UserDtos;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Validations.Users
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangeUserPasswordValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage(UserErrorCodes.OldPasswordRequired);
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(UserErrorCodes.NewPasswordLength)
                .Length(8, 100).WithMessage(UserErrorCodes.NewPasswordLength);
        }
    }
}