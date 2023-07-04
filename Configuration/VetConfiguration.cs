using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class VetConfiguration : IEntityTypeConfiguration<Vet>
    {
        public void Configure(EntityTypeBuilder<Vet> entity)
        {
            entity.ToTable("Vet");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FirstName).HasMaxLength(30).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(20).IsRequired();

            entity.HasOne(e => e.ApplicationUser)
            .WithOne(e => e.Vet)
            .HasForeignKey<Vet>(e => e.ApplicationUserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
