using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //one to one -> Company and Sector
            builder.HasOne(v => v.Sector)
               .WithOne(a => a.Company)
               .HasForeignKey<Sector>(b => b.CompanyId)
               .IsRequired(true);

            builder.HasIndex(x => x.RateId).IsUnique(false);
        }
    }
}
