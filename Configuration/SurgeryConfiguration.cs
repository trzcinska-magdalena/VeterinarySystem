using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class SurgeryConfiguration : IEntityTypeConfiguration<Surgery>
    {
        public void Configure(EntityTypeBuilder<Surgery> entity)
        {
            entity.ToTable("Surgery");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).HasMaxLength(30).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(300).IsRequired();
        }
    }
}
