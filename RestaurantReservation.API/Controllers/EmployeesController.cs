using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Customers;
using RestaurantReservation.API.Models.Employees;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : Controller
{
    private EmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeesController(IMapper mapper, EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
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

        _mapper.Map(employeeToPatch, existingEmployee);
        await _employeeRepository.Update(existingEmployee);

        return NoContent();
    }
}
