using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.UserDtos;
using Weblu.Domain.Errors.Auth;
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