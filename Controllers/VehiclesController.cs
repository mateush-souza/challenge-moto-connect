using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using challenge_moto_connect.Domain.Entity;
using challenge_moto_connect.Infrastructure.Context;

namespace challenge_moto_connect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ChallengeMotoConnectContext _context;

        public VehiclesController(ChallengeMotoConnectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all vehicles.
        /// </summary>
        /// <returns>List of vehicles.</returns>
        /// <response code="200">Returns the list of vehicles</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Vehicle>), 200)]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific vehicle by unique ID.
        /// </summary>
        /// <param name="id">Vehicle ID</param>
        /// <returns>A single vehicle</returns>
        /// <response code="200">Returns the requested vehicle</response>
        /// <response code="404">If the vehicle is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Vehicle), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Vehicle>> GetVehicle(Guid id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        /// <summary>
        /// Updates an existing vehicle.
        /// </summary>
        /// <param name="id">Vehicle ID</param>
        /// <param name="vehicle">Updated vehicle object</param>
        /// <response code="204">Vehicle successfully updated</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Vehicle not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PutVehicle(Guid id, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = "Modelo inválido",
                    details = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });
            }

            if (id != vehicle.VehicleId)
            {
                return BadRequest(new { error = "O ID na URL não corresponde ao ID do veículo no corpo da requisição." });
            }

            var existingVehicle = await _context.Vehicles.FindAsync(id);
            if (existingVehicle == null)
            {
                return NotFound($"Veículo com ID {id} não encontrado.");
            }

            try
            {
                existingVehicle.LicensePlate = vehicle.LicensePlate;
                existingVehicle.VehicleModel = vehicle.VehicleModel;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound($"Veículo com ID {id} não encontrado durante atualização.");
                }
                else
                {
                    throw;
                }
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

        /// <summary>
        /// Creates a new vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle to create</param>
        /// <returns>The newly created vehicle</returns>
        /// <response code="201">Vehicle created successfully</response>
        [HttpPost]
        [ProducesResponseType(typeof(Vehicle), 201)]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
        }

        /// <summary>
        /// Deletes a specific vehicle.
        /// </summary>
        /// <param name="id">Vehicle ID</param>
        /// <response code="204">Vehicle successfully deleted</response>
        /// <response code="404">Vehicle not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteVehicle(Guid id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleExists(Guid id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
