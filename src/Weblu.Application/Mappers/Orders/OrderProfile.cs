using System.Net;
using AutoMapper;
using Weblu.Application.DTOs.Orders.OrderDTOs;
using Weblu.Application.DTOs.Orders.OrderStatusDTOs;
using Weblu.Application.Extensions;
using Weblu.Application.Helpers;
using Weblu.Domain.Entities.Orders;

namespace Weblu.Application.Mappers.Orders;

public class OrderProfile : Profile
{
    protected OrderProfile()
    {
        // Order
        CreateMap<Order, OrderDetailDTO>()
            .ForMember(x => x.OrderedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
            .ForMember(x => x.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : string.Empty))
            .ForMember(x => x.StatusName, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(x => x.ServiceName, opt => opt.MapFrom(src => src.Service.Title))
            .ForMember(x => x.MethodName, opt => opt.MapFrom(src => src.Method.Name));

        CreateMap<Order, OrderSummeryDTO>()
            .ForMember(x => x.OrderedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
            .ForMember(x => x.StatusName, opt => opt.MapFrom(src => src.Status.Name))
            .ForMember(x => x.StatusName, opt => opt.MapFrom(src => src.Status.Name));

        CreateMap<CreateOrderDTO, Order>();

        CreateMap<UpdateOrderDTO, Order>();

        // Order Status
        CreateMap<OrderStatus, OrderStatusDTO>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShamsi()))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.HasValue ? src.UpdatedAt.Value.ToShamsi() : null));
        CreateMap<CreateOrderStatusDTO, OrderStatus>();
        CreateMap<UpdateOrderStatusDTO, OrderStatus>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now));
    }
}