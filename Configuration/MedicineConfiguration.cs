using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> entity)
        {
            entity.ToTable("Medicine");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(200).IsRequired();
        }
    }
}
