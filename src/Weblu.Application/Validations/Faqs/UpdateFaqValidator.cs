using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.FaqDtos;
using Weblu.Domain.Errors.Faqs;

namespace Weblu.Application.Validations.Faqs
{
    public class UpdateFaqValidator : AbstractValidator<UpdateFaqDto>
    {
        public UpdateFaqValidator()
        {
            RuleFor(f => f.Question)
                .NotEmpty().WithMessage(FaqErrorCodes.QuestionRequired)
                .MaximumLength(255).WithMessage(FaqErrorCodes.QuestionMaximumLength);

            RuleFor(f => f.Answer)
                .NotEmpty().WithMessage(FaqErrorCodes.AnswerRequired)
                .MaximumLength(5000).WithMessage(FaqErrorCodes.AnswerMaximumLength);

        }
    }
}