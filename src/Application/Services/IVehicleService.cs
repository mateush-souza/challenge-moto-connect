using challenge_moto_connect.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace challenge_moto_connect.Application.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDTO>> GetAllVehiclesAsync();
        Task<VehicleDTO> GetVehicleByIdAsync(Guid id);
        Task<VehicleDTO> CreateVehicleAsync(VehicleDTO vehicleDto);
        Task UpdateVehicleAsync(Guid id, VehicleDTO vehicleDto);
        Task DeleteVehicleAsync(Guid id);
    }
}


