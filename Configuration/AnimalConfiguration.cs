using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> entity)
        {
            entity.ToTable("Animal");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.BirthDate).IsRequired();
            entity.Property(e => e.Gender).HasMaxLength(1).IsRequired();

            entity.HasOne(e => e.Breed)
                .WithMany(e => e.Animals)
                .HasForeignKey(e => e.BreedId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Client)
                .WithMany(e => e.Animals)
                .HasForeignKey(e => e.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
