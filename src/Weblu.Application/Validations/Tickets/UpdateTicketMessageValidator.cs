using FluentValidation;
using Weblu.Application.Dtos.Tickets.TicketMessageDtos;
using Weblu.Domain.Errors.Tickets;

namespace Weblu.Application.Validations.Tickets
{
    public class UpdateTicketMessageValidator : AbstractValidator<UpdateTicketMessageDto>
    {
        public UpdateTicketMessageValidator()
        {
            RuleFor(m => m.Message)
                .NotEmpty().WithMessage(TicketMessageErrorCodes.MessageRequired)
                .MaximumLength(700).WithMessage(TicketMessageErrorCodes.MessageMaxLength);
        }
    }
}