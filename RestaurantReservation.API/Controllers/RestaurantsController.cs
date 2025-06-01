using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Restaurants;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RestaurantsController : Controller
{
    private readonly RestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public RestaurantsController(IMapper mapper, RestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}", Name = "GetRestaurant")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
    {
        var restaurant = await _restaurantRepository.GetById(id);
        if (restaurant == null)
            return NotFound();

        return Ok(_mapper.Map<RestaurantDto>(restaurant));
    }

    [HttpPost]
    public async Task<ActionResult<RestaurantDto>> CreateRestaurant(RestaurantCreateDto restaurantCreateDto)
    {
        var restaurant = _mapper.Map<Restaurant>(restaurantCreateDto);
        var addedRestaurant = await _restaurantRepository.Create(restaurant);
        var createdRestaurantReturn = _mapper.Map<RestaurantDto>(addedRestaurant);

        return CreatedAtRoute("GetRestaurant", new { id = addedRestaurant.RestaurantId }, createdRestaurantReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRestaurant(int id)
    {
        var existingRestaurant = await _restaurantRepository.GetById(id);
        if (existingRestaurant == null)
        {
            return NotFound();
        }

        await _restaurantRepository.DeleteById(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRestaurant(int id, RestaurantUpdateDto restaurantUpdateDto)
    {
        var existingRestaurant = await _restaurantRepository.GetById(id);
        if (existingRestaurant == null)
        {
            return NotFound();
        }

        _mapper.Map(restaurantUpdateDto, existingRestaurant);
        await _restaurantRepository.Update(existingRestaurant);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateRestaurant(int id, [FromBody] JsonPatchDocument<RestaurantUpdateDto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var existingRestaurant = await _restaurantRepository.GetById(id);
        if (existingRestaurant == null)
            return NotFound();

        var restaurantToPatch = _mapper.Map<RestaurantUpdateDto>(existingRestaurant);
        patchDocument.ApplyTo(restaurantToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _mapper.Map(restaurantToPatch, existingRestaurant);
        await _restaurantRepository.Update(existingRestaurant);

        return NoContent();
    }

    [HttpGet("{restaurantId}/total-revenue")]
    public async Task<IActionResult> GetTotalRestaurantRevenue(int restaurantId)
    {
        if (!await _restaurantRepository.IsRestaurantExists(restaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        var totalRevenue = await _restaurantRepository.CalculateRestaurantRevenue(restaurantId);

        return Ok(new { totalRevenue });
    }
}
