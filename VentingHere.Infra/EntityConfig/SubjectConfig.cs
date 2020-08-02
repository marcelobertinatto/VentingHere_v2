using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class SubjectConfig : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //one to many Subject and SubjectIssue
            builder.HasMany(x => x.SubjectIssue)
                .WithOne(x => x.Subject)                
                .IsRequired(true);

            //one to many
            builder.HasMany(x => x.ListCompanySubjectIssues)
                .WithOne(x => x.Subject)
                .IsRequired(true);
        }
    }
}
