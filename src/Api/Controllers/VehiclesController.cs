using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using challenge_moto_connect.Application.DTOs;
using challenge_moto_connect.Application.Services;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetVehicles()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<VehicleDTO>> GetVehicle(Guid id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpPut("{id:guid}")]
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

        [HttpPost]
        public async Task<ActionResult<VehicleDTO>> PostVehicle(VehicleDTO vehicleDto)
        {
            var createdVehicle = await _vehicleService.CreateVehicleAsync(vehicleDto);
            return CreatedAtAction(nameof(GetVehicle), new { id = createdVehicle.VehicleId }, createdVehicle);
        }

        [HttpDelete("{id:guid}")]
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


