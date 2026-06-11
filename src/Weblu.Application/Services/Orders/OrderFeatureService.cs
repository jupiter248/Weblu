using AutoMapper;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Services.Orders;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Errors.Orders;
using Weblu.Domain.Interfaces.Repositories;
using Weblu.Domain.Interfaces.Repositories.Common;
using Weblu.Domain.Interfaces.Repositories.Orders;

namespace Weblu.Application.Services.Orders;

public class OrderFeatureService : IOrderFeatureService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IFeatureRepository _featureRepository;

    public OrderFeatureService(
        IUnitOfWork unitOfWork,
        IOrderRepository orderRepository,
        IFeatureRepository featureRepository
        )
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _featureRepository = featureRepository;
    }
    public async Task AddAsync(int orderId, int featureId)
    {
        Order? order = await _orderRepository.GetByIdWithFeaturesAsync(orderId) ?? throw new NotFoundException(OrderErrorCodes.NotFound);
        Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

        order.AddFeature(feature);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int orderId, int featureId)
    {
        Order? order = await _orderRepository.GetByIdWithFeaturesAsync(orderId) ?? throw new NotFoundException(OrderErrorCodes.NotFound);
        Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

        order.RemoveFeature(feature);
        await _unitOfWork.CommitAsync();
    }
}