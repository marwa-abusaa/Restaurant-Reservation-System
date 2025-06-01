using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.API.Models.Tables;
using RestaurantReservation.Db.Models;
using RestaurantReservation.Db.Repositories;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TablesController : Controller
{
    private readonly TableRepository _tableRepository;
    private readonly RestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;
    

    public TablesController(IMapper mapper, TableRepository tableRepository, RestaurantRepository restaurantRepository)
    {
        _tableRepository = tableRepository;
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}", Name = "GetTable")]
    public async Task<ActionResult<TableDto>> GetTable(int id)
    {
        var table = await _tableRepository.GetById(id);
        if (table == null)
            return NotFound();

        return Ok(_mapper.Map<TableDto>(table));
    }

    [HttpPost]
    public async Task<ActionResult<TableDto>> CreateTable(TableCreateDto tableCreateDto)
    {
        if (!await _restaurantRepository.IsRestaurantExists(tableCreateDto.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        var table = _mapper.Map<Table>(tableCreateDto);
        var addedTable = await _tableRepository.Create(table);
        var createdTableReturn = _mapper.Map<TableDto>(addedTable);

        return CreatedAtRoute("GetTable", new { id = addedTable.TableId }, createdTableReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTable(int id)
    {
        var existingTable = await _tableRepository.GetById(id);
        if (existingTable == null)
        {
            return NotFound();
        }

        await _tableRepository.DeleteById(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTable(int id, TableUpdateDto tableUpdateDto)
    {
        var existingTable = await _tableRepository.GetById(id);
        if (existingTable == null)
        {
            return NotFound();
        }

        if (!await _restaurantRepository.IsRestaurantExists(tableUpdateDto.RestaurantId))
        {
            return NotFound(new { Message = "Restaurant not found." });
        }

        _mapper.Map(tableUpdateDto, existingTable);
        await _tableRepository.Update(existingTable);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PartiallyUpdateTable(int id, [FromBody] JsonPatchDocument<TableUpdateDto> patchDocument)
    {
        if (patchDocument == null)
            return BadRequest();

        var existingTable = await _tableRepository.GetById(id);
        if (existingTable == null)
            return NotFound();

        var tableToPatch = _mapper.Map<TableUpdateDto>(existingTable);
        patchDocument.ApplyTo(tableToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _mapper.Map(tableToPatch, existingTable);
        await _tableRepository.Update(existingTable);

        return NoContent();
    }
}
