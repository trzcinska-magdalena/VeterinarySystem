using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class AppointmentMedicineConfiguration : IEntityTypeConfiguration<AppointmentMedicine>
    {
        public void Configure(EntityTypeBuilder<AppointmentMedicine> entity)
        {
            entity.ToTable("AppointmentMedicine");
            entity.HasKey(e => new {e.AppointmentId, e.MedicineId});

            entity.Property(e => e.Amount).IsRequired();

            entity.HasOne(e => e.Medicine)
            .WithMany(e => e.AppointmentMedicines)
            .HasForeignKey(e => e.MedicineId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Appointment)
            .WithMany(e => e.AppointmentMedicines)
            .HasForeignKey(e => e.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
