using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class AppointmentVetConfiguration : IEntityTypeConfiguration<AppointmentVet>
    {
        public void Configure(EntityTypeBuilder<AppointmentVet> entity)
        {
            entity.ToTable("AppointmentVet");
            entity.HasKey(e => new { e.AppointmentId, e.VetId });

            entity.HasOne(e => e.Vet)
            .WithMany(e => e.AppointmentVets)
            .HasForeignKey(e => e.VetId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Appointment)
            .WithMany(e => e.AppointmentVets)
            .HasForeignKey(e => e.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
