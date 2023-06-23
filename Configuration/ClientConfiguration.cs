using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> entity)
        {
            entity.ToTable("Client");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FirstName).HasMaxLength(30).IsRequired();
            entity.Property(e => e.LastMame).HasMaxLength(70).IsRequired();
            entity.Property(e => e.City).HasMaxLength(150).IsRequired();
            entity.Property(e => e.PhoneNumber).HasMaxLength(12).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(50);
        }
    }
}
