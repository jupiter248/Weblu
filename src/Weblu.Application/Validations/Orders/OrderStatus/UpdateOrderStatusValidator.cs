using FluentValidation;
using Weblu.Application.DTOs.Orders.OrderStatusDTOs;
using Weblu.Domain.Errors.Orders;

namespace Weblu.Application.Validations.Orders.OrderStatus;

public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusDTO>
{
    public UpdateOrderStatusValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(OrderStatusErrorCodes.NameRequired)
            .MaximumLength(100).WithMessage(OrderStatusErrorCodes.NameMaxLength);

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage(OrderStatusErrorCodes.DescriptionMaxLength)
            .When(x => x.Description is not null);
    }
}