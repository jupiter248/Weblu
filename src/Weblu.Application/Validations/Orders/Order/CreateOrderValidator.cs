using FluentValidation;
using Weblu.Application.DTOs.Orders.OrderDTOs;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Orders;
using Weblu.Domain.Errors.Services;

namespace Weblu.Application.Validations.Orders.Order;

public class CreateOrderValidator : AbstractValidator<CreateOrderDTO>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage(ServiceErrorCodes.InvalidId);

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage(OrderStatusErrorCodes.InvalidId);

        RuleFor(x => x.MethodId)
            .GreaterThan(0).WithMessage(MethodErrorCodes.InvalidId);
    }
}