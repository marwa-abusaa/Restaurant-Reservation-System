using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : Controller
{
    private EmployeeRepository _employeeRepository;
    private RestaurantRepository _restaurantRepository;
    private OrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public EmployeesController(IMapper mapper, EmployeeRepository employeeRepository, RestaurantRepository restaurantRepository, OrderRepository orderRepository)
    {
        _employeeRepository = employeeRepository;
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _orderRepository = orderRepository;
    }

    [HttpGet("{id}", Name = "GetEmployee")]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
    {
        var employee = await _employeeRepository.GetById(id);
        if (employee == null)
            return NotFound();

        return Ok(_mapper.Map<EmployeeDto>(employee));
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeCreateDto employeeCreateDto)
    {
        if (!await _restaurantRepository.IsRestaurantExists(employeeCreateDto.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }
        var employee = _mapper.Map<Employee>(employeeCreateDto);
        var addedEmployee = await _employeeRepository.Create(employee);
        var createdEmployeeReturn = _mapper.Map<EmployeeDto>(addedEmployee);

        return CreatedAtRoute("GetEmployee", new { id = addedEmployee.EmployeeId }, createdEmployeeReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<EmployeeDto>> DeleteEmployee(int id)
    {
        var existingEmployee = await _employeeRepository.GetById(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }

        await _employeeRepository.DeleteById(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmployeeDto>> UpdateEmployee(int id, EmployeeUpdateDto employeeUpdateDto)
    {
        var existingEmployee = await _employeeRepository.GetById(id);
        if (existingEmployee == null)
        {
            return NotFound();
        }
        if (!await _restaurantRepository.IsRestaurantExists(employeeUpdateDto.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        _mapper.Map(employeeUpdateDto, existingEmployee);
        await _employeeRepository.Update(existingEmployee);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateEmployee(int id, [FromBody] JsonPatchDocument<EmployeeUpdateDto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var existingEmployee = await _employeeRepository.GetById(id);
        if (existingEmployee == null)
            return NotFound();

        var employeeToPatch = _mapper.Map<EmployeeUpdateDto>(existingEmployee);

        patchDocument.ApplyTo(employeeToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _restaurantRepository.IsRestaurantExists(employeeToPatch.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        _mapper.Map(employeeToPatch, existingEmployee);
        await _employeeRepository.Update(existingEmployee);

        return NoContent();
    }

    [HttpGet("managers")]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetManagers()
    {
        var managers = await _employeeRepository.ListManagers();
        

        return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(managers));
    }

    [HttpGet("{employeeId}/average-order-amount")]
    public async Task<IActionResult> GetAverageOrderAmount(int employeeId)
    {
        if (!await _employeeRepository.IsEmployeeExists(employeeId))
        {
            return NotFound(new { Message = "Employee not found." });
        }

        var averageOrderAmount = await _orderRepository.CalculateAverageOrderAmount(employeeId);

        if (averageOrderAmount == 0)
        {
            return Ok(new { Message = "No orders found for the specified employee." });
        }

        return Ok(new { averageOrderAmount });
    }
}
