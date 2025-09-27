using Microsoft.AspNetCore.Http;
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
    public class HistoriesController : ControllerBase
    {
        private readonly IHistoryService _historyService;

        public HistoriesController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet(Name = nameof(GetMaintenanceHistories))]
        public async Task<ActionResult<IEnumerable<HistoryDTO>>> GetMaintenanceHistories([FromQuery] PaginationParams paginationParams)
        {
            var pagedHistories = await _historyService.GetPagedHistoriesAsync(paginationParams);

            var metadata = new
            {
                pagedHistories.TotalCount,
                pagedHistories.PageSize,
                pagedHistories.CurrentPage,
                pagedHistories.TotalPages,
                pagedHistories.HasNext,
                pagedHistories.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            foreach (var history in pagedHistories.Items)
            {
                history.Links.Add(new LinkDto(Url.Link(nameof(GetMaintenanceHistory), new { id = history.MaintenanceHistoryID }), "self", "GET"));
                history.Links.Add(new LinkDto(Url.Link(nameof(PutMaintenanceHistory), new { id = history.MaintenanceHistoryID }), "update_history", "PUT"));
                history.Links.Add(new LinkDto(Url.Link(nameof(DeleteMaintenanceHistory), new { id = history.MaintenanceHistoryID }), "delete_history", "DELETE"));
            }

            return Ok(pagedHistories.Items);
        }

        [HttpGet("{id:guid}", Name = nameof(GetMaintenanceHistory))]
        public async Task<ActionResult<HistoryDTO>> GetMaintenanceHistory(Guid id)
        {
            var history = await _historyService.GetHistoryByIdAsync(id);
            if (history == null)
            {
                return NotFound();
            }

            history.Links.Add(new LinkDto(Url.Link(nameof(GetMaintenanceHistory), new { id = history.MaintenanceHistoryID }), "self", "GET"));
            history.Links.Add(new LinkDto(Url.Link(nameof(PutMaintenanceHistory), new { id = history.MaintenanceHistoryID }), "update_history", "PUT"));
            history.Links.Add(new LinkDto(Url.Link(nameof(DeleteMaintenanceHistory), new { id = history.MaintenanceHistoryID }), "delete_history", "DELETE"));

            return Ok(history);
        }

        [HttpPut("{id:guid}", Name = nameof(PutMaintenanceHistory))]
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

        [HttpPost(Name = nameof(PostMaintenanceHistory))]
        public async Task<ActionResult<HistoryDTO>> PostMaintenanceHistory(HistoryDTO historyDto)
        {
            var createdHistory = await _historyService.CreateHistoryAsync(historyDto);

            createdHistory.Links.Add(new LinkDto(Url.Link(nameof(GetMaintenanceHistory), new { id = createdHistory.MaintenanceHistoryID }), "self", "GET"));
            createdHistory.Links.Add(new LinkDto(Url.Link(nameof(PutMaintenanceHistory), new { id = createdHistory.MaintenanceHistoryID }), "update_history", "PUT"));
            createdHistory.Links.Add(new LinkDto(Url.Link(nameof(DeleteMaintenanceHistory), new { id = createdHistory.MaintenanceHistoryID }), "delete_history", "DELETE"));

            return CreatedAtAction(nameof(GetMaintenanceHistory), new { id = createdHistory.MaintenanceHistoryID }, createdHistory);
        }

        [HttpDelete("{id:guid}", Name = nameof(DeleteMaintenanceHistory))]
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

