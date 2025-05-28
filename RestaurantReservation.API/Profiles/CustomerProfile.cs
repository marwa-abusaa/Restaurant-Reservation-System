using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.Db.Models;
using AutoMapper;

namespace RestaurantReservation.API.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerCreateDto, Customer>();
        CreateMap<Customer, CustomerUpdateDto>().ReverseMap();
    }
}
