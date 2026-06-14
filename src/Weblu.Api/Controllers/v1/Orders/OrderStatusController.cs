using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Models;
using Weblu.Application.DTOs.Orders.OrderStatusDTOs;
using Weblu.Application.Interfaces.Services.Orders;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Orders.OrderStatus;
using Weblu.Domain.Parameters.Orders;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Orders;

[ApiController]
[Route("api/order-status")]
[ApiVersion("1")]
public class OrderStatusStatusController : ControllerBase
{
    private readonly IOrderStatusService _orderStatusService;
    public OrderStatusStatusController(IOrderStatusService orderStatusService)
    {
        _orderStatusService = orderStatusService;
    }
    [Authorize(Policy = Permissions.ManageOrdersStatus)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] OrderStatusParameters orderStatusParameters)
    {
        List<OrderStatusDTO> orderStatusDTOs = await _orderStatusService.GetAllAsync(orderStatusParameters);
        return Ok(orderStatusDTOs);
    }
    [Authorize(Policy = Permissions.ManageOrdersStatus)]
    [HttpGet("{orderStatusId:int}")]
    public async Task<IActionResult> GetById(int orderStatusId)
    {
        OrderStatusDTO orderStatusDTO = await _orderStatusService.GetByIdAsync(orderStatusId);
        return Ok(orderStatusDTO);
    }
    [Authorize(Policy = Permissions.ManageOrdersStatus)]
    [HttpPost]
    public async Task<IActionResult> CreateStatus([FromBody] CreateOrderStatusDTO createOrderStatusDTO)
    {
        Validator.ValidateAndThrow(createOrderStatusDTO, new CreateOrderStatusValidator());
        OrderStatusDTO orderStatusDTO = await _orderStatusService.CreateAsync(createOrderStatusDTO);
        return CreatedAtAction(nameof(GetById), new { articleId = orderStatusDTO.Id }, ApiResponse<OrderStatusDTO>.Success("OrderStatus created successfully", orderStatusDTO));
    }
    [Authorize(Policy = Permissions.ManageOrdersStatus)]
    [HttpPut("{orderStatusId:int}")]
    public async Task<IActionResult> Edit(int orderStatusId, [FromBody] UpdateOrderStatusDTO updateOrderStatusDTO)
    {
        Validator.ValidateAndThrow(updateOrderStatusDTO, new UpdateOrderStatusValidator());
        OrderStatusDTO orderStatusDetailDTO = await _orderStatusService.EditAsync(orderStatusId, updateOrderStatusDTO);
        return Ok(
            ApiResponse<OrderStatusDTO>.Success
            (
                "OrderStatus updated successfully",
                orderStatusDetailDTO
            )
        );
    }
    [Authorize(Policy = Permissions.ManageOrdersStatus)]
    [HttpDelete("{orderStatusId:int}")]
    public async Task<IActionResult> Delete(int orderStatusId)
    {
        await _orderStatusService.DeleteAsync(orderStatusId);
        return NoContent();
    }
}