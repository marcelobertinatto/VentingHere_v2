using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class CompanyRateConfig : IEntityTypeConfiguration<CompanyRate>
    {
        public void Configure(EntityTypeBuilder<CompanyRate> builder)
        {
            builder.HasKey(c => new { c.Id });

            //many to many: Company and ListCompanyRates
            builder.HasOne(ur => ur.Company)
                .WithMany(r => r.ListCompanyRates)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired(true);

            //many to many: Rate and ListCompanyRates
            builder.HasOne(ur => ur.Rate)
                .WithMany(r => r.ListCompanyRates)
                .HasForeignKey(x => x.RateId)
                .IsRequired(true);

            //one to many: User and Company/Rates
            builder.HasOne(x => x.User)
                .WithMany(x => x.ListCompanyRates)
                .HasForeignKey(x => x.UserId)
                .IsRequired(true);
        }
    }
}
