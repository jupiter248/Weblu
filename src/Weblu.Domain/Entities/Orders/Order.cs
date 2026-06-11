using System.Net;
using Weblu.Domain.Entities.Common;
using Weblu.Domain.Entities.Common.Features;
using Weblu.Domain.Entities.Common.Methods;
using Weblu.Domain.Entities.Services;
using Weblu.Domain.Errors.Common;
using Weblu.Domain.Errors.Orders;
using Weblu.Domain.Exceptions;

namespace Weblu.Domain.Entities.Orders;

public class Order : BaseEntity
{
    // Required properties
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    // Image and its description

    // Relationships
    public int StatusId { get; set; }
    public OrderStatus Status { get; set; } = default!;
    public int ServiceId { get; set; } = default!;
    public Service Service { get; set; } = default!;
    public int MethodId { get; set; } = default!;
    public Method Method { get; set; } = default!;
    public List<Feature> Features { get; set; } = new List<Feature>();
    public string OwnerId { get; set; } = default!;

    public void AddFeature(Feature feature)
    {
        if (Features.Any(f => f.Id == feature.Id))
        {
            throw new DomainException(OrderErrorCodes.FeatureAlreadyAdded, 409);
        }
        Features.Add(feature);
    }
    public void RemoveFeature(Feature feature)
    {
        if (!Features.Any(f => f.Id == feature.Id))
        {
            throw new DomainException(FeatureErrorCodes.FeatureNotFound, 404);
        }
        Features.Add(feature);
    }
    public void ChangeStatus(OrderStatus status)
    {
        if (StatusId == status.Id) return;
        StatusId = status.Id;
        Status = status;
    }
}