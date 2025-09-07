using challenge_moto_connect.Application.DTOs;
using challenge_moto_connect.Domain.Entity;
using challenge_moto_connect.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challenge_moto_connect.Application.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IRepository<History> _historyRepository;

        public HistoryService(IRepository<History> historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<IEnumerable<HistoryDTO>> GetAllHistoriesAsync()
        {
            var histories = await _historyRepository.GetAllAsync();
            return histories.Select(h => new HistoryDTO
            {
                MaintenanceHistoryID = h.MaintenanceHistoryID,
                Description = h.Description,
                MaintenanceDate = h.MaintenanceDate,
                VehicleID = h.VehicleID
            });
        }

        public async Task<HistoryDTO> GetHistoryByIdAsync(Guid id)
        {
            var history = await _historyRepository.GetByIdAsync(id);
            if (history == null) return null;

            return new HistoryDTO
            {
                MaintenanceHistoryID = history.MaintenanceHistoryID,
                Description = history.Description,
                MaintenanceDate = history.MaintenanceDate,
                VehicleID = history.VehicleID
            };
        }

        public async Task<HistoryDTO> CreateHistoryAsync(HistoryDTO historyDto)
        {
            var history = new History
            {
                MaintenanceHistoryID = Guid.NewGuid(),
                Description = historyDto.Description,
                MaintenanceDate = historyDto.MaintenanceDate,
                VehicleID = historyDto.VehicleID
            };
            await _historyRepository.AddAsync(history);
            return historyDto;
        }

        public async Task UpdateHistoryAsync(Guid id, HistoryDTO historyDto)
        {
            var history = await _historyRepository.GetByIdAsync(id);
            if (history == null) throw new KeyNotFoundException("History not found.");

            history.Description = historyDto.Description;
            history.MaintenanceDate = historyDto.MaintenanceDate;
            history.VehicleID = historyDto.VehicleID;

            await _historyRepository.UpdateAsync(history);
        }

        public async Task DeleteHistoryAsync(Guid id)
        {
            var history = await _historyRepository.GetByIdAsync(id);
            if (history == null) throw new KeyNotFoundException("History not found.");

            await _historyRepository.DeleteAsync(history.MaintenanceHistoryID);
        }
    }
}


