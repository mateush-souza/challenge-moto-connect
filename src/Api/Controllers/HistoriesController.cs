using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using challenge_moto_connect.Application.DTOs;
using challenge_moto_connect.Application.Services;

namespace challenge_moto_connect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoriesController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryDTO>>> GetMaintenanceHistories()
        {
            var histories = await _historyService.GetAllHistoriesAsync();
            return Ok(histories);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<HistoryDTO>> GetMaintenanceHistory(Guid id)
        {
            var history = await _historyService.GetHistoryByIdAsync(id);
            if (history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutMaintenanceHistory(Guid id, HistoryDTO historyDto)
        {
            if (id != historyDto.MaintenanceHistoryID)
            {
                return BadRequest();
            }

            try
            {
                await _historyService.UpdateHistoryAsync(id, historyDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HistoryDTO>> PostMaintenanceHistory(HistoryDTO historyDto)
        {
            var createdHistory = await _historyService.CreateHistoryAsync(historyDto);
            return CreatedAtAction(nameof(GetMaintenanceHistory), new { id = createdHistory.MaintenanceHistoryID }, createdHistory);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteMaintenanceHistory(Guid id)
        {
            try
            {
                await _historyService.DeleteHistoryAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}


