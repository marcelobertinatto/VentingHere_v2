using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class SectorConfig : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
           
        }
    }
}
