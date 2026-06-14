using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weblu.Application.Common.Models;
using Weblu.Application.DTOs.Orders.OrderDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Helpers;
using Weblu.Application.Interfaces.Services.Orders;
using Weblu.Application.Validations;
using Weblu.Application.Validations.Orders.Order;
using Weblu.Domain.Errors.Users;
using Weblu.Domain.Parameters.Orders;
using Weblu.Infrastructure.Identity.Authorization;

namespace Weblu.Api.Controllers.v1.Orders;

[ApiController]
[Route("api/order")]
[ApiVersion("1")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IOrderFeatureService _orderFeatureService;
    public OrderController(IOrderService orderService, IOrderFeatureService orderFeatureService)
    {
        _orderService = orderService;
        _orderFeatureService = orderFeatureService;
    }
    [Authorize(Policy = Permissions.ManageOrders)]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] OrderParameters orderParameters)
    {
        var userId = User.GetUserId() ?? throw new UnauthorizedException(UserErrorCodes.UserNotFound);
        PagedResponse<OrderSummeryDTO> orderSummeryDTOs = await _orderService.GetAllPagedAsync(userId, orderParameters);
        return Ok(orderSummeryDTOs);
    }
    [Authorize(Policy = Permissions.ManageOrders)]
    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> GetById(int orderId)
    {
        var userId = User.GetUserId() ?? throw new UnauthorizedException(UserErrorCodes.UserNotFound);
        OrderDetailDTO orderDetailDTO = await _orderService.GetByIdAsync(orderId, userId);
        return Ok(orderDetailDTO);
    }
    [Authorize(Policy = Permissions.ManageOrders)]
    [HttpPost]
    public async Task<IActionResult> Order([FromBody] CreateOrderDTO createOrderDTO)
    {
        Validator.ValidateAndThrow(createOrderDTO, new CreateOrderValidator());
        var userId = User.GetUserId() ?? throw new UnauthorizedException(UserErrorCodes.UserNotFound);
        OrderDetailDTO orderDetailDTO = await _orderService.OrderAsync(createOrderDTO, userId);
        return CreatedAtAction(nameof(GetById), new { articleId = orderDetailDTO.Id }, ApiResponse<OrderDetailDTO>.Success("Order created successfully", orderDetailDTO));
    }
    [Authorize(Policy = Permissions.ManageOrders)]
    [HttpPut("{orderId:int}")]
    public async Task<IActionResult> Edit(int orderId, [FromBody] UpdateOrderDTO updateOrderDTO)
    {
        Validator.ValidateAndThrow(updateOrderDTO, new UpdateOrderValidator());
        var userId = User.GetUserId() ?? throw new UnauthorizedException(UserErrorCodes.UserNotFound);
        OrderDetailDTO orderDetailDTO = await _orderService.UpdateAsync(userId, orderId, updateOrderDTO);
        return Ok(
            ApiResponse<OrderDetailDTO>.Success
            (
                "Order updated successfully",
                orderDetailDTO
            )
        );
    }
    [Authorize(Policy = Permissions.ManageOrders)]
    [HttpDelete("{orderId:int}")]
    public async Task<IActionResult> Delete(int orderId)
    {
        var userId = User.GetUserId() ?? throw new UnauthorizedException(UserErrorCodes.UserNotFound);
        await _orderService.DeleteAsync(orderId, userId);
        return NoContent();
    }
    [Authorize(Policy = Permissions.ManageOrders)]
    [HttpPost("{orderId:int}/feature/{featureId:int}")]
    public async Task<IActionResult> AddFeature(int orderId, int featureId)
    {
        var userId = User.GetUserId() ?? throw new UnauthorizedException(UserErrorCodes.UserNotFound);
        await _orderFeatureService.AddAsync(userId, orderId, featureId);
        return Ok(ApiResponse.Success("Feature added successfully"));
    }
    [Authorize(Policy = Permissions.ManageOrders)]
    [HttpDelete("{orderId:int}/feature/{featureId:int}")]
    public async Task<IActionResult> DeleteFeature(int orderId, int featureId)
    {
        var userId = User.GetUserId() ?? throw new UnauthorizedException(UserErrorCodes.UserNotFound);
        await _orderFeatureService.DeleteAsync(userId, orderId, featureId);
        return Ok(ApiResponse.Success("Feature deleted successfully"));
    }
}