using AutoMapper;
using Weblu.Application.DTOs.Orders.OrderStatusDTOs;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Services.Orders;
using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Errors.Orders;
using Weblu.Domain.Interfaces.Repositories;
using Weblu.Domain.Interfaces.Repositories.Orders;
using Weblu.Domain.Parameters.Orders;

namespace Weblu.Application.Services.Orders;

public class OrderStatusService : IOrderStatusService
{
    private readonly IOrderStatusRepository _orderStatusRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrderStatusService(IUnitOfWork unitOfWork, IMapper mapper, IOrderStatusRepository orderStatusRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _orderStatusRepository = orderStatusRepository;
    }
    public async Task<OrderStatusDTO> CreateAsync(CreateOrderStatusDTO createOrderStatusDTO)
    {
        OrderStatus orderStatus = _mapper.Map<OrderStatus>(createOrderStatusDTO);

        _orderStatusRepository.Add(orderStatus);
        await _unitOfWork.CommitAsync();

        OrderStatusDTO orderStatusDTO = _mapper.Map<OrderStatusDTO>(orderStatus);
        return orderStatusDTO;
    }

    public async Task DeleteAsync(int statusId)
    {
        OrderStatus orderStatus = await _orderStatusRepository.GetByIdAsync(statusId) ?? throw new NotFoundException(OrderStatusErrorCodes.NotFound);
        orderStatus.Delete();
        await _unitOfWork.CommitAsync();
    }

    public async Task<List<OrderStatusDTO>> GetAllAsync(OrderStatusParameters orderStatusParameters)
    {
        IReadOnlyList<OrderStatus> orderStatuses = await _orderStatusRepository.GetAllAsync(orderStatusParameters);
        List<OrderStatusDTO> orderStatusDTOs = _mapper.Map<List<OrderStatusDTO>>(orderStatuses);
        return orderStatusDTOs;
    }

    public async Task<OrderStatusDTO> GetByIdAsync(int statusId)
    {
        OrderStatus orderStatus = await _orderStatusRepository.GetByIdAsync(statusId) ?? throw new NotFoundException(OrderStatusErrorCodes.NotFound);
        OrderStatusDTO orderStatusDTO = _mapper.Map<OrderStatusDTO>(orderStatus);
        return orderStatusDTO; throw new NotImplementedException();
    }

    public async Task<OrderStatusDTO> EditAsync(int statusId, UpdateOrderStatusDTO updateOrderStatusDTO)
    {
        OrderStatus orderStatus = await _orderStatusRepository.GetByIdAsync(statusId) ?? throw new NotFoundException(OrderStatusErrorCodes.NotFound);
        orderStatus = _mapper.Map(updateOrderStatusDTO, orderStatus);

        orderStatus.MarkUpdated();
        _orderStatusRepository.Update(orderStatus);
        await _unitOfWork.CommitAsync();

        OrderStatusDTO orderStatusDTO = _mapper.Map<OrderStatusDTO>(orderStatus);
        return orderStatusDTO; throw new NotImplementedException();
    }
}