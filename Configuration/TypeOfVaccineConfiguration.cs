using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class TypeOfVaccineConfiguration : IEntityTypeConfiguration<TypeOfVaccine>
    {
        public void Configure(EntityTypeBuilder<TypeOfVaccine> entity)
        {
            entity.ToTable("TypeOfVaccine");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
        }
    }
}
