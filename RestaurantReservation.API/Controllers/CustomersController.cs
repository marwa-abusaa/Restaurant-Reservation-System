using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private CustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomersController(IMapper mapper, CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]   
    public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
    {
        var customer = await _customerRepository.GetById(id);
        if (customer == null)
            return NotFound();

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    

}
