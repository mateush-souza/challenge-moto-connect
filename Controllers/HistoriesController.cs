using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using challenge_moto_connect.Domain.Entity;
using challenge_moto_connect.Infrastructure.Context;

namespace challenge_moto_connect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class HistoriesController : ControllerBase
    {
        private readonly ChallengeMotoConnectContext _context;

        public HistoriesController(ChallengeMotoConnectContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all maintenance history records.
        /// </summary>
        /// <remarks>
        /// Returns <see cref="IEnumerable{History}"/> representing every maintenance history stored in the database.
        /// The collection can be empty when no records exist.
        /// </remarks>
        /// <returns>A list of <see cref="History"/> objects.</returns>
        /// <response code="200">Successful operation. The response body contains the collection of maintenance histories.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<History>>> GetMaintenanceHistories()
        {
            return Ok(await _context.Histories.ToListAsync());
        }

        /// <summary>
        /// Retrieves a maintenance history by its identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the maintenance history.</param>
        /// <returns>The requested <see cref="History"/> when found.</returns>
        /// <response code="200">Successful operation. The response body contains the maintenance history.</response>
        /// <response code="404">No maintenance history with the specified <paramref name="id"/> was found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<History>> GetMaintenanceHistory(Guid id)
        {
            var maintenanceHistory = await _context.Histories.FindAsync(id);

            if (maintenanceHistory is null)
            {
                return NotFound();
            }

            return Ok(maintenanceHistory);
        }

        /// <summary>
        /// Updates an existing maintenance history.
        /// </summary>
        /// <param name="id">The identifier of the maintenance history to update.</param>
        /// <param name="maintenanceHistory">The updated maintenance history object.</param>
        /// <returns>No content.</returns>
        /// <response code="204">The maintenance history was successfully updated.</response>
        /// <response code="400">The <paramref name="id"/> in the route does not match the identifier of the provided <paramref name="maintenanceHistory"/>.</response>
        /// <response code="404">No maintenance history with the specified <paramref name="id"/> exists.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutMaintenanceHistory(Guid id, History maintenanceHistory)
        {
            if (id != maintenanceHistory.MaintenanceHistoryID)
            {
                return BadRequest();
            }

            _context.Entry(maintenanceHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceHistoryExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a new maintenance history record.
        /// </summary>
        /// <param name="maintenanceHistory">The maintenance history to create.</param>
        /// <returns>The newly created maintenance history.</returns>
        /// <response code="201">The maintenance history was successfully created.</response>
        /// <response code="400">The provided <paramref name="maintenanceHistory"/> is invalid.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<History>> PostMaintenanceHistory(History maintenanceHistory)
        {
            _context.Histories.Add(maintenanceHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMaintenanceHistory), new { id = maintenanceHistory.MaintenanceHistoryID }, maintenanceHistory);
        }

        /// <summary>
        /// Deletes a maintenance history record.
        /// </summary>
        /// <param name="id">The identifier of the maintenance history to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">The maintenance history was successfully deleted.</response>
        /// <response code="404">No maintenance history with the specified <paramref name="id"/> exists.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMaintenanceHistory(Guid id)
        {
            var maintenanceHistory = await _context.Histories.FindAsync(id);
            if (maintenanceHistory is null)
            {
                return NotFound();
            }

            _context.Histories.Remove(maintenanceHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaintenanceHistoryExists(Guid id) =>
            _context.Histories.Any(e => e.MaintenanceHistoryID == id);
    }
}
