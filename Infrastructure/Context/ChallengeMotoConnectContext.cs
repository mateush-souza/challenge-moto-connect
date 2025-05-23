
using challenge_moto_connect.Domain.Entity;
using challenge_moto_connect.Infra.Data.Mappings;
using challenge_moto_connect.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

namespace challenge_moto_connect.Infrastructure.Context
{
    public class ChallengeMotoConnectContext(DbContextOptions<ChallengeMotoConnectContext> options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<MaintenanceHistory> MaintenanceHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new MaintenanceHistoryMapping());

        }

    }
}
