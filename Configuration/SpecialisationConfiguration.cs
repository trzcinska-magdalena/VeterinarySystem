using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class SpecialisationConfiguration : IEntityTypeConfiguration<Specialisation>
    {
        public void Configure(EntityTypeBuilder<Specialisation> entity)
        {
            entity.ToTable("Specialisation");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).HasMaxLength(30).IsRequired();
        }
    }
}
