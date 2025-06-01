using AutoMapper;
using RestaurantReservation.API.Models.Tables;
using RestaurantReservation.Db.Models;

namespace RestaurantReservation.API.Profiles;

public class TableProfile : Profile
{
    public TableProfile()
    {
        CreateMap<Table, TableDto>();
        CreateMap<TableCreateDto, Table>();
        CreateMap<TableUpdateDto, Table>().ReverseMap();
    }
}
