using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> entity)
        {
            entity.ToTable("Breed");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).HasMaxLength(30).IsRequired();
        }
    }
}
