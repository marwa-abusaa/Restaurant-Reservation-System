using AutoMapper;
using RestaurantReservation.API.Models.OrderItems;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Profiles;

public class OrderItemProfile : Profile
{
    public OrderItemProfile()
    {
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItemCreateDto, OrderItem>();
        CreateMap<OrderItemUpdateDto, OrderItem>().ReverseMap();
    }
}
