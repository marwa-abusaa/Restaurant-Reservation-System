using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.API.Models.Paging;
using RestaurantReservation.API.Services;
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
    const int maxPageSize = 5;

    public CustomersController(IMapper mapper, CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }


    /// <summary>
    /// Gets all customers with pagination.
    /// </summary>
    /// <param name="pageNumber">The number of the page to retrieve.</param>
    /// <param name="pageSize">The number of items per page.</param>
    /// <returns>A list of customer DTOs.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCustomers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
    {
        if (pageNumber <= 0 || pageSize <= 0)
            return BadRequest("Page and pageSize must be greater than zero.");

        var query = _customerRepository.GetAll();

        var pagedResult = await PaginationHelper<Customer>.GetPagedResult(query, pageNumber, pageSize, maxPageSize);

        var pagedResultDto = new PagedResult<CustomerDto>
        {
            PageNumber = pagedResult.PageNumber,
            PageSize = pagedResult.PageSize,
            TotalCount = pagedResult.TotalCount,
            TotalPages = pagedResult.TotalPages,
            HasNextPage = pagedResult.HasNextPage,
            HasPreviousPage = pagedResult.HasPreviousPage,
            Items = _mapper.Map<List<CustomerDto>>(pagedResult.Items)
        };

        return Ok(pagedResultDto);
    }

    /// <summary>
    /// Retrieves a customer by their ID.
    /// </summary>
    /// <param name="id">The ID of the customer to retrieve.</param>
    /// <returns> A customer DTO if found; otherwise, a 404 Not Found response.</returns>
    [HttpGet("{id}", Name = "GetCustomer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
    {
        var customer = await _customerRepository.GetById(id);
        if (customer == null)
            return NotFound();

        return Ok(_mapper.Map<CustomerDto>(customer));
    }


    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="customerCreateDto">The customer data to create.</param>
    /// <returns>The created customer DTO.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateDto customerCreateDto)
    {
        var customer = _mapper.Map<Customer>(customerCreateDto);
        var addedCustomer = await _customerRepository.Create(customer);
        var createdCustomerReturn = _mapper.Map<CustomerDto>(addedCustomer);

        return CreatedAtRoute("GetCustomer", new { id = addedCustomer.CustomerId }, createdCustomerReturn);
    }


    /// <summary>
    /// Deletes a customer by ID.
    /// </summary>
    /// <param name="id">The ID of the customer to delete.</param>
    /// <returns>No content if successful; otherwise, a 404 Not Found response.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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


    /// <summary>
    /// Updates an existing customer by ID.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="customerUpdateDto">The updated customer data.</param>
    /// <returns>No content if successful; otherwise, a 404 Not Found response.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


    /// <summary>
    /// Partially updates an existing customer by ID using a JSON patch document.
    /// </summary>
    /// <param name="id">The ID of the customer to update.</param>
    /// <param name="patchDocument">The JSON patch document containing updates.</param>
    /// <returns>No content if successful; otherwise, a 404 Not Found response.</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


    /// <summary>
    /// Retrieves all customers who have made reservations with a party size greater than the specified minimum.
    /// </summary>
    /// <param name="minPartySize">The minimum party size to filter reservations.</param>
    /// <returns> A list of customer DTOs </returns>
    [HttpGet("reservations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
