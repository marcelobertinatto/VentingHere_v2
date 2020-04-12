using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class VentConfig : IEntityTypeConfiguration<Vent>
    {
        public void Configure(EntityTypeBuilder<Vent> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.RegisterDateTime).HasColumnType("datetime");

            ////one to many -> Company and Vent
            //builder.HasOne(v => v.Company)
            //   .WithMany(a => a.ListVents)
            //   .HasForeignKey(x => x.CompanyId);

            //one to many -> User and Vent
            builder.HasOne(x => x.User)
                .WithMany(x => x.ListVents)
                .HasForeignKey(x => x.UserId)
                .IsRequired(true);

            //one to many -> Vent and Company
            builder.HasOne(x => x.Company)
                .WithMany(x => x.ListVents)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired(true);
        }
    }
}
