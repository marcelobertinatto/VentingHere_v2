using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class SubjectIssueConfig : IEntityTypeConfiguration<SubjectIssue>
    {
        public void Configure(EntityTypeBuilder<SubjectIssue> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            //many to one: SubjectIssue and Subject
            builder.HasOne(x => x.Subject)
                .WithMany(x => x.SubjectIssue)
                .IsRequired(true);

            //one to many: SubjectIssue and Company
            builder.HasMany(x => x.ListCompanySubjectIssues)
                .WithOne(x => x.SubjectIssue)
                .IsRequired(true);
        }
    }
}
