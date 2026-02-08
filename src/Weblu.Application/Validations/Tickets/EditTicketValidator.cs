using FluentValidation;
using Weblu.Application.Dtos.Tickets.TicketDtos;
using Weblu.Domain.Errors.Tickets;

namespace Weblu.Application.Validations.Tickets
{
    public class EditTicketValidator : AbstractValidator<EditTicketDto>
    {
        public EditTicketValidator()
        {
            RuleFor(c => c.Subject)
                .NotEmpty().WithMessage(TicketErrorCodes.SubjectRequired)
                .MaximumLength(100).WithMessage(TicketErrorCodes.SubjectMaxLength);
        }
    }
}