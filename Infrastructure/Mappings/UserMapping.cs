using challenge_moto_connect.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace challenge_moto_connect.Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserID);

            builder
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(u => u.Type)
                .IsRequired();

            // Converter bool para NUMBER(1) se usar Oracle
            var boolToNumberConverter = new ValueConverter<bool, int>(
                v => v ? 1 : 0,
                v => v == 1);

            builder
                .Property(u => u.IsCancel)
                .HasConversion(boolToNumberConverter)
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder
                .Property(u => u.UserCancelID)
                .HasColumnType("RAW(16)")
                .IsRequired();
        }
    }
}
