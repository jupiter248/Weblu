using FluentValidation;
using Weblu.Application.DTOs.Users.UserDTOs;
using Weblu.Domain.Errors.Users;

namespace Weblu.Application.Validations.Users
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangePasswordDTO>
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