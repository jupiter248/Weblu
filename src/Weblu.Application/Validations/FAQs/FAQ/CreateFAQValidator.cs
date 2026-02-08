using FluentValidation;
using Weblu.Application.Dtos.FAQs.FAQDtos;
using Weblu.Domain.Errors.FAQs;

namespace Weblu.Application.Validations.FAQs.FAQ
{
    public class CreateFAQValidator : AbstractValidator<CreateFAQDto>
    {
        public CreateFAQValidator()
        {
            RuleFor(f => f.Question)
                .NotEmpty().WithMessage(FAQErrorCodes.QuestionRequired)
                .MaximumLength(500).WithMessage(FAQErrorCodes.QuestionMaximumLength);

            RuleFor(f => f.Answer)
                .NotEmpty().WithMessage(FAQErrorCodes.AnswerRequired)
                .MaximumLength(5000).WithMessage(FAQErrorCodes.AnswerMaximumLength);
        }
    }
}