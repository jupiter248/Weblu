using AutoMapper;
using Weblu.Application.Common.Models;
using Weblu.Application.DTOs.Orders.OrderDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Extensions;
using Weblu.Application.Interfaces.Services.Common;
using Weblu.Application.Interfaces.Services.Orders;
using Weblu.Application.Interfaces.Services.ServiceServices;
using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Errors.Methods;
using Weblu.Domain.Errors.Orders;
using Weblu.Domain.Errors.Services;
using Weblu.Domain.Errors.Users;
using Weblu.Domain.Interfaces.Repositories;
using Weblu.Domain.Interfaces.Repositories.Common;
using Weblu.Domain.Interfaces.Repositories.Orders;
using Weblu.Domain.Interfaces.Repositories.Services;
using Weblu.Domain.Interfaces.Repositories.Users;
using Weblu.Domain.Parameters.Orders;

namespace Weblu.Application.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    public readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IServiceRepository _serviceRepository;
    private readonly IMethodRepository _methodRepository;
    private readonly IUserRepository _userRepository;
    public OrderService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOrderRepository orderRepository,
        IOrderStatusRepository orderStatusRepository,
        IServiceRepository serviceRepository,
        IMethodRepository methodRepository,
        IUserRepository userRepository
        )
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
        _serviceRepository = serviceRepository;
        _methodRepository = methodRepository;
        _userRepository = userRepository;
    }
    public async Task DeleteAsync(int orderId, string userId)
    {
        var userExists = await _userRepository.UserExistsAsync(userId);
        if (!userExists) throw new NotFoundException(UserErrorCodes.UserNotFound);

        var isAdmin = await _userRepository.IsAdminAsync(userId);
        var order = await _orderRepository.GetByIdAsync(orderId) ?? throw new NotFoundException(OrderErrorCodes.NotFound);

        if (!isAdmin && order.OwnerId != userId) throw new NotFoundException(OrderErrorCodes.NotFound);

        order.Delete();
        await _unitOfWork.CommitAsync();
    }

    public async Task<PagedResponse<OrderSummeryDTO>> GetAllPagedAsync(string userId, OrderParameters orderParameters)
    {
        var userExists = await _userRepository.UserExistsAsync(userId);
        if (!userExists) throw new NotFoundException(UserErrorCodes.UserNotFound);

        var isAdmin = await _userRepository.IsAdminAsync(userId);
        if (!isAdmin && orderParameters.UserId != userId) throw new NotFoundException(OrderErrorCodes.NotFound);

        var orders = await _orderRepository.GetAllAsync(orderParameters);
        var orderDtos = _mapper.Map<List<OrderSummeryDTO>>(orders);

        foreach (var item in orderDtos)
        {
            item.Username = await _userRepository.GetUsernameAsync(item.OwnerId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
        }

        var pagedOrders = _mapper.Map<PagedResponse<OrderSummeryDTO>>(orders);
        pagedOrders.Items = orderDtos;
        return pagedOrders;
    }

    public async Task<OrderDetailDTO> GetByIdAsync(int orderId, string userId)
    {
        var userExists = await _userRepository.UserExistsAsync(userId);
        if (!userExists) throw new NotFoundException(UserErrorCodes.UserNotFound);

        var isAdmin = await _userRepository.IsAdminAsync(userId);
        var order = await _orderRepository.GetByIdAsync(orderId) ?? throw new NotFoundException(OrderErrorCodes.NotFound);

        if (!isAdmin && order.OwnerId != userId) throw new NotFoundException(OrderErrorCodes.NotFound);

        var orderDTO = _mapper.Map<OrderDetailDTO>(order);
        orderDTO.Username = await _userRepository.GetUsernameAsync(orderDTO.OwnerId) ?? throw new NotFoundException(UserErrorCodes.UserNotFound);
        return orderDTO;
    }

    public async Task<OrderDetailDTO> OrderAsync(CreateOrderDTO createOrderDTO, string userId)
    {
        var userExists = await _userRepository.UserExistsAsync(userId);
        if (!userExists) throw new NotFoundException(UserErrorCodes.UserNotFound);


        var status = await _orderStatusRepository.GetByIdAsync(createOrderDTO.StatusId) ?? throw new NotFoundException(OrderStatusErrorCodes.NotFound);
        var service = await _serviceRepository.GetByIdWithFeaturesAsync(createOrderDTO.ServiceId) ?? throw new NotFoundException(ServiceErrorCodes.ServiceNotFound);
        var method = await _methodRepository.GetByIdAsync(createOrderDTO.MethodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

        var order = _mapper.Map<Order>(createOrderDTO);
        order.Name = $"{service.Title} + {method.Name}";
        order.Slug = order.Name.Slugify();
        order.Status = status;
        order.Service = service;
        order.Method = method;
        order.Features = service.Features;
        order.OwnerId = userId;


        _orderRepository.Add(order);
        await _unitOfWork.CommitAsync();

        var orderDTO = _mapper.Map<OrderDetailDTO>(order);

        var username = await _userRepository.GetUsernameAsync(userId);
        if (string.IsNullOrWhiteSpace(username)) throw new UnauthorizedException(UserErrorCodes.UserNotFound);

        orderDTO.Username = username;
        return orderDTO;
    }

    public async Task<OrderDetailDTO> UpdateAsync(string userId, int orderId, UpdateOrderDTO updateOrderDTO)
    {
        var userExists = await _userRepository.UserExistsAsync(userId);
        if (!userExists) throw new NotFoundException(UserErrorCodes.UserNotFound);

        var order = await _orderRepository.GetByIdAsync(orderId) ?? throw new NotFoundException(OrderErrorCodes.NotFound);

        var method = await _methodRepository.GetByIdAsync(updateOrderDTO.MethodId) ?? throw new NotFoundException(MethodErrorCodes.MethodNotFound);

        var updatedOrder = _mapper.Map<Order>(updateOrderDTO);
        updatedOrder.Name = $"{order.Service.Title} + {method.Name}";
        order.Slug = order.Name.Slugify();
        order.Method = method;

        updatedOrder.MarkUpdated();
        await _unitOfWork.CommitAsync();

        var orderDTO = _mapper.Map<OrderDetailDTO>(updatedOrder);
        var username = await _userRepository.GetUsernameAsync(userId);
        if (string.IsNullOrWhiteSpace(username)) throw new UnauthorizedException(UserErrorCodes.UserNotFound);

        orderDTO.Username = username;
        return orderDTO;
    }

    public async Task ChangeStatus(int orderId, int statusId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId) ?? throw new NotFoundException(OrderErrorCodes.NotFound);
        var status = await _orderStatusRepository.GetByIdAsync(statusId) ?? throw new NotFoundException(OrderStatusErrorCodes.NotFound);

        order.ChangeStatus(status);
        await _unitOfWork.CommitAsync();
    }
}