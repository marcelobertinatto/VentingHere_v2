using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class CompanySubjectIssueConfig : IEntityTypeConfiguration<CompanySubjectIssue>
    {
        public void Configure(EntityTypeBuilder<CompanySubjectIssue> builder)
        {
            builder.HasKey(c => new { c.Id });

            //many to many: Company and ListCompanySubjectIssues
            builder.HasOne(ur => ur.Company)
                .WithMany(r => r.ListCompanySubjectIssues)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired(true);

            //many to many: Subject and ListCompanySubjectIssues
            builder.HasOne(ur => ur.Subject)
                .WithMany(r => r.ListCompanySubjectIssues)
                .HasForeignKey(x => x.SubjectId)
                .IsRequired(true);

            //many to many: SubjectIssue and ListCompanySubjectIssues
            builder.HasOne(ur => ur.SubjectIssue)
                .WithMany(r => r.ListCompanySubjectIssues)
                .HasForeignKey(x => x.SubjectIssueId)
                .IsRequired(true);

            //one to many: UserSubjectTellUs with Users
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}
