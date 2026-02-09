using FluentValidation;
using Weblu.Application.DTOs.Tickets.TicketMessageDTOs;
using Weblu.Domain.Errors.Tickets;

namespace Weblu.Application.Validations.Tickets
{
    public class EditTicketMessageValidator : AbstractValidator<EditTicketMessageDTO>
    {
        public EditTicketMessageValidator()
        {
            RuleFor(m => m.Message)
                .NotEmpty().WithMessage(TicketMessageErrorCodes.MessageRequired)
                .MaximumLength(700).WithMessage(TicketMessageErrorCodes.MessageMaxLength);
        }
    }
}