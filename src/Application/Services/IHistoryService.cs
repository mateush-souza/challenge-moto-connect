using challenge_moto_connect.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challenge_moto_connect.Application.Services
{
    public interface IHistoryService
    {
        Task<IEnumerable<HistoryDTO>> GetAllHistoriesAsync();
        Task<HistoryDTO> GetHistoryByIdAsync(Guid id);
        Task<HistoryDTO> CreateHistoryAsync(HistoryDTO historyDto);
        Task UpdateHistoryAsync(Guid id, HistoryDTO historyDto);
        Task DeleteHistoryAsync(Guid id);
    }
}


