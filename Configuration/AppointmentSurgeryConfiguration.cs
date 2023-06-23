using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class AppointmentSurgeryConfiguration : IEntityTypeConfiguration<AppointmentSurgery>
    {
        public void Configure(EntityTypeBuilder<AppointmentSurgery> entity)
        {
            entity.ToTable("AppointmentSurgery");
            entity.HasKey(e => new { e.AppointmentId, e.SurgeryId });

            entity.HasOne(e => e.Surgery)
            .WithMany(e => e.AppointmentSurgeries)
            .HasForeignKey(e => e.SurgeryId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Appointment)
            .WithMany(e => e.AppointmentSurgeries)
            .HasForeignKey(e => e.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
