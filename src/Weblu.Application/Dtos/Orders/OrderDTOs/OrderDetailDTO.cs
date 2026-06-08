using Weblu.Application.DTOs.Common.FeatureDTOs;
using Weblu.Application.DTOs.Users.UserDTOs;

namespace Weblu.Application.DTOs.Orders.OrderDTOs;

public class OrderDetailDTO
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsUpdated { get; set; } = default!;

    public string? UpdatedAt { get; set; }
    public string OrderedAt { get; set; } = default!;

    public int ServiceId { get; set; }
    public string ServiceName { get; set; } = default!;
    public int MethodId { get; set; }
    public string MethodName { get; set; } = default!;
    public int StatusId { get; set; }
    public string StatusName { get; set; } = default!;
    public UserDTO UserDTO { get; set; } = default!;
    public List<FeatureDTO> Features { get; set; } = new List<FeatureDTO>();

}