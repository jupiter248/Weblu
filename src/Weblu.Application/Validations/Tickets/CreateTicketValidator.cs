using FluentValidation;
using Weblu.Application.DTOs.Tickets.TicketDTOs;
using Weblu.Domain.Errors.Tickets;

namespace Weblu.Application.Validations.Tickets
{
    public class CreateTicketValidator : AbstractValidator<CreateTicketDTO>
    {
        public CreateTicketValidator()
        {
            RuleFor(c => c.Subject)
                .NotEmpty().WithMessage(TicketErrorCodes.SubjectRequired)
                .MaximumLength(100).WithMessage(TicketErrorCodes.SubjectMaxLength);

            RuleFor(m => m.Message)
                .NotEmpty().WithMessage(TicketMessageErrorCodes.MessageRequired)
                .MaximumLength(700).WithMessage(TicketMessageErrorCodes.MessageMaxLength);
        }
    }
}