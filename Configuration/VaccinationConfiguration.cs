using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class VaccinationConfiguration : IEntityTypeConfiguration<Vaccination>
    {
        public void Configure(EntityTypeBuilder<Vaccination> entity)
        {
            entity.ToTable("Vaccination");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.ExpiryDate).IsRequired();

            entity.HasOne(e => e.Animal)
            .WithMany(e => e.Vaccinations)
            .HasForeignKey(e => e.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.TypeOfVaccine)
            .WithMany(e => e.Vaccinations)
            .HasForeignKey(e => e.TypeOfVaccineId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
