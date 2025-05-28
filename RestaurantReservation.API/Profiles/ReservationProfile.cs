using AutoMapper;
using RestaurantReservation.API.Models.Reservations;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Profiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Reservation, ReservationDto>();
        CreateMap<ReservationCreateDto, Reservation>();
        CreateMap<ReservationUpdateDto, Reservation>().ReverseMap();
    }
}
