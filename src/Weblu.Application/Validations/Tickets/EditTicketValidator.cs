using FluentValidation;
using Weblu.Application.DTOs.Tickets.TicketDTOs;
using Weblu.Domain.Errors.Tickets;

namespace Weblu.Application.Validations.Tickets
{
    public class EditTicketValidator : AbstractValidator<EditTicketDTO>
    {
        public EditTicketValidator()
        {
            RuleFor(c => c.Subject)
                .NotEmpty().WithMessage(TicketErrorCodes.SubjectRequired)
                .MaximumLength(100).WithMessage(TicketErrorCodes.SubjectMaxLength);
        }
    }
}