using FluentValidation;
using Weblu.Application.Dtos.Tickets.TicketMessageDtos;
using Weblu.Domain.Errors.Tickets;

namespace Weblu.Application.Validations.Tickets
{
    public class EditTicketMessageValidator : AbstractValidator<EditTicketMessageDto>
    {
        public EditTicketMessageValidator()
        {
            RuleFor(m => m.Message)
                .NotEmpty().WithMessage(TicketMessageErrorCodes.MessageRequired)
                .MaximumLength(700).WithMessage(TicketMessageErrorCodes.MessageMaxLength);
        }
    }
}