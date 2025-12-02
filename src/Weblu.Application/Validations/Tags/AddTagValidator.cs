using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.TagDtos;
using Weblu.Domain.Errors.Tags;

namespace Weblu.Application.Validations.Tags
{
    public class AddTagValidator : AbstractValidator<AddTagDto>
    {
        public AddTagValidator()
        {
            RuleFor(n => n.Name)
            .NotEmpty().WithMessage(TagErrorCodes.NameRequired)
            .MaximumLength(100).WithMessage(TagErrorCodes.NameMaxLength);

            RuleFor(n => n.Description)
            .MaximumLength(1000).WithMessage(TagErrorCodes.DescriptionMaxLength);
        }
    }
}