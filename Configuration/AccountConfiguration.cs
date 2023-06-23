using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinarySystem.Models.Db;

namespace VeterinarySystem.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> entity)
        {
            entity.ToTable("Account");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Login).HasMaxLength(30).IsRequired();
            entity.Property(e => e.Password).HasMaxLength(20).IsRequired();
            entity.Property(e => e.Admin).IsRequired();
        }
    }
}
