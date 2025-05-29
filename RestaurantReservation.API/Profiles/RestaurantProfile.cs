using AutoMapper;
using RestaurantReservation.API.Models.Restaurants;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Profiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<RestaurantCreateDto, Restaurant>();
        CreateMap<RestaurantUpdateDto, Restaurant>().ReverseMap();
    }
}
