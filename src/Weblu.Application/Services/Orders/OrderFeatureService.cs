using AutoMapper;
using Weblu.Application.Common.Services;
using Weblu.Application.Exceptions.CustomExceptions;
using Weblu.Application.Interfaces.Services.Orders;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Domain.Entities.Orders;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Errors.Orders;
using Weblu.Domain.Interfaces.Repositories;
using Weblu.Domain.Interfaces.Repositories.Common;
using Weblu.Domain.Interfaces.Repositories.Orders;
using Weblu.Domain.Interfaces.Repositories.Users;

namespace Weblu.Application.Services.Orders;

public class OrderFeatureService : BaseService, IOrderFeatureService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly IFeatureRepository _featureRepository;
    private readonly IUserRepository _userRepository;

    public OrderFeatureService(
        IUnitOfWork unitOfWork,
        IOrderRepository orderRepository,
        IFeatureRepository featureRepository,
        IUserRepository userRepository
        ) : base(userRepository)
    {
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _featureRepository = featureRepository;
        _userRepository = userRepository;
    }
    public async Task AddAsync(string userId, int orderId, int featureId)
    {
        await EnsureUserExistsAsync(userId);
        var isAdmin = await _userRepository.IsAdminAsync(userId);

        Order? order = await _orderRepository.GetByIdWithFeaturesAsync(orderId) ?? throw new NotFoundException(OrderErrorCodes.NotFound);

        if (!isAdmin && order.OwnerId != userId) throw new NotFoundException(OrderErrorCodes.NotFound);

        Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

        order.AddFeature(feature);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(string userId, int orderId, int featureId)
    {
        await EnsureUserExistsAsync(userId);

        var isAdmin = await _userRepository.IsAdminAsync(userId);

        Order? order = await _orderRepository.GetByIdWithFeaturesAsync(orderId) ?? throw new NotFoundException(OrderErrorCodes.NotFound);

        if (!isAdmin && order.OwnerId != userId) throw new NotFoundException(OrderErrorCodes.NotFound); Feature? feature = await _featureRepository.GetByIdAsync(featureId) ?? throw new NotFoundException(FeatureErrorCodes.FeatureNotFound);

        order.RemoveFeature(feature);
        await _unitOfWork.CommitAsync();
    }
}