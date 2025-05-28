using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Reservations;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : Controller
{
    private readonly ReservationRepository _reservationRepository;
    private readonly RestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public ReservationsController(IMapper mapper, ReservationRepository reservationRepository, RestaurantRepository restaurantRepository)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _restaurantRepository = restaurantRepository;
    }

    [HttpGet("{id}", Name = "GetReservation")]
    public async Task<ActionResult<ReservationDto>> GetReservation(int id)
    {
        var reservation = await _reservationRepository.GetById(id);
        if (reservation == null)
            return NotFound();

        return Ok(_mapper.Map<ReservationDto>(reservation));
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDto>> CreateReservation(ReservationCreateDto reservationCreateDto)
    {
        if (!await _restaurantRepository.IsRestaurantExists(reservationCreateDto.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        var reservation = _mapper.Map<Reservation>(reservationCreateDto);
        var addedReservation = await _reservationRepository.Create(reservation);
        var createdReservationReturn = _mapper.Map<ReservationDto>(addedReservation);

        return CreatedAtRoute("GetReservation", new { id = addedReservation.ReservationId }, createdReservationReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteReservation(int id)
    {
        var existingReservation = await _reservationRepository.GetById(id);
        if (existingReservation == null)
        {
            return NotFound();
        }

        await _reservationRepository.DeleteById(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateReservation(int id, ReservationUpdateDto reservationUpdateDto)
    {
        var existingReservation = await _reservationRepository.GetById(id);
        if (existingReservation == null)
        {
            return NotFound();
        }

        _mapper.Map(reservationUpdateDto, existingReservation);
        await _reservationRepository.Update(existingReservation);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateReservation(int id, [FromBody] JsonPatchDocument<ReservationUpdateDto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var existingReservation = await _reservationRepository.GetById(id);
        if (existingReservation == null)
            return NotFound();

        var reservationToPatch = _mapper.Map<ReservationUpdateDto>(existingReservation);
        patchDocument.ApplyTo(reservationToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _mapper.Map(reservationToPatch, existingReservation);
        await _reservationRepository.Update(existingReservation);

        return NoContent();
    }
}
