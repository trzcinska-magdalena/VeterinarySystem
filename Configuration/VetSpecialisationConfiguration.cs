using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class VetSpecialisationConfiguration : IEntityTypeConfiguration<VetSpecialisation>
    {
        public void Configure(EntityTypeBuilder<VetSpecialisation> entity)
        {
            entity.ToTable("VetSpecialisation");
            entity.HasKey(e => new { e.SpecialisationId, e.VetId });

            entity.Property(e => e.DateFrom).IsRequired();

            entity.HasOne(e => e.Specialisation)
            .WithMany(e => e.VetSpecialisations)
            .HasForeignKey(e => e.SpecialisationId)
            .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Vet)
            .WithMany(e => e.VetSpecialisations)
            .HasForeignKey(e => e.VetId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
