using AutoMapper;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Profiles;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItem, MenuItemDto>();
        CreateMap<MenuItemCreateDto, MenuItem>();
        CreateMap<MenuItemUpdateDto, MenuItem>().ReverseMap();
    }
}
