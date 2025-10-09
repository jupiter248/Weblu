using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.MethodDtos;
using Weblu.Domain.Errors.Methods;

namespace Weblu.Application.Validations.Methods
{
    public class UpdateMethodValidator : AbstractValidator<UpdateMethodDto>
    {
        public UpdateMethodValidator()
        {
            RuleFor(n => n.Name)
             .NotEmpty().WithMessage(MethodErrorCodes.MethodNameRequired)
             .MaximumLength(50).WithMessage(MethodErrorCodes.MethodNameMaxLength);

            RuleFor(n => n.Description)
            .NotEmpty().WithMessage(MethodErrorCodes.MethodDescriptionRequired)
            .MaximumLength(150).WithMessage(MethodErrorCodes.MethodDescriptionMaxLength);
        }
    }
}