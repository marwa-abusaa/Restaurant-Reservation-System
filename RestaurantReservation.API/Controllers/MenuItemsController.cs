using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.MenuItems;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : Controller
{
    private MenuItemRepository _menuItemRepository;
    private RestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public MenuItemsController(IMapper mapper, MenuItemRepository menuItemRepository, RestaurantRepository restaurantRepository)
    {
        _menuItemRepository = menuItemRepository;
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}", Name = "GetMenuItem")]
    public async Task<ActionResult<MenuItemDto>> GetMenuItem(int id)
    {
        var menuItem = await _menuItemRepository.GetById(id);
        if (menuItem == null)
            return NotFound();

        return Ok(_mapper.Map<MenuItemDto>(menuItem));
    }

    [HttpPost]
    public async Task<ActionResult<MenuItemDto>> CreateMenuItem(MenuItemCreateDto menuItemCreateDto)
    {
        if (!await _restaurantRepository.IsRestaurantExists(menuItemCreateDto.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        var menuItem = _mapper.Map<MenuItem>(menuItemCreateDto);
        var addedMenuItem = await _menuItemRepository.Create(menuItem);
        var createdMenuItemReturn = _mapper.Map<MenuItemDto>(addedMenuItem);

        return CreatedAtRoute("GetMenuItem", new { id = addedMenuItem.MenuItemId }, createdMenuItemReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMenuItem(int id)
    {
        var existingMenuItem = await _menuItemRepository.GetById(id);
        if (existingMenuItem == null)
        {
            return NotFound();
        }

        await _menuItemRepository.DeleteById(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMenuItem(int id, MenuItemUpdateDto menuItemUpdateDto)
    {
        var existingMenuItem = await _menuItemRepository.GetById(id);
        if (existingMenuItem == null)
        {
            return NotFound();
        }
        if (!await _restaurantRepository.IsRestaurantExists(menuItemUpdateDto.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        _mapper.Map(menuItemUpdateDto, existingMenuItem);
        await _menuItemRepository.Update(existingMenuItem);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateMenuItem(int id, [FromBody] JsonPatchDocument<MenuItemUpdateDto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var existingMenuItem = await _menuItemRepository.GetById(id);
        if (existingMenuItem == null)
            return NotFound();

        var menuItemToPatch = _mapper.Map<MenuItemUpdateDto>(existingMenuItem);
        patchDocument.ApplyTo(menuItemToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _restaurantRepository.IsRestaurantExists(menuItemToPatch.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        _mapper.Map(menuItemToPatch, existingMenuItem);
        await _menuItemRepository.Update(existingMenuItem);

        return NoContent();
    }
}
