using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.Db.Models;
using AutoMapper;

namespace RestaurantReservation.API.Profiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeCreateDto, Employee>();
        CreateMap<Employee, EmployeeUpdateDto>().ReverseMap();
    }
}
