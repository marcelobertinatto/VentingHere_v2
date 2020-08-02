using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class CompanySectorConfig : IEntityTypeConfiguration<CompanySector>
    {
        public void Configure(EntityTypeBuilder<CompanySector> builder)
        {
            builder.HasKey(c => new { c.Id });

            //many to many: Sector and ListCompanySector
            builder.HasOne(ur => ur.Sector)
                .WithMany(r => r.ListCompanySector)
                .HasForeignKey(x => x.SectorId)
                .IsRequired(true);

            //many to many: Company and ListCompanySector
            builder.HasOne(ur => ur.Company)
                .WithMany(r => r.ListCompanySector)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired(true);
        }
    }
}
