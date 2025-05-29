using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomersController : ControllerBase
{
    private CustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomersController(IMapper mapper, CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}", Name = "GetCustomer")]   
    public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
    {
        var customer = await _customerRepository.GetById(id);
        if (customer == null)
            return NotFound();

        return Ok(_mapper.Map<CustomerDto>(customer));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
    {
        var customer = _mapper.Map<Customer>(customerCreateDto);
        var addedCustomer = await _customerRepository.Create(customer);
        var createdCustomerReturn = _mapper.Map<CustomerDto>(addedCustomer);

        return CreatedAtRoute("GetCustomer", new { id = addedCustomer.CustomerId }, createdCustomerReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomerDto>> DeleteCustomer(int id)
    {
        var existingCustomer = await _customerRepository.GetById(id);
        if(existingCustomer == null)
        {
            return NotFound();
        }

        await _customerRepository.DeleteById(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerDto>> UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto)
    {
        var existingCustomer = await _customerRepository.GetById(id);
        if (existingCustomer == null)
        {
            return NotFound();
        }

        _mapper.Map(customerUpdateDto, existingCustomer);
        await _customerRepository.Update(existingCustomer);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateCustomer(int id, [FromBody] JsonPatchDocument<CustomerUpdateDto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var existingCustomer = await _customerRepository.GetById(id);
        if (existingCustomer == null)
            return NotFound();

        var customerToPatch = _mapper.Map<CustomerUpdateDto>(existingCustomer);

        patchDocument.ApplyTo(customerToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _mapper.Map(customerToPatch, existingCustomer);
        await _customerRepository.Update(existingCustomer);

        return NoContent();
    }

    [HttpGet("reservations")]
    public async Task<ActionResult<IEnumerable<CustomerDto>>> ListCustomersWithReservationsAbovePartySize([FromQuery] int minPartySize)
    {
        var customers = await _customerRepository.GetCustomersWithReservationsAbovePartySize(minPartySize);

        if (!customers.Any())
        {
            return Ok(new { Message = $"No customers have made reservations with a party size greater than {minPartySize}" });
        }
        return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customers));
    }
}
