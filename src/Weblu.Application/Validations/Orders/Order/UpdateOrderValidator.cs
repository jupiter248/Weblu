using FluentValidation;
using Weblu.Application.DTOs.Orders.OrderDTOs;
using Weblu.Domain.Errors.Methods;

namespace Weblu.Application.Validations.Orders.Order;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderDTO>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.MethodId)
            .GreaterThan(0).WithMessage(MethodErrorCodes.InvalidId);
    }
}