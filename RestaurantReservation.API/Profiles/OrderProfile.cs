using AutoMapper;
using RestaurantReservation.API.Models.Orders;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>();
        CreateMap<OrderCreateDto, Order>();
        CreateMap<OrderUpdateDto, Order>().ReverseMap();
    }
}
