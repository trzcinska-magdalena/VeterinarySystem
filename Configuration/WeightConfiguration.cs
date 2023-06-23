using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class WeightConfiguration : IEntityTypeConfiguration<Weight>
    {
        public void Configure(EntityTypeBuilder<Weight> entity)
        {
            entity.ToTable("Weight");
            entity.HasKey(e => new {e.AnimalId, e.Date});

            entity.Property(e => e.Value).IsRequired();
            entity.Property(e => e.Date).IsRequired();

            entity.HasOne(e => e.Animal)
            .WithMany(e => e.Weights)
            .HasForeignKey(e => e.AnimalId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
