using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.OrderItems;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : Controller
{
    private readonly OrderItemRepository _orderItemRepository;
    private readonly MenuItemRepository _menuItemRepository;
    private readonly OrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderItemsController(IMapper mapper, OrderItemRepository orderItemRepository, MenuItemRepository menuItemRepository, OrderRepository orderRepository)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
        _menuItemRepository = menuItemRepository;
        _orderRepository = orderRepository;
    }

    [HttpGet("{id}", Name = "GetOrderItem")]
    public async Task<ActionResult<OrderItemDto>> GetOrderItem(int id)
    {
        var orderItem = await _orderItemRepository.GetById(id);
        if (orderItem == null)
            return NotFound(new { Message = "OrderItem not found." });

        return Ok(_mapper.Map<OrderItemDto>(orderItem));
    }

    [HttpPost]
    public async Task<ActionResult<OrderItemDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto)
    {
        if (!await _menuItemRepository.IsMenuItemExists(orderItemCreateDto.MenuItemId))
        {
            return NotFound(new { Message = "MenuItem not found." });
        }
        if (!await _orderRepository.IsOrderExists(orderItemCreateDto.OrderId))
        {
            return NotFound(new { Message = "Order not found." });
        }

        var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);
        var addedOrderItem = await _orderItemRepository.Create(orderItem);
        var createdOrderItemReturn = _mapper.Map<OrderItemDto>(addedOrderItem);

        return CreatedAtRoute("GetOrderItem", new { id = addedOrderItem.OrderItemId }, createdOrderItemReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrderItem(int id)
    {
        var existingOrderItem = await _orderItemRepository.GetById(id);
        if (existingOrderItem == null)
        {
            return NotFound();
        }

        await _orderItemRepository.DeleteById(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrderItem(int id, OrderItemUpdateDto orderItemUpdateDto)
    {
        var existingOrderItem = await _orderItemRepository.GetById(id);
        if (existingOrderItem == null)
        {
            return NotFound();
        }

        _mapper.Map(orderItemUpdateDto, existingOrderItem);
        await _orderItemRepository.Update(existingOrderItem);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateOrderItem(int id, [FromBody] JsonPatchDocument<OrderItemUpdateDto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var existingOrderItem = await _orderItemRepository.GetById(id);
        if (existingOrderItem == null)
            return NotFound();

        var orderItemToPatch = _mapper.Map<OrderItemUpdateDto>(existingOrderItem);
        patchDocument.ApplyTo(orderItemToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _mapper.Map(orderItemToPatch, existingOrderItem);
        await _orderItemRepository.Update(existingOrderItem);

        return NoContent();
    }
}
