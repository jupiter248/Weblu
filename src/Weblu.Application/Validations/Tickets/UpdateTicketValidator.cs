using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Weblu.Application.Dtos.TicketDtos;
using Weblu.Domain.Errors.Tickets;

namespace Weblu.Application.Validations.Tickets
{
    public class UpdateTicketValidator : AbstractValidator<UpdateTicketDto>
    {
        public UpdateTicketValidator()
        {
            RuleFor(c => c.Subject)
                .NotEmpty().WithMessage(TicketErrorCodes.SubjectRequired)
                .MaximumLength(100).WithMessage(TicketErrorCodes.SubjectMaxLength);
        }
    }
}