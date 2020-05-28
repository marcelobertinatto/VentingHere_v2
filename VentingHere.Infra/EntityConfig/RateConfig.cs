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

            //one to many: User and Rates
            builder.HasOne(x => x.User)
                .WithMany(x => x.ListRates)
                .HasForeignKey(x => x.UserId)
                .IsRequired(false);

            //one to one -> Company and Rate
            builder.HasOne(x => x.Company)
                .WithOne(x => x.Rate)
                .HasForeignKey<Company>(x => x.RateId)
                .IsRequired(false);
        }
    }
}
