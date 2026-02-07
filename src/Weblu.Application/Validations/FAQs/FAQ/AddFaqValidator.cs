using FluentValidation;
using Weblu.Application.Dtos.FAQs.FaqDtos;
using Weblu.Domain.Errors.FAQs;

namespace Weblu.Application.Validations.FAQs.FAQ
{
    public class AddFaqValidator : AbstractValidator<AddFaqDto>
    {
        public AddFaqValidator()
        {
            RuleFor(f => f.Question)
                .NotEmpty().WithMessage(FaqErrorCodes.QuestionRequired)
                .MaximumLength(500).WithMessage(FaqErrorCodes.QuestionMaximumLength);

            RuleFor(f => f.Answer)
                .NotEmpty().WithMessage(FaqErrorCodes.AnswerRequired)
                .MaximumLength(5000).WithMessage(FaqErrorCodes.AnswerMaximumLength);
        }
    }
}