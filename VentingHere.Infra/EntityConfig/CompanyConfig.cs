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

            //one to many: Company and ListCompanyRates
            builder.HasMany(x => x.ListCompanyRates)
                .WithOne(x => x.Company)
                .IsRequired(true);

            //one to many: Company and ListCompanySubjectIssues
            builder.HasMany(x => x.ListCompanySubjectIssues)
                .WithOne(x => x.Company)
                //OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);

            //one to one -> Company and ListCompanySector
            builder.HasMany(x => x.ListCompanySector)
                .WithOne(x => x.Company)
                .IsRequired(true);
        }
    }
}
