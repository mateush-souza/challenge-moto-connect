using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using challenge_moto_connect.Domain.Entity;
using challenge_moto_connect.Infrastructure.Context;

namespace challenge_moto_connect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceHistoriesController : ControllerBase
    {
        private readonly ChallengeMotoConnectContext _context;

        public MaintenanceHistoriesController(ChallengeMotoConnectContext context)
        {
            _context = context;
        }

        // GET: api/maintenancehistories
        /// <summary>
        /// Get all maintenance histories
        /// </summary>
        /// <response code="200">Return all maintenance history</response>
        /// <response code="404">No Maintenance History Found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceHistory>>> GetMaintenanceHistory()
        {
            return await _context.MaintenanceHistories.ToListAsync();
        }

        // GET: api/MaintenanceHistories/5
        /// <summary>
        /// Get all maintenance histories
        /// </summary>
        /// <response code="200">Return all maintenance history</response>
        /// <response code="404">No Maintenance History Found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceHistory>> GetMaintenanceHistory(Guid id)
        {
            var maintenanceHistory = await _context.MaintenanceHistories.FindAsync(id);

            if (maintenanceHistory == null)
            {
                return NotFound();
            }

            return maintenanceHistory;
        }

        // PUT: api/MaintenanceHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Get all maintenance histories
        /// </summary>
        /// <response code="200">Return all maintenance history</response>
        /// <response code="404">No Maintenance History Found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaintenanceHistory(Guid id, MaintenanceHistory maintenanceHistory)
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
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MaintenanceHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Get all maintenance histories
        /// </summary>
        /// <response code="200">Return all maintenance history</response>
        /// <response code="404">No Maintenance History Found</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        public async Task<ActionResult<MaintenanceHistory>> PostMaintenanceHistory(MaintenanceHistory maintenanceHistory)
        {
            _context.MaintenanceHistories.Add(maintenanceHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaintenanceHistory", new { id = maintenanceHistory.MaintenanceHistoryID }, maintenanceHistory);
        }

        // DELETE: api/MaintenanceHistories/5
        /// <summary>
        /// Get all maintenance histories
        /// </summary>
        /// <response code="200">Return all maintenance history</response>
        /// <response code="404">No Maintenance History Found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceHistory(Guid id)
        {
            var maintenanceHistory = await _context.MaintenanceHistories.FindAsync(id);
            if (maintenanceHistory == null)
            {
                return NotFound();
            }

            _context.MaintenanceHistories.Remove(maintenanceHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaintenanceHistoryExists(Guid id)
        {
            return _context.MaintenanceHistories.Any(e => e.MaintenanceHistoryID == id);
        }
    }
}
