using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> entity)
        {
            entity.ToTable("Appointment");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(200).IsRequired();

            entity.HasOne(e => e.Animal)
            .WithMany(e => e.Appointments)
            .HasForeignKey(e => e.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
