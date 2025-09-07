using System;

namespace challenge_moto_connect.Application.DTOs
{
    public class HistoryDTO
    {
        public Guid MaintenanceHistoryID { get; set; }
        public string Description { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public Guid VehicleID { get; set; }
    }
}


