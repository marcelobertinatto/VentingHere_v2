using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class RateConfig : IEntityTypeConfiguration<Rate>
    {
        public void Configure(EntityTypeBuilder<Rate> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //one to one -> Company and Rate
            builder.HasMany(x => x.ListCompanyRates)
                .WithOne(x => x.Rate)
                .IsRequired(true);
        }
    }
}
