using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using challenge_moto_connect.Application.DTOs;
using challenge_moto_connect.Application.Services;
using challenge_moto_connect.Application.DTOs.Pagination;
using challenge_moto_connect.Application.DTOs.HATEOAS;
using System.Text.Json;

namespace challenge_moto_connect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet(Name = nameof(GetVehicles))]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehicles([FromQuery] PaginationParams paginationParams)
        {
            var pagedVehicles = await _vehicleService.GetPagedVehiclesAsync(paginationParams);

            var metadata = new
            {
                pagedVehicles.TotalCount,
                pagedVehicles.PageSize,
                pagedVehicles.CurrentPage,
                pagedVehicles.TotalPages,
                pagedVehicles.HasNext,
                pagedVehicles.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            foreach (var vehicle in pagedVehicles.Items)
            {
                vehicle.Links.Add(new LinkDto(Url.Link(nameof(GetVehicle), new { id = vehicle.VehicleId }), "self", "GET"));
                vehicle.Links.Add(new LinkDto(Url.Link(nameof(PutVehicle), new { id = vehicle.VehicleId }), "update_vehicle", "PUT"));
                vehicle.Links.Add(new LinkDto(Url.Link(nameof(DeleteVehicle), new { id = vehicle.VehicleId }), "delete_vehicle", "DELETE"));
            }

            return Ok(pagedVehicles.Items);
        }

        [HttpGet("{id:guid}", Name = nameof(GetVehicle))]
        public async Task<ActionResult<VehicleDTO>> GetVehicle(Guid id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.Links.Add(new LinkDto(Url.Link(nameof(GetVehicle), new { id = vehicle.VehicleId }), "self", "GET"));
            vehicle.Links.Add(new LinkDto(Url.Link(nameof(PutVehicle), new { id = vehicle.VehicleId }), "update_vehicle", "PUT"));
            vehicle.Links.Add(new LinkDto(Url.Link(nameof(DeleteVehicle), new { id = vehicle.VehicleId }), "delete_vehicle", "DELETE"));

            return Ok(vehicle);
        }

        [HttpPut("{id:guid}", Name = nameof(PutVehicle))]
        public async Task<IActionResult> PutVehicle(Guid id, [FromBody] VehicleDTO vehicleDto)
        {
            if (id != vehicleDto.VehicleId)
            {
                return BadRequest(new { error = "O ID na URL não corresponde ao ID do veículo no corpo da requisição." });
            }

            try
            {
                await _vehicleService.UpdateVehicleAsync(id, vehicleDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Veículo com ID {id} não encontrado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar veículo: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                return StatusCode(500, new { error = "Erro interno", message = ex.Message, innerException = ex.InnerException?.Message });
            }

            return NoContent();
        }

        [HttpPost(Name = nameof(PostVehicle))]
        public async Task<ActionResult<VehicleDTO>> PostVehicle(VehicleDTO vehicleDto)
        {
            try
            {
                var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicleDto);

                createdVehicle.Links.Add(new LinkDto(Url.Link(nameof(GetVehicle), new { id = createdVehicle.VehicleId }), "self", "GET"));
                createdVehicle.Links.Add(new LinkDto(Url.Link(nameof(PutVehicle), new { id = createdVehicle.VehicleId }), "update_vehicle", "PUT"));
                createdVehicle.Links.Add(new LinkDto(Url.Link(nameof(DeleteVehicle), new { id = createdVehicle.VehicleId }), "delete_vehicle", "DELETE"));

                return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.VehicleId }, createdVehicle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}", Name = nameof(DeleteVehicle))]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            try
            {
                await _vehicleService.DeleteVehicleAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

