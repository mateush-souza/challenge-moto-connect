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
        /// Get all vehicles
        /// </summary>
        /// <response code="200">Return all vehicles</response>
        /// <response code="404">No vehicles found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/Vehicles/5
        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <response code="200">Return all vehicles</response>
        /// <response code="404">No vehicles found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(Guid id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <response code="200">Return all vehicles</response>
        /// <response code="404">No vehicles found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
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
                existingVehicle.IsCancel = vehicle.IsCancel;
                existingVehicle.UserCancelID = vehicle.UserCancelID;

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
                // Log do erro com detalhes para debug
                Console.WriteLine($"Erro ao atualizar veículo: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                // Retornar erro com detalhes para facilitar diagnóstico
                return StatusCode(500, new { error = "Erro interno", message = ex.Message, innerException = ex.InnerException?.Message });
            }

            return NoContent();
        }
        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <response code="200">Return all vehicles</response>
        /// <response code="404">No vehicles found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.VehicleId }, vehicle);
        }

        // DELETE: api/Vehicles/5
        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <response code="200">Return all vehicles</response>
        /// <response code="404">No vehicles found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
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
