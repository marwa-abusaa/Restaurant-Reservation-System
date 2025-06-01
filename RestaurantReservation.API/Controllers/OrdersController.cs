using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Orders;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : Controller
{
    private readonly OrderRepository _orderRepository;
    private readonly EmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public OrdersController(IMapper mapper, OrderRepository orderRepository, EmployeeRepository employeeRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    [HttpGet("{id}", Name = "GetOrder")]
    public async Task<ActionResult<OrderDto>> GetOrder(int id)
    {
        var order = await _orderRepository.GetById(id);
        if (order == null)
            return NotFound(new { Message = $"Order with ID {id} not found." });

        return Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateDto orderCreateDto)
    {
        if (!await _employeeRepository.IsEmployeeExists(orderCreateDto.EmployeeId))
        {
            return NotFound(new { Message = "Employee not found." });
        }

        var order = _mapper.Map<Order>(orderCreateDto);
        var addedOrder = await _orderRepository.Create(order);
        var createdOrderReturn = _mapper.Map<OrderDto>(addedOrder);

        return CreatedAtRoute("GetOrder", new { id = addedOrder.OrderId }, createdOrderReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        var existingOrder = await _orderRepository.GetById(id);
        if (existingOrder == null)
        {
            return NotFound();
        }

        await _orderRepository.DeleteById(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
    {
        var existingOrder = await _orderRepository.GetById(id);
        if (existingOrder == null)
        {
            return NotFound();
        }
        if (!await _employeeRepository.IsEmployeeExists(orderUpdateDto.EmployeeId))
        {
            return NotFound(new { Message = "Employee not found." });
        }

        _mapper.Map(orderUpdateDto, existingOrder);
        await _orderRepository.Update(existingOrder);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateOrder(int id, [FromBody] JsonPatchDocument<OrderUpdateDto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var existingOrder = await _orderRepository.GetById(id);
        if (existingOrder == null)
            return NotFound();

        var orderToPatch = _mapper.Map<OrderUpdateDto>(existingOrder);
        patchDocument.ApplyTo(orderToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _employeeRepository.IsEmployeeExists(orderToPatch.EmployeeId))
        {
            return NotFound(new { Message = "Employee not found." });
        }

        _mapper.Map(orderToPatch, existingOrder);
        await _orderRepository.Update(existingOrder);

        return NoContent();
    }
}
